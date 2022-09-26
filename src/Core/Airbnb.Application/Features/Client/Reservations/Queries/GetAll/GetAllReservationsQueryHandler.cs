using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Reservations.Queries.GetAll
{
    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, List<GetReservationResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public GetAllReservationsQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<GetReservationResponse>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            List<Reservation> reservations = await _unit.ReservationRepository
                .GetAllAsync(request.Expression,false, "PropertyReview", "GuestReview");
            List<GetReservationResponse> responses = _mapper.Map<List<GetReservationResponse>>(reservations);
            if (responses is null) throw new Exception("Internal server error");
            return responses;
        }
    }
}
