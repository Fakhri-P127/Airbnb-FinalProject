using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Exceptions.GuestReviews;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Application.Features.Client.GuestReviews.Commands.Create;
using Airbnb.Application.Helpers;
using Airbnb.Application.Mapping;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Domain.Enums.Reservations;
using AutoMapper;
using Bogus;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq.Expressions;
using System.Security.Claims;
using static Airbnb.Application.Contracts.v1.ApiRoutes;

namespace Airbnb.Application.Tests.Features.GuestReviewsTests.Commands
{
    public class CreateGuestReviewCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnit;
        private readonly Mock<IHttpContextAccessor> _mockAccessor;
        private readonly CreateGuestReviewCommandHandler _handler;
        private List<GuestReview> _guestReviews = new();
        private List<Reservation> _reservations;
        private Host _hosts;
        private List<AppUser> _users;
        private readonly Guid _userId;
        public CreateGuestReviewCommandHandlerTests()
        {
            _mockUnit = new Mock<IUnitOfWork>();
          
            var mapperConfig = new MapperConfiguration(config =>config.AddProfile<GuestReviewMappings>());
            _mapper = mapperConfig.CreateMapper();
            _users = new Faker<AppUser>()
             .RuleFor(x => x.Id, d => d.Random.Guid())
             .RuleFor(x => x.Firstname, d => d.Person.FirstName)
             .RuleFor(x => x.Lastname, d => d.Person.LastName)
             .RuleFor(x => x.UserName, d => d.Person.UserName)
             .RuleFor(x => x.Email, d => d.Person.Email)
             .RuleFor(x => x.DateOfBirth, d => d.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(-18)))
             .Generate(2);
            _hosts = new Faker<Host>()
             .RuleFor(x => x.Id, d => d.Random.Guid())
                .RuleFor(x => x.Status, d => d.Random.Number(1, 3))
                .RuleFor(x => x.AppUserId, _users.First().Id)
                .Generate();
            _reservations = new Faker<Reservation>()
             .RuleFor(x => x.Id, d => d.Random.Guid())
                .RuleFor(x => x.HostId, _hosts.Id)
                .RuleFor(x => x.AppUserId, _users.First().Id)
                .RuleFor(x => x.Status, d => d.Random.Number(1, 6))
                .RuleFor(x => x.AdultCount, d => d.Random.Number(1, 5))
                .RuleFor(x => x.ChildCount, d => d.Random.Number(1, 3))
                .RuleFor(x => x.CheckInDate, d => d.Date.Between(DateTime.Now, DateTime.Now.AddYears(1)))
                .RuleFor(x => x.CheckInDate, d => d.Date.Between(DateTime.Now.AddDays(1), DateTime.Now.AddYears(1)))
                .Generate(2);
            _reservations.First().Status = 5;
            _reservations.Last().Status = 2;//exceptioni yoxlamaq uchun
            _users.First().Host = _hosts;
            _hosts.Reservations.Add(_reservations.First());

