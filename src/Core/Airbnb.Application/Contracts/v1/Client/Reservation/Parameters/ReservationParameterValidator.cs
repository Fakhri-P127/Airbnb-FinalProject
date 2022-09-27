using Airbnb.Domain.Enums.Reservations;
using FluentValidation;

namespace Airbnb.Application.Contracts.v1.Client.Reservation.Parameters
{
    public class ReservationParameterValidator : AbstractValidator<ReservationParameters>
    {
        public ReservationParameterValidator()
        {
            // setting min value to min enum value and max to max.
            RuleFor(x => x.Status)
                .GreaterThanOrEqualTo((int)Enum.GetValues<Enum_ReservationStatus>().FirstOrDefault())
                .LessThanOrEqualTo((int)Enum.GetValues<Enum_ReservationStatus>().LastOrDefault());

            #region default price validations
            RuleFor(x => x.MinTotalPrice).GreaterThanOrEqualTo(20).LessThanOrEqualTo(100000);
            RuleFor(x => x.MaxTotalPrice).GreaterThanOrEqualTo(20).LessThanOrEqualTo(100000);
            #endregion
            #region conditional price validations
            When(x => x.MinTotalPrice.HasValue, () =>
            {
                RuleFor(x => x.MaxTotalPrice).GreaterThan(x => x.MinTotalPrice.Value).LessThanOrEqualTo(100000);
            });
            #endregion

            #region Guid Id validations
            // bunlara eslinde ehtiyac yoxdu chunki model validation ishe dusherek tipleri yoxlayir amma yazdim yenede
            //When(x => x.PropertyId.HasValue, () =>
            //{
            //    RuleFor(x => x.PropertyId).Must(x => ValidateGuid(x.Value.ToString())).WithMessage("Id must be guid");
            //});
            //When(x => x.AppUserId.HasValue, () =>
            //{
            //RuleFor(x => x.AppUserId).Must(x=> ValidateGuid(x.Value.ToString()));
            //});
            //When(x => x.HostId.HasValue, () =>
            //{
            //    RuleFor(x => x.HostId).Must(x => ValidateGuid(x.Value.ToString()));
            //});
            #endregion

            #region default checkInAndOutDates
            RuleFor(x => x.MinCheckInDate).GreaterThanOrEqualTo(new DateTime(2010, 01, 01))
                .LessThanOrEqualTo(DateTime.Now);

            RuleFor(x => x.MaxCheckInDate)
               .GreaterThan(new DateTime(2010, 01, 01))
               .LessThanOrEqualTo(DateTime.Now.AddYears(2));

            RuleFor(x => x.MinCheckOutDate)
               .GreaterThanOrEqualTo(new DateTime(2010, 01, 02))
               .LessThanOrEqualTo(x => DateTime.Now.AddYears(2));

            RuleFor(x => x.MaxCheckOutDate)
                .GreaterThanOrEqualTo(new DateTime(2010,01,02))
                .LessThanOrEqualTo(x => DateTime.Now.AddYears(2));
            #endregion
            #region condtional validation for checkIn and checkOut dates
            When(x => x.MinCheckInDate.HasValue, () =>
            {
                RuleFor(x => x.MaxCheckInDate)
                .GreaterThan(x => x.MinCheckInDate.Value.Date.AddDays(1));
                //.LessThanOrEqualTo(DateTime.Now.AddYears(2));

                RuleFor(x => x.MinCheckOutDate)
                    .GreaterThanOrEqualTo(x => x.MinCheckInDate.Value.Date.AddDays(1));
                //.LessThanOrEqualTo(x => DateTime.Now.AddYears(2));

                RuleFor(x => x.MaxCheckOutDate)
                    .GreaterThanOrEqualTo(x => x.MinCheckInDate.Value.Date.AddDays(1));
                //.LessThanOrEqualTo(x => DateTime.Now.AddYears(2));
            });
            When(x => x.MaxCheckInDate.HasValue, () =>
            {
                RuleFor(x => x.MinCheckOutDate)
              .LessThanOrEqualTo(x => x.MaxCheckInDate.Value.Date.AddDays(1));

                RuleFor(x => x.MaxCheckOutDate)
                    .GreaterThanOrEqualTo(x => x.MaxCheckInDate.Value.Date.AddDays(1));
                    //.LessThanOrEqualTo(x => DateTime.Now.AddYears(2));
            });
            When(x => x.MinCheckOutDate.HasValue, () =>
            {
                RuleFor(x => x.MaxCheckOutDate)
                    .GreaterThanOrEqualTo(x => x.MinCheckOutDate.Value.Date.AddDays(1));
                //.LessThanOrEqualTo(x => DateTime.Now.AddYears(2));

                // tam deqiqlik uchun bu tipde yazmaq olar amma mence ehtiyac yoxdu
                //RuleFor(x => x.MaxCheckInDate).LessThanOrEqualTo(x => x.MinCheckOutDate.Value.AddDays(-1));
            });
            #endregion


        }
        public static bool ValidateGuid(string value)
        {
            return Guid.TryParse(value, out var result);
        }
    }
}
