using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.PropertyGroups;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Admin.PropertyGroups.Commands.Delete
{
    public class DeletePropertyGroupCommandHandler : IRequestHandler<DeletePropertyGroupCommand>
    {
        private readonly IUnitOfWork _unit;
   
        private readonly IWebHostEnvironment _env;

        public DeletePropertyGroupCommandHandler(IUnitOfWork unit, IWebHostEnvironment env)
        {
            _unit = unit;
            _env = env;
        }
        public async Task<Unit> Handle(DeletePropertyGroupCommand request, CancellationToken cancellationToken)
        {
            PropertyGroup propertyGroup = await _unit.PropertyGroupRepository.GetByIdAsync(request.Id, null,true);
            if (propertyGroup is null) throw new PropertyGroupNotFoundException();
            FileHelpers.FileDelete(_env.WebRootPath, "assets/images/PropertyGroupImages", propertyGroup.Image);
            await _unit.PropertyGroupRepository.DeleteAsync(propertyGroup);
            return await Task.FromResult(Unit.Value);
        }
    }
}
