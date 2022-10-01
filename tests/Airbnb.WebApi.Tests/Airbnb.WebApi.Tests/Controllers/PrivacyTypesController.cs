using Airbnb.Application.Contracts.v1.Admin.PrivacyTypes.Responses;
using Airbnb.Application.Exceptions.Cities;
using Airbnb.Application.Exceptions.Countries;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Application.Features.Admin.PrivacyTypes.Commands.Create;
using Airbnb.Application.Features.Admin.PrivacyTypes.Queries.GetById;
using Airbnb.WebAPI.Controllers.v1.Admin;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Times = Moq.Times;

namespace Airbnb.WebApi.Tests.Controllers
{
    public class UnitTest1
    {
        private readonly PrivacyTypesController _sut;
        private readonly Mock<ISender> _mockRepo;
        private readonly PrivacyTypeResponse _response;
        //private  GetByIdPrivacyTypeQuery _query;
        public UnitTest1()
        {
            _mockRepo = new Mock<ISender>();
            _sut = new PrivacyTypesController(_mockRepo.Object);
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
        [Theory]
        [InlineData("f1e865bb-b75c-4300-899c-1cf007373119")]
        [InlineData("06dfff1e-5d9b-4b2d-a72b-a0d2e6c1ed93")]
        //[InlineData("2414151353151")]
        public async Task GetPrivacyTypeById_IfSuccessful_ReturnsResponse(string id)
        {
            //_query = ;
            bool result = Guid.TryParse(id, out Guid Id);
            if (!result) throw new Exception();
            _mockRepo.Setup(x => x.
             Send(It.IsAny<GetByIdPrivacyTypeQuery>(), CancellationToken.None))
                .ReturnsAsync(_response);
            var resultCntrl = await _sut.GetPrivacyTypeById(Id);
            resultCntrl.ShouldNotBeNull();
            resultCntrl.ShouldBeAssignableTo<OkObjectResult>();
            _mockRepo.Verify(x => x
            .Send(It.IsAny<GetByIdPrivacyTypeQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            _response.ShouldBeAssignableTo<PrivacyTypeResponse>();
            //var viewResult = Assert.IsType<OkObjectResult>(resultCntrl);
            //var privacytypes = Assert.IsType<PrivacyTypeResponse>(_response);

            //Assert.Equal(2, privacytypes);
        }

        [Fact]
        public async Task CreatePrivacyType_WhenSuccessfulyCreated_ReturnsOkResponse()
        {
            //CreatePrivacyTypeCommand privacyType = null;
            _mockRepo.Setup(x => x
            .Send(It.IsAny<CreatePrivacyTypeCommand>(), CancellationToken.None))
                .ReturnsAsync(new PrivacyTypeResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "salam",
                    PropertyCount = 0,
                    //CreatedAt=DateTime.Now,
                    //ModifiedAt=DateTime.Now,
                    //IsDisplayed = true
                });
            // _mockRepo.Setup(x => x
            //.Send(It.IsAny<CreatePrivacyTypeCommand>(), CancellationToken.None))
            //     .Callback<CreatePrivacyTypeCommand>(x=>privacyType=x);

            CreatePrivacyTypeCommand pType = new()
            {
                Name = "sagol"
            };
            var res = await _sut.CreatePrivacyType(pType);
            res.ShouldNotBeNull();
            _mockRepo.Verify(x => x
            .Send(It.IsAny<CreatePrivacyTypeCommand>(), CancellationToken.None),
            Times.Once);
            //privacyType.Name.ShouldBeEquivalentTo(pType.Name);
        }
    }
}