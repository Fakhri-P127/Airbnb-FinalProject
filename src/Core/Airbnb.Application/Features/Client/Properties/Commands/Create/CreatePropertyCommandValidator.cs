using FluentValidation;

namespace Airbnb.Application.Features.Client.Properties.Commands.Create
{
    public class CreatePropertyCommandValidator:AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(x => x.Price).GreaterThanOrEqualTo(10).LessThanOrEqualTo(100000).NotEmpty();
            RuleFor(x => x.AirCoverId).NotEmpty();
            RuleFor(x => x.HostId).NotEmpty();
            RuleFor(x => x.CancellationPolicyId).NotEmpty();
            RuleFor(x => x.PrivacyTypeId).NotEmpty();
            RuleFor(x => x.PropertyGroupId).NotEmpty();
            RuleFor(x => x.PropertyTypeId).NotEmpty();

            RuleFor(x => x.MinNightCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(60).NotEmpty();
            RuleFor(x => x.MaxGuestCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).NotEmpty();
            RuleFor(x => x.MaxNightCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(60);
            RuleFor(x => x.BathroomCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).NotEmpty();
            RuleFor(x => x.BedroomCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).NotEmpty();
            RuleFor(x => x.BedCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).NotEmpty();

            RuleFor(x => x.Title).Length(3,100).NotEmpty();
            RuleFor(x => x.Description).Length(5,1000).NotEmpty();
            RuleFor(x => x.Latitude).NotEmpty();
            RuleFor(x => x.Longitude).NotEmpty();
            RuleFor(x => x.City).Length(2, 50).NotEmpty();
            RuleFor(x => x.Country).Length(2, 50).NotEmpty();
            RuleFor(x => x.Street).Length(5, 50).NotEmpty();
          
            RuleFor(x => x.MainPropertyImage).NotEmpty();
            RuleForEach(x => x.DetailPropertyImages).NotEmpty();
            RuleForEach(x => x.PropertyAmenities).NotEmpty();
            
        }
    }

}
