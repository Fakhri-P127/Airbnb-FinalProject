using FluentValidation;

namespace Airbnb.Application.Features.Client.Properties.Commands.Update
{
    public class UpdatePropertyCommandValidator:AbstractValidator<UpdatePropertyCommand>
    {
        public UpdatePropertyCommandValidator()
        {
            RuleFor(x => x.Price).GreaterThanOrEqualTo(10).LessThanOrEqualTo(100000);
           
            RuleFor(x => x.MinNightCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(60);
            RuleFor(x => x.MaxGuestCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            RuleFor(x => x.MaxNightCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(60);
            RuleFor(x => x.BathroomCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            RuleFor(x => x.BedroomCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            RuleFor(x => x.BedCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);

            RuleFor(x => x.Title).Length(3, 100);
            RuleFor(x => x.Description).Length(5, 1000);
            RuleFor(x => x.Street).Length(3, 50);
            RuleFor(x => x.CityId).Must(x=>x.Value.GetType() == typeof(Guid)).WithMessage("type must be guid");

            //RuleForEach(x => x.PropertyAmenities);
            //RuleForEach(x => x.PropertyImages);
        }
    }
}
