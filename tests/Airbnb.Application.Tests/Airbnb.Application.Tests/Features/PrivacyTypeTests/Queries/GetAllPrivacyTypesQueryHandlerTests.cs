using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetAll;
using Airbnb.Application.Mapping;
using AutoMapper;
using Moq;

namespace Airbnb.Application.Tests.Features.PrivacyTypeTests.Queries
{
    public class GetAllPrivacyTypesQueryHandlerTests
    {
        public readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnit;
        public GetAllPrivacyTypesQueryHandlerTests()
        {
            _mockUnit = new Mock<IUnitOfWork>();
            var mapperConfig = new MapperConfiguration(config =>
            config.AddProfile<PrivacyTypeMappings>());
            _mapper = mapperConfig.CreateMapper();

        }

        [Fact]
        public async Task GetAllPrivacyTypes_when_axsham()
        {
            var handler = new GetAllPrivacyTypeQueryHandler(_mapper, _mockUnit.Object);
            
            var result = handler.Handle()
        }
    }
}