            _userId = _users.Last().Id;
            _mockAccessor = new();
            DefaultHttpContext context = new();
            ClaimsIdentity claimIdentity = new(new[]
            {
                new Claim(ClaimTypes.NameIdentifier,_userId.ToString())
            });
            context.User = new ClaimsPrincipal(claimIdentity);
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);
            _handler = new CreateGuestReviewCommandHandler(_mockUnit.Object, _mapper, _mockAccessor.Object);
        }

        [Fact]
        public async Task Handle_WhenGuestReviewIsValid_CreatesANewGuestReview()
        {
            CreateGuestReviewCommand command = new()
            { GuestScore = 4, Text = "nice" /*, HostId = _hosts.Id, ReservationId = _reservations.First().Id*/ };

            _mockUnit.Setup(x => x.HostRepository.GetSingleAsync(
            It.IsAny<Expression<Func<Host, bool>>>(), true, "AppUser"))
            .ReturnsAsync(_hosts);

            _mockUnit.Setup(x => x.ReservationRepository.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<Expression<Func<Reservation, bool>>>(), false, "GuestReview"))
                .ReturnsAsync(_reservations.First());

            GuestReview guestReview = _mapper.Map<GuestReview>(command);
            //guestReview.AppUserId = _reservations.First().AppUserId;
            guestReview.Id = Guid.NewGuid();

            _mockUnit.Setup(x => x.GuestReviewRepository.AddAsync(It.IsAny<GuestReview>()))
                .Callback<GuestReview>(gReview => _guestReviews.Add(gReview));

            _mockUnit.Setup(x => x.GuestReviewRepository.GetByIdAsync(It.IsAny<Guid>(), null, false, GuestReviewHelper.AllGuestReviewIncludes()))
              .ReturnsAsync(guestReview);

            var result = await _handler.Handle(command, It.IsAny<CancellationToken>());

            _guestReviews.Should().NotBeNull().And.HaveCount(1);
            result.Should().BeOfType<GuestReviewResponse>();
            result.Text.Should().Be(command.Text);
            result.GuestScore.Should().Be(command.GuestScore);
            Console.WriteLine();
        }
        [Fact]
        public async Task CheckExceptionsThenReturnReservation_WhenHostWithThisIdDoesNotExist_ThrowsHostNotFoundException()
        {
            //1 hostumuz var ancaq, hostun appuserId si birinci user in Id sine beraberdi ve _userId ise 2ci userin Id sine,
            //ona gore tapa bilmir.

            _mockUnit.Setup(x => x.HostRepository.GetSingleAsync(
                x => x.AppUserId == _userId, true,"AppUser"))
                .ReturnsAsync(_hosts.AppUserId != _userId ? null : _hosts);

            Func<Task> act = async () => await _handler.Handle(new CreateGuestReviewCommand(), It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<HostNotFoundException>();
        }
        [Fact]
        public async Task CheckExceptionsThenReturnReservation_WhenReservationWithThisIdDoesNotExist_ThrowReservationNotFoundException()
        {
            Guid reservationId = Guid.NewGuid();

            _mockUnit.Setup(x => x.HostRepository.GetSingleAsync(
                It.IsAny<Expression<Func<Host,bool>>>(), true, "AppUser"))
                .ReturnsAsync(_hosts);

            Reservation? returnValue = _reservations.FirstOrDefault(x => x.Id == reservationId);
            _mockUnit.Setup(x => x.ReservationRepository.GetByIdAsync(reservationId,
                It.IsAny<Expression<Func<Reservation, bool>>>(), false, "GuestReview"))
                .ReturnsAsync(returnValue is null ? null : returnValue);

            Func<Task> act = async () => await _handler.Handle(new CreateGuestReviewCommand(), It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<ReservationNotFoundException>();
        }
        [Fact]
        public async Task CheckExceptionsThenReturnReservation_WhenReservationHasNotFinished_ThrowNotAvailableYetException()
        {
            CreateGuestReviewCommand command = new()
            {
                GuestScore = 4,
                Text = "salam",
                ReservationId = _reservations.Last().Id
            };
            _mockUnit.Setup(x => x.HostRepository.GetSingleAsync(
             It.IsAny<Expression<Func<Host, bool>>>(), true, "AppUser"))
             .ReturnsAsync(_hosts);

            _mockUnit.Setup(x => x.ReservationRepository.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<Expression<Func<Reservation, bool>>>(), false, "GuestReview"))
                .ReturnsAsync(_reservations.First(x=>x.Id==command.ReservationId));
            //if (reservation.Status != (int) Enum_ReservationStatus.ReservationFinished)
            //   throw new GuestReview_NotAvailableYetException(reservation.CheckOutDate);
            Func<Task> act = async () => await _handler.Handle(command, It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<GuestReview_NotAvailableYetException>();
        }
        [Fact]
        public async Task CheckExceptionsThenReturnReservation_WhenReservationHostIdIsSameWithAuthenticatedHostIdIsNotSame_ThrowGuestReview_HostIdNotMatchedException()
        {
            CreateGuestReviewCommand command = new()
            {
                ReservationId = _reservations.First().Id
            };
            //exception i yoxlamaq uchun yaradiriq
            Host host = new()
            { 
                Id = Guid.NewGuid()
            };
            _mockUnit.Setup(x => x.HostRepository.GetSingleAsync(
             It.IsAny<Expression<Func<Host, bool>>>(), true, "AppUser"))
             .ReturnsAsync(host);

            _mockUnit.Setup(x => x.ReservationRepository.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<Expression<Func<Reservation, bool>>>(), false, "GuestReview"))
                .ReturnsAsync(_reservations.First());
            
            Func<Task> act = async () => await _handler.Handle(command, It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<GuestReview_HostIdNotMatchedException>();
        }

        [Fact]
        public async Task CheckExceptionsThenReturnReservation_WhenReservationAlreadyHasGuestReview_ThrowGuestReviewDuplicateException()
        {
            GuestReview guestReview = new()
            {
                Id = Guid.NewGuid(),
                GuestScore = 4,
                IsDisplayed = true,
                Text = "awesome guest",
                HostId = _hosts.Id,
                ReservationId = _reservations.First().Id,
                AppUserId = _users.Last().Id
            };
            _guestReviews.Add(guestReview);
            _reservations.First().GuestReview = _guestReviews.First();// duplicate erroru uchun

            _mockUnit.Setup(x => x.HostRepository.GetSingleAsync(
               It.IsAny<Expression<Func<Host, bool>>>(), true, "AppUser"))
               .ReturnsAsync(_hosts);

            _mockUnit.Setup(x => x.ReservationRepository.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<Expression<Func<Reservation, bool>>>(), false, "GuestReview"))
                .ReturnsAsync(_reservations.First());

            Func<Task> act = async () => await _handler.Handle(new CreateGuestReviewCommand(), It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<GuestReviewDuplicateValidationException>();
        }

       
    }
}
