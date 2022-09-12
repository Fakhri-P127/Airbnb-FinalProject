using Airbnb.Application.Common.Interfaces;
using Airbnb.Domain.Entities.PropertyRelated;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Reservations.Commands.Create
{
    public class CreateReservationCommandValidator:AbstractValidator<CreateReservationCommand>
    {   
        public CreateReservationCommandValidator()
        {
            RuleFor(x=>x.CheckInDate).GreaterThanOrEqualTo(DateTime.UtcNow.Date.AddDays(1))
                .LessThanOrEqualTo(DateTime.UtcNow.AddYears(1)).NotEmpty();
            // Checkout date deki validation lari default deyerlere uygun olaraq burda yazdim
            // ve esas yoxlamalar command da olur. Yeni eger host default max ve min night deyerlerini
            // deyishibse onda o yoxlamalar command da aparilacaq.
            RuleFor(x=>x.CheckOutDate).GreaterThanOrEqualTo(x=>x.CheckInDate.Date.AddDays(1))
                .LessThanOrEqualTo(x=>x.CheckInDate.Date.AddDays(60)).NotEmpty();
            RuleFor(x => x.AdultCount).GreaterThanOrEqualTo(1).NotNull();
            RuleFor(x => x.ChildCount).GreaterThanOrEqualTo(0).NotNull();
            RuleFor(x => x.InfantCount).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.PetCount).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5).NotNull();
            RuleFor(x => x.PropertyId).NotEmpty();
            RuleFor(x => x.AppUserId).NotEmpty();
            RuleFor(x => x.HostId).NotEmpty();
   
        }
    }
}
