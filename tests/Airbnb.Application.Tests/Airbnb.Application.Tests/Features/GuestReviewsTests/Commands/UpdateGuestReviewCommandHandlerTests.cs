using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.GuestReviews;
using Airbnb.Application.Features.Client.GuestReviews.Commands.Update;
using Airbnb.Application.Helpers;
using Airbnb.Application.Mapping;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq.Expressions;

namespace Airbnb.Application.Tests.Features.GuestReviewsTests.Commands
{
    public class UpdateGuestReviewCommandHandlerTests
    {
        private readonly Mock<IHttpContextAccessor> _mockAccessor;
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnit;
        private readonly UpdateGuestReviewCommandHandler _handler;
        private readonly List<GuestReview> _guestReviews;
        private readonly List<Reservation> _reservations;
        private readonly Host _hosts;
        private readonly List<AppUser> _users;
        public UpdateGuestReviewCommandHandlerTests()
        {
            _mockAccessor = new Mock<IHttpContextAccessor>();

            _mockUnit = new Mock<IUnitOfWork>();
            var mapperConfig = new MapperConfiguration(config =>
          config.AddProfile<GuestReviewMappings>());
            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateGuestReviewCommandHandler(_mockUnit.Object, _mapper, _mockAccessor.Object);
            _guestReviews = new Faker<GuestReview>()
                .RuleFor(x => x.Id, d => d.Random.Guid())
                .RuleFor(x => x.Text, f => f.Lorem.Word())
                .RuleFor(x => x.GuestScore, f => f.Random.Float(1, 5))
                .Generate(8);
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
            _users.First().Host = _hosts;
            _hosts.Reservations.Add(_reservations.First());
        }

      
        [Fact]
        public async Task Handle_WhenDataExistsWithGivenRouteId_UpdatesGuestReview()
        {
            DefaultHttpContext context = new();
            var routeId = _guestReviews.First().Id.ToString();
            context.Request.RouteValues["id"] = routeId;
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);
            UpdateGuestReviewCommand command = new() { Text = "updatedText", GuestScore = 4 };
            Guid Id = BaseHelper.GetIdFromRoute(_mockAccessor.Object);
            // vaxt qalsa bu setup lari yaxshi orqanize ele, tekrar tekrar yazilmasin.
            GuestReview updateGuestReview = _guestReviews.First(x => x.Id == Id);
            _mockUnit.Setup(x => x.GuestReviewRepository.GetByIdAsync(Id,
                It.IsAny<Expression<Func<GuestReview, bool>>>(), true))
                .ReturnsAsync(updateGuestReview);

            _mockUnit.Setup(x => x.GuestReviewRepository.Update(It.IsAny<GuestReview>(), true))
                .Callback<GuestReview, bool>((gReview, entityState) =>
                {
                    updateGuestReview = _mapper.Map<GuestReview>(command);
                });
            _mockUnit.Setup(x => x.GuestReviewRepository.GetByIdAsync(Id,
               It.IsAny<Expression<Func<GuestReview, bool>>>(), false, GuestReviewHelper.AllGuestReviewIncludes()))
               .ReturnsAsync(updateGuestReview);

            var result = await _handler.Handle(command, It.IsAny<CancellationToken>());

            updateGuestReview.Should().NotBeNull();
            updateGuestReview.Text.Should().Be(command.Text);
            updateGuestReview.GuestScore.Should().Be(command.GuestScore);

            #region verifies
            _mockAccessor.Verify(x=>x.HttpContext, Times.Exactly(2));
            _mockUnit.Verify(x => x.GuestReviewRepository.Update(It.IsAny<GuestReview>(), true), Times.Once());
            _mockUnit.Verify(x => x.GuestReviewRepository.GetByIdAsync(Id,
                 It.IsAny<Expression<Func<GuestReview, bool>>>(), true), Times.Once);
            _mockUnit.Verify(x => x.GuestReviewRepository.GetByIdAsync(Id,
                 It.IsAny<Expression<Func<GuestReview, bool>>>(), false,
                 GuestReviewHelper.AllGuestReviewIncludes()), Times.Once);
            #endregion
        }
        [Fact]
        public async Task Handle_WhenGuestReviewWithThisIdDoesNotExist_ThrowGuestReviewNotFoundException()
        {
            DefaultHttpContext context = new();
            var routeId = Guid.NewGuid().ToString();
            context.Request.RouteValues["id"] = routeId;
            _mockAccessor.Setup(x => x.HttpContext).Returns(context);

            Guid Id = BaseHelper.GetIdFromRoute(_mockAccessor.Object);
            _mockUnit.Setup(x => x.GuestReviewRepository.GetByIdAsync(Id,
                It.IsAny<Expression<Func<GuestReview, bool>>>(), true))
                .ReturnsAsync(_guestReviews.FirstOrDefault(x => x.Id == Id));

            Func<Task> act = async () => await _handler.Handle(new UpdateGuestReviewCommand(),
                It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<GuestReviewNotFoundException>();
            #region verifies
            _mockAccessor.Verify(x => x.HttpContext, Times.Exactly(2));// bir defe setup, bir defe de object de
            _mockUnit.Verify(x => x.GuestReviewRepository.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<Expression<Func<GuestReview, bool>>>(), true), Times.Once());
            #endregion
        }
    }
}
