using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Parameters;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetAll;
using Airbnb.Application.Mapping;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace Airbnb.Application.Tests.Features.PrivacyTypeTests.Queries
{
    public class GetAllPrivacyTypesQueryHandlerTests
    {
        public readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnit;
        private List<PrivacyType> _privacyTypes;
        private readonly GetAllPrivacyTypeQueryHandler _handler;
        public GetAllPrivacyTypesQueryHandlerTests()
        {
            _privacyTypes = new Faker<PrivacyType>()
               .RuleFor(x => x.Id, Guid.NewGuid)
               .RuleFor(x => x.Name, x => x.Lorem.Letter(5))
               .RuleFor(x => x.CreatedAt, x => x.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
               .RuleFor(x => x.ModifiedAt, x => x.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
               .RuleFor(x => x.IsDisplayed, true).Generate(5);
     
            _mockUnit = new Mock<IUnitOfWork>();
            var mapperConfig = new MapperConfiguration(config => config.AddProfile<PrivacyTypeMappings>());
            _mapper = mapperConfig.CreateMapper();
            _handler = new GetAllPrivacyTypeQueryHandler(_mapper, _mockUnit.Object);
        }

        [Fact]
        public async Task GetAllPrivacyTypesQueryHandler_WhenDataExists_ReturnsListOfPrivacyTypes()
        {
            // arrange
            _mockUnit.Setup(x => x.PrivacyTypeRepository.GetAllAsync(It.IsAny<Expression<Func<PrivacyType, bool>>>(),
                It.IsAny<PrivacyTypeParameters>(), false, "Properties")).ReturnsAsync(_privacyTypes);
            GetAllPrivacyTypeQuery query = new GetAllPrivacyTypeQuery(new PrivacyTypeParameters(), null);

            //act
            var result = await _handler
                .Handle(query, CancellationToken.None);
            //assert
            result.Should().BeOfType<List<PrivacyTypeResponse>>();
            result.Count.Should().Be(_privacyTypes.Count);
        }
        [Fact]
        public async Task GetAllPrivacyTypesQueryHandler_WhenNoDataExists_ReturnsEmpty()
        {
            _mockUnit.Setup(x => x.PrivacyTypeRepository.GetAllAsync(It.IsAny<Expression<Func<PrivacyType, bool>>>(),
                It.IsAny<PrivacyTypeParameters>(), false)).ReturnsAsync(new List<PrivacyType>());

            var result = await _handler
                .Handle(new GetAllPrivacyTypeQuery(new PrivacyTypeParameters(), null), It.IsAny<CancellationToken>());

            result.Should().BeOfType<List<PrivacyTypeResponse>>();
            result.Count.Should().Be(0);
        }
    }
}
