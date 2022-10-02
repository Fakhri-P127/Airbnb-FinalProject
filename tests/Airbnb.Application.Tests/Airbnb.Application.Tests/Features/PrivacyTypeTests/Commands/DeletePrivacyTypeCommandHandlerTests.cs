using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Parameters;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Create;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Delete;
using Airbnb.Application.Mapping;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Tests.Features.PrivacyTypeTests.Commands
{
    public class DeletePrivacyTypeCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnit;
        private List<PrivacyType> _privacyTypes;
        private readonly DeletePrivacyTypeCommandHandler _handler;
        private const string _existingId1 = "16bf214b-e396-40ca-b3e2-e6925398cf43";
        private const string _existingId2 = "e27b5ebe-aa34-4325-bb9e-2be0f8db8df7";
        private const string _nonExistingId = "f33a0fb2-418f-4538-bfcc-bb1eef571a51";
        public DeletePrivacyTypeCommandHandlerTests()
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
         
            _handler = new DeletePrivacyTypeCommandHandler(_mockUnit.Object);

        }

        [Fact]
        public async Task Handle_GivenExistingId_DeletesNewPrivacyType()
        {
            // arrange
            _ = Guid.TryParse(_existingId2, out Guid Id);
            _mockUnit.Setup(x => x.PrivacyTypeRepository.GetByIdAsync(Id, null, true))
                .ReturnsAsync(_privacyTypes.FirstOrDefault(x=>x.Id == Id));

            PrivacyType? privacyType = _privacyTypes.First(x => x.Id == Id);

            _mockUnit.Setup(x => x.PrivacyTypeRepository.DeleteAsync(privacyType))
                .Callback<PrivacyType>(priv=>_privacyTypes.Remove(priv));
            DeletePrivacyTypeCommand request = new(Guid.Parse(_existingId2));
            //act
            var result = await _handler.Handle(request, It.IsAny<CancellationToken>());
            //assert
            result.Should().BeOfType<MediatR.Unit>();
            _privacyTypes.Should().NotContain(privacyType);
        }
        [Fact]
        public async Task Handle_GivenNonExistingId_ThrowsNotFoundException()
        {
            // arrange
            _ = Guid.TryParse(_nonExistingId, out Guid Id);
            _mockUnit.Setup(x => x.PrivacyTypeRepository.GetByIdAsync(Id, null, true))
                .ReturnsAsync(_privacyTypes.FirstOrDefault(x => x.Id == Id));


            //act
            Func<Task> act = async()=> await _handler.Handle(new DeletePrivacyTypeCommand(It.IsAny<Guid>()), It.IsAny<CancellationToken>());
            await act.Should().ThrowAsync<PrivacyTypeNotFoundException>();
            //assert
           
        }
    }
}
