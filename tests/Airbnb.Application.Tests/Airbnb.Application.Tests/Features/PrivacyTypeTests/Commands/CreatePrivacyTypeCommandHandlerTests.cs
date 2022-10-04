using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Parameters;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Create;
using Airbnb.Application.Mapping;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace Airbnb.Application.Tests.Features.PrivacyTypeTests.Commands
{
    public class CreatePrivacyTypeCommandHandlerTests
    {
        public readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnit;
        private List<PrivacyType> _privacyTypes;
        private readonly CreatePrivacyTypeCommandHandler _handler;
        private const string _existingId1 = "16bf214b-e396-40ca-b3e2-e6925398cf43";
        private const string _existingId2 = "e27b5ebe-aa34-4325-bb9e-2be0f8db8df7";
       
        public CreatePrivacyTypeCommandHandlerTests()
        {
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
            _handler = new CreatePrivacyTypeCommandHandler(_mockUnit.Object, _mapper);

        }

        [Fact]
        public async Task Handle_GivenValidCommand_CreatesNewPrivacyType()
        {
            // arrange
            CreatePrivacyTypeCommand command = new() { Name = "created000" };
            _mockUnit.Setup(x => x.PrivacyTypeRepository.GetAllAsync(x=>x.Name == command.Name,
                 It.IsAny<PrivacyTypeParameters>(), false))
                .ReturnsAsync(_privacyTypes.Where(x=>x.Name==command.Name).ToList());

            _mockUnit.Setup(x => x.PrivacyTypeRepository.AddAsync(It.IsAny<PrivacyType>()))
                .Callback<PrivacyType>((privType) => { _privacyTypes.Add(privType);
                });

            PrivacyType privacyType = new() { Name = command.Name };

            _mockUnit.Setup(x => x.PrivacyTypeRepository.GetByIdAsync(It.IsAny<Guid>(), null, false, "Properties"))
                .ReturnsAsync(privacyType);
            // act
            var result = await _handler.Handle(command, It.IsAny<CancellationToken>());
            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<PrivacyTypeResponse>();
            result.Name.Should().BeEquivalentTo(command.Name);
            _privacyTypes.Last().Name.Should().Be(privacyType.Name);
        }

        [Fact]
        public async Task Handle_GivenDuplicatePrivacyTypeName_ThrowsDuplicateNameException()
        {
            // arrange
            CreatePrivacyTypeCommand command = new() { Name = _privacyTypes.First().Name };
            _mockUnit.Setup(x => x.PrivacyTypeRepository.GetSingleAsync(x => x.Name == command.Name, false))
                .ReturnsAsync(_privacyTypes.FirstOrDefault(x => x.Name == command.Name));

            Func<Task> act = async()=> await _handler.Handle(command, It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<DuplicatePrivacyTypeNameValidationException>();
        }
    }
    
}
