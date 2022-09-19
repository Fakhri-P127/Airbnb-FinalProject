using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Application.Exceptions.Properties;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Airbnb.Application.Features.Client.Properties.Commands.Delete
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand>
    {
        private readonly IUnitOfWork _unit;
        private readonly IWebHostEnvironment _env;

        public DeletePropertyCommandHandler(IUnitOfWork unit,IWebHostEnvironment env)
        {
            _unit = unit;
            _env = env;
        }
        public async Task<Unit> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            Property property = await _unit.PropertyRepository.GetByIdAsync(request.Id, null,PropertyHelper.AllPropertyIncludes());
            if (property is null) throw new PropertyNotFoundException();

            property.PropertyImages.ForEach(image =>
            {
                FileHelpers.FileDelete(_env.WebRootPath, "assets/images/PropertyImages", image.Name);
            });
            await _unit.PropertyRepository.DeleteAsync(property);
            return await Task.FromResult(Unit.Value);
        }
    }
}
