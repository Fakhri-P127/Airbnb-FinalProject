using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Client.Reservations.Queries.GetById;
public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, GetReservationResponse>
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;

    public GetReservationByIdQueryHandler(IUnitOfWork unit, IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }
    public async Task<GetReservationResponse> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        Reservation reservation = await _unit.ReservationRepository
                .GetByIdAsync(request.Id,request.Expression,false, "PropertyReview", "GuestReview");
        if (reservation is null) throw new ReservationNotFoundException(request.Id);

        GetReservationResponse responses = _mapper.Map<GetReservationResponse>(reservation);
        //if (!responses.Any()) throw new Exception("Internal server error");
        return responses;
    }
}
