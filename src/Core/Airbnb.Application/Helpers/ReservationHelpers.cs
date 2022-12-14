using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Reservation.Responses;
using Airbnb.Application.Exceptions.Reservations;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;

namespace Airbnb.Application.Helpers
{
    public static class ReservationHelpers
    {
        public async static Task<PostReservationResponse> ReturnResponse(Reservation reservation,IUnitOfWork _unit,IMapper _mapper)
        {
            reservation = await _unit.ReservationRepository.GetByIdAsync(reservation.Id, null);
            PostReservationResponse response = _mapper.Map<PostReservationResponse>(reservation);
            //if (response is null) throw new Exception("Internal server error");
            return response;
        }
        public static void CheckOutDateValidationChecker(Property property, int reservedDays)
        {
            if (property.MinNightCount > reservedDays)
                throw new ReservationMinNightValidationException(property.MinNightCount
                    , reservedDays);
            if (property.MaxNightCount < reservedDays)
                throw new ReservationMaxNightValidationException(property.MaxNightCount, reservedDays);
        }

        public static void CalculatePrice(Reservation reservation, int reservedDays)
        {
            // guest 2 den choxdusa onda her birine gore price artir. Saytda bele idi
            int totalGuests = reservation.AdultCount + reservation.ChildCount;
            if (totalGuests > 2)
            {
                int PricePerDay = (int)(reservation.Property.Price * reservedDays);
                int PricePerGuest = (int)(PricePerDay * 0.2);
                reservation.PricePerDay = PricePerDay + (totalGuests - 2) * PricePerGuest;
            }
            else
            {
                reservation.PricePerDay = (int)(reservation.Property.Price * reservedDays);
            }
            // qiymetin 10 % i service fee di, random olaraq sechmishem
            reservation.ServiceFee = (int)(reservation.PricePerDay * 0.1);
            reservation.TotalPrice = reservation.PricePerDay + reservation.ServiceFee;
        }
        public static string ChangeStatusToString(int status)
        {
            string statusStr;
            switch (status)
            {
                case 1:
                    statusStr = "Upcoming";
                    return statusStr;
                case 2:
                    statusStr = "Arriving soon";
                    return statusStr;
                case 3:
                    statusStr = "Currently hosting";
                    return statusStr;
                case 4:
                    statusStr = "Checking out";
                    return statusStr;
                case 5:
                    statusStr = "Reservation finished";
                    return statusStr;
                case 6:
                    statusStr = "Reservation cancelled";
                    return statusStr;
                default:
                    statusStr = "idk man";
                    return statusStr;
            }
        }
    }
}
