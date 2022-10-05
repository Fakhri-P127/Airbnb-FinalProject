using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.GuestReviews;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Application.Features.Client.GuestReviews.Commands.Update;
using Airbnb.Application.Helpers;
using Airbnb.Application.Mapping;
using Airbnb.Application.Tests.Datas.ReservationDatas;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq.Expressions;
using System.Security.Claims;
using Host = Airbnb.Domain.Entities.AppUserRelated.Host;

namespace Airbnb.Application.Tests.Features.GuestReviewsTests.Commands
{
    public class UpdateGuestReviewCommandHandlerTests
    {
        private readonly Mock<IHttpContextAccessor> _mockAccessor;
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnit;
        private  UpdateGuestReviewCommandHandler _handler;
        private readonly List<GuestReview> _guestReviews;
        private readonly List<Reservation> _reservations;
        private readonly Host _hosts;
        private readonly List<AppUser> _users;
        private readonly Guid _userId;

        public UpdateGuestReviewCommandHandlerTests()
        {
            _mockUnit = new Mock<IUnitOfWork>();
            _mockAccessor = new Mock<IHttpContextAccessor>();
            var mapperConfig = new MapperConfiguration(config => config.AddProfile<GuestReviewMappings>());
            _mapper = mapperConfig.CreateMapper();
            _guestReviews = new Faker<GuestReview>()
                .RuleFor(x => x.Id, d => d.Random.Guid())
                .RuleFor(x => x.Text, f => f.Lorem.Word())
                .RuleFor(x => x.GuestScore, f => f.Random.Float(1, 5))
                .Generate(8);
            _users = DatasForReservationTests.CreateListOfUsers();
            _hosts = DatasForReservationTests.CreateHost(_users);
            _reservations = DatasForReservationTests.CreateListOfReservations(_hosts, _users);

            _users.First().Host = _hosts;
            _hosts.Reservations.Add(_reservations.First());

            _userId = _users.Last().Id;

            //guest review Id ni route dan goturmek uchun
            DefaultHttpContext context = new();
            var routeId = _guestReviews.First().Id.ToString();
            context.Request.RouteValues["id"] = routeId;
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);
            _handler = new UpdateGuestReviewCommandHandler(_mockUnit.Object, _mapper, _mockAccessor.Object);
        }

      
        [Fact]
        public async Task Handle_WhenDataExistsWithGivenRouteId_UpdatesGuestReview()
        {
            UpdateGuestReviewCommand command = new() { Text = "updatedText", GuestScore = 4 };
            Guid Id = BaseHelper.GetIdFromRoute(_mockAccessor.Object);

            GuestReview updateGuestReview = _guestReviews.First(x => x.Id == Id);
            _mockUnit.Setup(x => x.GuestReviewRepository.GetByIdAsync(Id,
                It.IsAny<Expression<Func<GuestReview, bool>>>(), true, "Reservation"))
                .ReturnsAsync(updateGuestReview);

            DefaultHttpContext context = new();
            var routeId = _guestReviews.First().Id.ToString();//random id veririk ki, not found versin.
            context.Request.RouteValues["id"] = routeId;
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);
            ClaimsIdentity claimIdentity = new(new[]
            {
                new Claim(ClaimTypes.NameIdentifier,_userId.ToString())
            });
            context.User = new ClaimsPrincipal(claimIdentity);
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);

            // vaxt qalsa bu setup lari yaxshi orqanize ele, tekrar tekrar yazilmasin.
            _mockUnit.Setup(x => x.HostRepository.GetSingleAsync(
               It.IsAny<Expression<Func<Host, bool>>>(), false))
                .ReturnsAsync(_hosts);
            // authenticated olunan hostla eyni olub olmadigini yoxlamaq uchun deyerleri menimsedirik.
            _guestReviews.First().Host = _hosts;
            _guestReviews.First().Reservation = _reservations.First();

            _mockUnit.Setup(x => x.GuestReviewRepository.Update(It.IsAny<GuestReview>(), true))
                .Callback<GuestReview, bool>((gReview, entityState) =>
                {
                    updateGuestReview = _mapper.Map<GuestReview>(command);
                });
            _mockUnit.Setup(x => x.GuestReviewRepository.GetByIdAsync(Id,
               It.IsAny<Expression<Func<GuestReview, bool>>>(), false, GuestReviewHelper.AllGuestReviewIncludes()))
               .ReturnsAsync(updateGuestReview);

            _handler = new (_mockUnit.Object, _mapper, _mockAccessor.Object);

            var result = await _handler.Handle(command, It.IsAny<CancellationToken>());

            updateGuestReview.Should().NotBeNull();
            updateGuestReview.Text.Should().Be(command.Text);
            updateGuestReview.GuestScore.Should().Be(command.GuestScore);

            #region verifies
            _mockAccessor.Verify(x=>x.HttpContext, Times.Exactly(3));
            _mockUnit.Verify(x => x.HostRepository.GetSingleAsync(It.IsAny<Expression<Func<Host, bool>>>(),
                false));
            _mockUnit.Verify(x => x.GuestReviewRepository.Update(It.IsAny<GuestReview>(), true), Times.Once());
            _mockUnit.Verify(x => x.GuestReviewRepository.GetByIdAsync(Id,
                 It.IsAny<Expression<Func<GuestReview, bool>>>(), true,"Reservation"), Times.Once);
            _mockUnit.Verify(x => x.GuestReviewRepository.GetByIdAsync(Id,
                 It.IsAny<Expression<Func<GuestReview, bool>>>(), false,
                 GuestReviewHelper.AllGuestReviewIncludes()), Times.Once);
            #endregion
        }
        [Fact]
        public async Task Handle_WhenGuestReviewWithThisIdDoesNotExist_ThrowGuestReviewNotFoundException()
        {
            DefaultHttpContext context = new();
            var routeId = Guid.NewGuid().ToString();//random id veririk ki, not found versin.
            context.Request.RouteValues["id"] = routeId;
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);

            Guid Id = BaseHelper.GetIdFromRoute(_mockAccessor.Object);
            _mockUnit.Setup(x => x.GuestReviewRepository.GetByIdAsync(Id,
                It.IsAny<Expression<Func<GuestReview, bool>>>(), true,"Reservation"))
                .ReturnsAsync(_guestReviews.FirstOrDefault(x => x.Id == Id));

            _handler = new(_mockUnit.Object, _mapper, _mockAccessor.Object);

            Func<Task> act = async () => await _handler.Handle(new UpdateGuestReviewCommand(),
                It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<GuestReviewNotFoundException>();
            #region verifies
            _mockAccessor.Verify(x => x.HttpContext, Times.Exactly(2));// bir defe setup, bir defe de object de
            _mockUnit.Verify(x => x.GuestReviewRepository.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<Expression<Func<GuestReview, bool>>>(), true,"Reservation"), Times.Once());
            #endregion
        }
        [Fact]
        public async Task CheckExceptionsThenReturnGuestReview_WhenHostWithThisIdDoesNotExist_ThrowsHostNotFoundException()
        {
            DefaultHttpContext context = new();
            var routeId = _guestReviews.First().Id.ToString();//random id veririk ki, not found versin.
            context.Request.RouteValues["id"] = routeId;
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);

            //1 hostumuz var ancaq, hostun appuserId si birinci user in Id sine beraberdi ve _userId ise 2ci userin Id sine,
            //ona gore tapa bilmir.
            _mockUnit.Setup(x => x.GuestReviewRepository.GetByIdAsync(It.IsAny<Guid>(),
                   It.IsAny<Expression<Func<GuestReview, bool>>>(), true, "Reservation"))
                   .ReturnsAsync(_guestReviews.First());

            ClaimsIdentity claimIdentity = new(new[]
            {
                new Claim(ClaimTypes.NameIdentifier,_userId.ToString())
            });
            context.User = new ClaimsPrincipal(claimIdentity);
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);

            _mockUnit.Setup(x => x.HostRepository.GetSingleAsync(
                x => x.AppUserId == _userId, true, "AppUser"))
                .ReturnsAsync(_hosts.AppUserId != _userId ? null : _hosts);

            _handler = new(_mockUnit.Object, _mapper, _mockAccessor.Object);

            Func<Task> act = async () => await _handler.Handle(new UpdateGuestReviewCommand(), It.IsAny<CancellationToken>());

            await act.Should().ThrowAsync<HostNotFoundException>();
        }

        [Fact]
        public async Task CheckExceptionsThenReturnGuestReview_WhenReservationHostIdIsSameWithAuthenticatedHostIdIsNotSame_ThrowGuestReview_HostIdNotMatchedException()
        {
            //exception i yoxlamaq uchun yaradiriq
            Host host = new()
            {
                Id = Guid.NewGuid()
            };
            DefaultHttpContext context = new();
            var routeId = _guestReviews.First().Id.ToString();//random id veririk ki, not found versin.
            context.Request.RouteValues["id"] = routeId;
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);

            //1 hostumuz var ancaq, hostun appuserId si birinci user in Id sine beraberdi ve _userId ise 2ci userin Id sine,
            //ona gore tapa bilmir.
            _mockUnit.Setup(x => x.GuestReviewRepository.GetByIdAsync(It.IsAny<Guid>(),
                   It.IsAny<Expression<Func<GuestReview, bool>>>(), true, "Reservation"))
                   .ReturnsAsync(_guestReviews.First());

            ClaimsIdentity claimIdentity = new(new[]
            {
                new Claim(ClaimTypes.NameIdentifier,_userId.ToString())
            });
            context.User = new ClaimsPrincipal(claimIdentity);
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);

            ///------///
            _mockUnit.Setup(x => x.HostRepository.GetSingleAsync(
               It.IsAny<Expression<Func<Host,bool>>>(),false))
                .ReturnsAsync(host);// geriye donduyum host bashqadi, guest review in hostu amma bashqa. Exception i ele tuturuq
            _guestReviews.First().Host = _hosts;
            _guestReviews.First().Reservation = _reservations.First();
            _handler = new(_mockUnit.Object, _mapper, _mockAccessor.Object);
            Func<Task> act = async () => await _handler.Handle(new UpdateGuestReviewCommand(),
                It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<GuestReview_HostIdNotMatchedException>();
        }
    }
}
