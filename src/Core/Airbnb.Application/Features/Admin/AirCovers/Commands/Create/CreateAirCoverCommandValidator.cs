using FluentValidation;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Create
{
    public class CreateAirCoverCommandValidator:AbstractValidator<CreateAirCoverCommand>
    {
        public CreateAirCoverCommandValidator()
        {
            RuleFor(x => x.Logo).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(500);
            RuleFor(x => x.FindMore).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.GetWhatYouBookedGuarantee).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.FullDaySafetyLine).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.CheckInGuarantee).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.BookingProtectionGuarantee).NotEmpty().MaximumLength(1000);
        }
    }
}
