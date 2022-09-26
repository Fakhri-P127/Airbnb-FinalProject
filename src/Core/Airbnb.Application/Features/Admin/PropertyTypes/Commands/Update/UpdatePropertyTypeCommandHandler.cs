using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Admin.PropertyTypes.Responses;
using Airbnb.Application.Exceptions.PropertyTypes;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Admin.PropertyTypes.Commands.Update
{
    public class UpdatePropertyTypeCommandHandler : IRequestHandler<UpdatePropertyTypeCommand, PostPropertyTypeResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UpdatePropertyTypeCommandHandler(IUnitOfWork unit, IMapper mapper,IHttpContextAccessor accessor)
        {
            _unit = unit;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<PostPropertyTypeResponse> Handle(UpdatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            Guid Id = BaseHelper.GetIdFromRoute(_accessor);
            PropertyType propertyType = await _unit.PropertyTypeRepository.GetByIdAsync(Id, null,true);
            if (propertyType is null) throw new PropertyTypeNotFoundException();
            _unit.PropertyTypeRepository.Update(propertyType, false);
            _mapper.Map(request, propertyType);
            await _unit.SaveChangesAsync();
            return await PropertyTypeHelper.ReturnResponse(propertyType, _unit, _mapper);
        }
    }
}
