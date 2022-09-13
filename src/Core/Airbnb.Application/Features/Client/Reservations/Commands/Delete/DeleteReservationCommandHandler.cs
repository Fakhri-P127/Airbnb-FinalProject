using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Domain.Entities.PropertyRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Reservations.Commands.Delete
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteReservationCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            Reservation reservation = await _unit.ReservationRepository.GetByIdAsync(request.Id, null);
            if (reservation is null) throw new ReservationNotFoundException(request.Id);
            await _unit.ReservationRepository.DeleteAsync(reservation);
            return await Task.FromResult(Unit.Value);
        }
    }
}
