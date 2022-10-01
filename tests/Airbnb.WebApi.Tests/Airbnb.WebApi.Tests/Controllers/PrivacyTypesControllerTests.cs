using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Parameters;
using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Exceptions.Cities;
using Airbnb.Application.Exceptions.Countries;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Create;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Delete;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Update;
using Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetAll;
using Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetById;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.WebAPI.Controllers.v1.Admin;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Times = Moq.Times;

namespace Airbnb.WebApi.Tests.Controllers
{
    public class PrivacyTypesControllerTests
    {
        private readonly PrivacyTypesController _sut;
        private readonly Mock<ISender> _mockMediatr;
        private readonly PrivacyTypeResponse _response;
        private readonly Guid PrivacyTypeId = Guid.NewGuid();
        //private  GetByIdPrivacyTypeQuery _query;
        public PrivacyTypesControllerTests()
        {
            _mockMediatr = new Mock<ISender>();
            _sut = new PrivacyTypesController(_mockMediatr.Object);
            _response = new PrivacyTypeResponse
            {
                Id = Guid.Parse("f1e865bb-b75c-4300-899c-1cf007373119"),
                Name = "Salam",
                PropertyCount = 0,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                IsDisplayed = true
            };
            //_query = new GetByIdPrivacyTypeQuery(Guid.NewGuid());
        }
        [Fact]
        public async Task GetAllPrivacyTypes_IfNotEmpty_ReturnsListOfPrivacyTypeResponse()
        {
            // arrange
            _mockMediatr.Setup(x => x.Send(It.IsAny<GetAllPrivacyTypeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PrivacyTypeResponse>() { new PrivacyTypeResponse(), new PrivacyTypeResponse() });
            // act

            IActionResult result = await _sut.GetAllPrivacyTypes(It.IsAny<PrivacyTypeParameters>());

            // assert
            //result.Should().BeOfType<OkObjectResult>();
            List<PrivacyTypeResponse> response = result.Should().BeOfType<OkObjectResult>().Subject.Value as List<PrivacyTypeResponse>;
            response.Count.Should().BeGreaterThan(0);
            _mockMediatr.Verify(x => x.Send(It.IsAny<GetAllPrivacyTypeQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async Task GetAllPrivacyTypes_IfEmpty_ReturnsEmptyArray()
        {
            // arrange
            _mockMediatr.Setup(x => x.Send(It.IsAny<GetAllPrivacyTypeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PrivacyTypeResponse>());
            // act
            IActionResult result = await _sut.GetAllPrivacyTypes(It.IsAny<PrivacyTypeParameters>());
            // assert
            List<PrivacyTypeResponse> response = result.Should().BeOfType<OkObjectResult>().Subject.Value as List<PrivacyTypeResponse>;
            response.Count.Should().Be(0);
            _mockMediatr.Verify(x => x.Send(It.IsAny<GetAllPrivacyTypeQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(8)]
        public async Task GetAllPrivacyTypes_GivenPageSize_ReturnsTheSameAmountOfPrivacyTypes(int pageSize)
        {
            // arrange
            List<PrivacyTypeResponse> returnedResponse = new();
            for (int i = 0; i < pageSize; i++)
            {
                PrivacyTypeResponse ptResponse = new()
                {
                    Id = Guid.NewGuid(),
                    Name = $"salam{i}",
                    IsDisplayed = true
                };
                returnedResponse.Add(ptResponse);
            }
            _mockMediatr.Setup(x => x.Send(It.IsAny<GetAllPrivacyTypeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedResponse);
            // act
            PrivacyTypeParameters parameters = new()
            {
                PageSize = pageSize
            };
            IActionResult result = await _sut.GetAllPrivacyTypes(parameters);
            // assert
            List<PrivacyTypeResponse> response = result.Should().BeOfType<OkObjectResult>().Subject.Value as List<PrivacyTypeResponse>;
            response.Count.Should().Be(pageSize);// default pagesize
            _mockMediatr.Verify(x => x.Send(It.IsAny<GetAllPrivacyTypeQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async Task GetPrivacyTypeById_IfSuccessful_ReturnsPrivacyTypeResponse()
        {
            // arrange
            _mockMediatr.Setup(x => x.
             Send(It.IsAny<GetByIdPrivacyTypeQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new PrivacyTypeResponse());
            // act
            IActionResult result = await _sut.GetPrivacyTypeById(It.IsAny<Guid>());
            //assert
            result.Should().BeOfType<OkObjectResult>();
            _mockMediatr.Verify(x => x
            .Send(It.IsAny<GetByIdPrivacyTypeQuery>(), It.IsAny<CancellationToken>()), Times.Once());
        }
       
        [Fact]
        public async Task CreatePrivacyType_WhenSuccessfulyCreated_ReturnsOkResponse()
        {
            //arrange
            _mockMediatr.Setup(x => x
            .Send(It.IsAny<CreatePrivacyTypeCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PrivacyTypeResponse());
            //act
            IActionResult result = await _sut.CreatePrivacyType(It.IsAny<CreatePrivacyTypeCommand>());
            //assert
            result.Should().BeOfType<CreatedAtActionResult>();
            _mockMediatr.Verify(x => x
            .Send(It.IsAny<CreatePrivacyTypeCommand>(), It.IsAny<CancellationToken>()),
            Times.Once);
        }

        [Fact]
        public async Task CreatePrivacyType_WhenValidCommand_ReturnsPrivacyTypeWithSameValues()
        {
            //arrange
           
            string privacyTypeName = "Full house";
            CreatePrivacyTypeCommand command = new() { Name = privacyTypeName };

            _mockMediatr.Setup(x => x
            .Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PrivacyTypeResponse() { Id = PrivacyTypeId, Name = privacyTypeName });
            //act
            IActionResult result = await _sut.CreatePrivacyType(command);
            //assert
            var response = result.Should().BeOfType<CreatedAtActionResult>().Subject.Value as PrivacyTypeResponse;
            response.Name.Should().BeEquivalentTo(command.Name);

            _mockMediatr.Verify(x => x
            .Send(It.IsAny<CreatePrivacyTypeCommand>(), It.IsAny<CancellationToken>()),
            Times.Once);
        }

        [Fact]
        public async Task UpdatePrivacyType_GivenValidUpdateCommand_UpdatesPrivacyType()
        {
            //arrange
            var command = new UpdatePrivacyTypeCommand
            {
                Name = "updatedName"
            };
            _mockMediatr.Setup(x => x.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PrivacyTypeResponse() { Name = command.Name});
            //act
            IActionResult result = await _sut.UpdatePrivacyType(command);
            //assert
            var response = result.Should().BeOfType<OkObjectResult>().Subject.Value as PrivacyTypeResponse;
            response.Name.Should().BeEquivalentTo(command.Name);
            _mockMediatr.Verify(x => x
            .Send(It.IsAny<UpdatePrivacyTypeCommand>(), It.IsAny<CancellationToken>()),
            Times.Once);
        }
        [Fact]
        public async Task DeletePrivacyType_WhenGivenValidId_ReturnsNoContent()
        {
            _mockMediatr.Setup(x => x.Send(new DeletePrivacyTypeCommand(PrivacyTypeId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);
            IActionResult result = await _sut.DeletePrivacyType(PrivacyTypeId);

            result.Should().BeOfType<NoContentResult>();
        }
    }
}