using Airbnb.Application.Common.Interfaces;
using FluentValidation;

namespace Airbnb.Application.Features.Client.Reservations.Commands.Create
{
    public class CreateReservationCommandValidator:AbstractValidator<CreateReservationCommand>
    {
        private readonly IUnitOfWork _unit;

        public CreateReservationCommandValidator(IUnitOfWork unit)
        {
            _unit = unit;
            
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

            RuleFor(x => x.PropertyId).NotEmpty().MustAsync(async (id, cancellationToken) =>
            {
                bool propertyExists = await _unit.PropertyRepository.GetByIdAsync(id, null) is not null;
                return propertyExists;
            }).WithMessage("Property with this Id doesn't exist").WithErrorCode("404");
        
            //RuleFor(x => x.AppUserId).NotEmpty();//action filterde yoxlanilir
            //RuleFor(x => x.HostId).NotEmpty();
            //// ichinden appuserId ni goturmeliydim deye obshi class a rulefor verdim.
            //RuleFor(x => x).MustAsync(async (x, cancellationToken) =>
            //{
            //    Host host = await _unit.HostRepository.GetByIdAsync(x.HostId, null);
            //    if (host is null) return false;
            //    if(host.AppUserId == x.AppUserId) throw new Reservation_CantReserveYourOwnPropertyException();
            //    return true;
            //}).WithMessage("Host with this Id doesn't exist");
        }
    }
}
