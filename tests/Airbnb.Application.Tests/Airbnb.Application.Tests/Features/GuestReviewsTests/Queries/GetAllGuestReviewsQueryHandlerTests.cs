using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Parameters;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Features.Client.GuestReviews.Queries.GetAll;
using Airbnb.Application.Helpers;
using Airbnb.Application.Mapping;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace Airbnb.Application.Tests.Features.GuestReviewsTests.Queries
{
    public class GetAllGuestReviewsQueryHandlerTests
    {
        //private readonly Mock<CustomUserManager<AppUser>> _mockUserManager;
        private readonly Mock<IUnitOfWork> _mockUnit;
        private readonly IMapper _mapper;
        private readonly GetAllGuestReviewsQueryHandler _handler;
        private List<GuestReview> _guestReviews;

        public GetAllGuestReviewsQueryHandlerTests()
        {
            _guestReviews = new Faker<GuestReview>()
                .RuleFor(x => x.Id, Guid.NewGuid())
                .RuleFor(x => x.Text, f => f.Lorem.Word())
                .RuleFor(x => x.GuestScore, f => f.Random.Float(1, 5))
                .Generate(8);
            var mapperConfig = new MapperConfiguration(config => config.AddProfile<GuestReviewMappings>());
            _mapper = mapperConfig.CreateMapper();
            _mockUnit = new Mock<IUnitOfWork>();
            //_mockUserManager = new Mock<CustomUserManager<AppUser>>();

            _handler = new(_mockUnit.Object, _mapper, null);// null vere bilerem chunki yoxlamiram Expression i
        }

        [Fact]
        public async Task Handle_WhenDataExists_ReturnsListOfGuestReviews()
        {
            // arrange
            _mockUnit.Setup(x => x.GuestReviewRepository.GetAllAsync(It.IsAny<Expression<Func<GuestReview, bool>>>(),
                It.IsAny<GuestReviewParameters>(), false, GuestReviewHelper.AllGuestReviewIncludes()))
                .ReturnsAsync(_guestReviews);

            GetAllGuestReviewsQuery query = new(new GuestReviewParameters(), null);
            //act
            var result = await _handler
                .Handle(query, CancellationToken.None);
            //assert
            result.Should().BeOfType<List<GuestReviewResponse>>();
            result.Count.Should().Be(_guestReviews.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]// bunlarin sayida bogusla generate etdiyimizden az olmalidi ki error vermesin,
        [InlineData(5)]// ona da fikir vermek lazimdi.
        public async Task Handle_WhenGivenParameters_ResponseReturnsWithGivenPageSizeCount(int pageSize)
        {
            GuestReviewParameters parameters = new() { PageSize = pageSize };

            // arrange
            _mockUnit.Setup(x => x.GuestReviewRepository.GetAllAsync(It.IsAny<Expression<Func<GuestReview, bool>>>(),
                It.IsAny<GuestReviewParameters>(), false, GuestReviewHelper.AllGuestReviewIncludes()))
                .ReturnsAsync(_guestReviews.Take(parameters.PageSize).ToList());

            GetAllGuestReviewsQuery query = new(parameters, null);
            //act
            var result = await _handler
                .Handle(query, CancellationToken.None);
            //assert
            result.Should().BeOfType<List<GuestReviewResponse>>();
            result.Count.Should().Be(pageSize);
        }
    }
}
