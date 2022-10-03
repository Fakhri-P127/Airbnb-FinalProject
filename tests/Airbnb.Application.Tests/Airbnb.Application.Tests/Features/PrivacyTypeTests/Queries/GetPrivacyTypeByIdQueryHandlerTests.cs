using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetById;
using Airbnb.Application.Mapping;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace Airbnb.Application.Tests.Features.PrivacyTypeTests.Queries
{
    /// <summary>
    /// Get by id lerin hamisi eynidi deye bir bu bes eder mence. Vaxt chatsa belke yene yazdim idk
    /// </summary>
    public class GetPrivacyTypeByIdQueryHandlerTests
    {
        public readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnit;
        private List<PrivacyType> _privacyTypes;
        private readonly GetByIdPrivacyTypeQueryHandler _handler;
        private const string _existingId1 = "16bf214b-e396-40ca-b3e2-e6925398cf43";
        private const string _existingId2 = "e27b5ebe-aa34-4325-bb9e-2be0f8db8df7";
        private const string _nonExistingId = "f33a0fb2-418f-4538-bfcc-bb1eef571a51";
        public GetPrivacyTypeByIdQueryHandlerTests()
        {
            //_privacyTypes = new Faker<PrivacyType>()
            //   .RuleFor(x => x.Id, Guid.NewGuid)
            //   .RuleFor(x => x.Name, x => x.Lorem.Letter(5))
            //   .RuleFor(x => x.CreatedAt, x => x.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
            //   .RuleFor(x => x.ModifiedAt, x => x.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
            //   .RuleFor(x => x.IsDisplayed, true).Generate(5);
            _privacyTypes = new List<PrivacyType>()
            {
                new PrivacyType()
                {
                Id = Guid.Parse(_existingId1),
                Name = "existing 1"
                 },
                new PrivacyType()
                {
                 Id = Guid.Parse(_existingId2),
                 Name = "existing 2"
                }
                };
            _mockUnit = new Mock<IUnitOfWork>();
            var mapperConfig = new MapperConfiguration(config =>
            config.AddProfile<PrivacyTypeMappings>());
            _mapper = mapperConfig.CreateMapper();
            _handler = new GetByIdPrivacyTypeQueryHandler(_mockUnit.Object, _mapper);

        }

        [Theory]
        [InlineData(_existingId1)]
        [InlineData(_existingId2)]
        public async Task Handle_WhenGivingExistingIdInDatabase_ReturnsGivenData(Guid id)
        {
            _mockUnit.Setup(x => x.PrivacyTypeRepository.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<Expression<Func<PrivacyType, bool>>>(), false, "Properties"))
                .ReturnsAsync(_privacyTypes.FirstOrDefault(x => x.Id == id));

            var result = await _handler.Handle(new GetByIdPrivacyTypeQuery(id, null), It.IsAny<CancellationToken>());

            result.Should().NotBeNull();
            result.Should().BeOfType<PrivacyTypeResponse>();
        }
        [Fact]
        public async Task Handle_WhenGivingIdThatDoesNotExistInDatabase_ThrowsNotFoundException()
        {
            _ = Guid.TryParse(_nonExistingId, out Guid Id);
            _mockUnit.Setup(x => x.PrivacyTypeRepository.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<Expression<Func<PrivacyType, bool>>>(), false, "Properties"))
                .ReturnsAsync(_privacyTypes.FirstOrDefault(x=>x.Id == Id));

            Func<Task> act = async ()=> await _handler.Handle(new GetByIdPrivacyTypeQuery(Id, null), It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<PrivacyTypeNotFoundException>();
        }
    }
}
