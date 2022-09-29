using Airbnb.Application.Contracts.v1.Client.Reservation.Parameters;
using Airbnb.Domain.Enums.Reservations;
using FluentValidation;

namespace Airbnb.Application.Contracts.v1.Client.Host.Parameters
{
    public class HostParametersValidator:AbstractValidator<HostParameters>
    {
        public HostParametersValidator()
        {
            RuleFor(x => x.Status).GreaterThanOrEqualTo((int)Enum.GetValues<Enum_HostStatus>().FirstOrDefault())
                .LessThanOrEqualTo((int)Enum.GetValues<Enum_HostStatus>().LastOrDefault());

            //When(x => x.AppUserId.HasValue, () =>
            //{
            //    RuleFor(x => x.AppUserId).Must(x => ValidateGuid(x.Value.ToString()));
            //});
        }
        //public static bool ValidateGuid(string value)
        //{
        //    return Guid.TryParse(value, out var result);
        //}
    }
}
