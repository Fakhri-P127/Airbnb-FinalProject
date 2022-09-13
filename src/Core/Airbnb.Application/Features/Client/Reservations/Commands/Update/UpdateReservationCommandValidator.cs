using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Reservations.Commands.Update
{
    public class UpdateReservationCommandValidator:AbstractValidator<UpdateReservationCommand>
    {
        public UpdateReservationCommandValidator()
        {
            RuleFor(x => x.CheckInDate).GreaterThanOrEqualTo(DateTime.UtcNow.Date.AddDays(1))
               .LessThanOrEqualTo(DateTime.UtcNow.AddYears(1));
            // Checkout date deki validation lari default deyerlere uygun olaraq burda yazdim
            // ve esas yoxlamalar command da olur. Yeni eger host default max ve min night deyerlerini
            // deyishibse onda o yoxlamalar command da aparilacaq.
            RuleFor(x => x.CheckOutDate).GreaterThanOrEqualTo(x => x.CheckInDate.Value.Date.AddDays(1))
                .LessThanOrEqualTo(x => x.CheckInDate.Value.Date.AddDays(60));
            RuleFor(x => x.AdultCount).GreaterThanOrEqualTo(1).NotNull();
            RuleFor(x => x.ChildCount).GreaterThanOrEqualTo(0).NotNull();
            RuleFor(x => x.InfantCount).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.PetCount).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5).NotNull();
          
        }
    }
}
