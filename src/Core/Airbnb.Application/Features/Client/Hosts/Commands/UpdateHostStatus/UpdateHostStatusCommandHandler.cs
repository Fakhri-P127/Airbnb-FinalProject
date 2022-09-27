using Airbnb.Application.Common.Interfaces;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Enums.Reservations;
using MediatR;

namespace Airbnb.Application.Features.Client.Hosts.Commands.UpdateHostStatus
{
    public class UpdateHostStatusCommandHandler : IRequestHandler<UpdateHostStatusCommand>
    {
        private readonly IUnitOfWork _unit;

        public UpdateHostStatusCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(UpdateHostStatusCommand request, CancellationToken cancellationToken)
        {
            // IsSuperHost default olaraq false oldugu uchun 5e qeder olanlari goturmurem,
            // ekstra boshuna data gelmesin
            List<Host> hosts = await _unit.HostRepository.GetAllAsync(x => x.Reservations.Count > 5,true,
                "Reservations");
            if (hosts is null || !hosts.Any()) return await Task.FromResult(Unit.Value);
            hosts.ForEach(host =>
            {
                _unit.HostRepository.Update(host, false);
                if (host.Reservations.Count >= 6 && host.Reservations.Count <= 10) host.Status = 
                    (int)Enum_HostStatus.ExpertHost;
                if (host.Reservations.Count > 10) host.Status = (int)Enum_HostStatus.SuperHost;
            });
            await _unit.SaveChangesAsync();
            return await Task.FromResult(Unit.Value);
        }
    }
}
