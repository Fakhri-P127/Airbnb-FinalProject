using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PropertyGroups.Responses;
using Airbnb.Application.Exceptions.PropertyGroups;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Commands.Update
{
    public class UpdatePropertyGroupCommandHandler : IRequestHandler<UpdatePropertyGroupCommand, PostPropertyGroupResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public UpdatePropertyGroupCommandHandler(IUnitOfWork unit, IMapper mapper, IWebHostEnvironment env)
        {
            _unit = unit;
            _mapper = mapper;
            _env = env;
        }
        public async Task<PostPropertyGroupResponse> Handle(UpdatePropertyGroupCommand request, CancellationToken cancellationToken)
        {
            PropertyGroup propertyGroup = await _unit.PropertyGroupRepository.GetByIdAsync(request.Id, null);
            if (propertyGroup is null) throw new PropertyGroupNotFoundException();
            _unit.PropertyGroupRepository.Update(propertyGroup);
            _mapper.Map(request, propertyGroup);
            await ImageCheck(request, propertyGroup);
           
            await _unit.SaveChangesAsync();
            propertyGroup = await _unit.PropertyGroupRepository.GetByIdAsync(propertyGroup.Id, null);
               
            PostPropertyGroupResponse response = _mapper.Map<PostPropertyGroupResponse>(propertyGroup);
            return response;

        }
        private async Task ImageCheck(UpdatePropertyGroupCommand request, PropertyGroup propertyGroup)
        {
            if (request.Image is not null)
            {
                if (!request.Image.IsImageOkay(2))
                {
                    throw new PropertyGroupImageValidationException();
                }
                if (!string.IsNullOrWhiteSpace(propertyGroup.Image))
                    FileHelpers.FileDelete(_env.WebRootPath, "assets/images/PropertyGroupImages", propertyGroup.Image);
                propertyGroup.Image = await request.Image
                    .FileCreate(_env.WebRootPath, "assets/images/PropertyGroupImages");
            }
        }
    }
}
