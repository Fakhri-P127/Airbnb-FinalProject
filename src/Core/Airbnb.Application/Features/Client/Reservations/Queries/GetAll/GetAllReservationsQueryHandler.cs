using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using LinqKit;
using MediatR;

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
            ExpressionStarter<Reservation> filters = FilterRequest(request);
            List<Reservation> reservations = await _unit.ReservationRepository
                .GetAllAsync(filters,request.Parameters, false, "PropertyReview", "GuestReview");
            List<GetReservationResponse> responses = _mapper.Map<List<GetReservationResponse>>(reservations);
            //if (!responses.Any()) throw new Exception("Internal server error");
            return responses;
        }

        private static ExpressionStarter<Reservation> FilterRequest(GetAllReservationsQuery request)
        {
            ExpressionStarter<Reservation> filters = PredicateBuilder.New<Reservation>(true);
            if (request.Parameters.Status.HasValue) filters = filters
                    .And(x => x.Status == request.Parameters.Status);

            if (request.Parameters.HostId.HasValue) filters = filters
                    .And(x => x.HostId == request.Parameters.HostId);
            if (request.Parameters.AppUserId.HasValue) filters = filters
                    .And(x => x.AppUserId == request.Parameters.AppUserId);
            if (request.Parameters.PropertyId.HasValue) filters = filters
                    .And(x => x.PropertyId == request.Parameters.PropertyId);

            if (request.Parameters.MinCheckInDate.HasValue) filters = filters
                    .And(x => x.CheckInDate >= request.Parameters.MinCheckInDate);
            if (request.Parameters.MaxCheckInDate.HasValue) filters = filters
                    .And(x => x.CheckInDate <= request.Parameters.MaxCheckInDate);
            if (request.Parameters.MinCheckOutDate.HasValue) filters = filters
                    .And(x => x.CheckOutDate >= request.Parameters.MinCheckOutDate);
            if (request.Parameters.MaxCheckOutDate.HasValue) filters = filters
                    .And(x => x.CheckOutDate <= request.Parameters.MaxCheckOutDate);

            if (request.Parameters.MinTotalPrice.HasValue) filters = filters
                    .And(x => x.TotalPrice >= request.Parameters.MinTotalPrice);
            if (request.Parameters.MaxTotalPrice.HasValue) filters = filters
                    .And(x => x.TotalPrice <= request.Parameters.MaxTotalPrice);
            
            return ExpressionHelpers<Reservation>.FilteredPredicateOrIfNoFilterReturnNull(filters);
        }
    }
}
