using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Application.Exceptions.PrivacyTypes;
using Airbnb.Application.Exceptions.PropertyGroups;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Commands.Create
{
    public class CreatePropertyGroupCommandHandler : IRequestHandler<CreatePropertyGroupCommand, PostPropertyGroupResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CreatePropertyGroupCommandHandler(IUnitOfWork unit, IMapper mapper, IWebHostEnvironment env)
        {
            _unit = unit;
            _mapper = mapper;
            _env = env;
        }
        public async Task<PostPropertyGroupResponse> Handle(CreatePropertyGroupCommand request, CancellationToken cancellationToken)
        {
            if (await _unit.PropertyGroupRepository.GetSingleAsync(x => x.Name == request.Name) is not null)
                throw new DuplicatePropertyGroupNameValidationException();
            PropertyGroup propertyGroup = new(){ Name = request.Name};
            await ImageCheck(request, propertyGroup);
            await _unit.PropertyGroupRepository.AddAsync(propertyGroup);
            return await PropertyGroupHelper.ReturnResponse(propertyGroup, _unit, _mapper);
        }
        private async Task ImageCheck(CreatePropertyGroupCommand request, PropertyGroup propertyGroup)
        {
            if (request.Image is null) throw new PropertyGroupImageValidationException()
            {
                ErrorMessage = "Image can't be empty. Please insert a photo"
            };

            if (!request.Image.IsImageOkay(2))
                throw new PropertyGroupImageValidationException();

            propertyGroup.Image = await request.Image
                .FileCreate(_env.WebRootPath, "assets/images/PropertyGroupImages");

        }
    }
}
