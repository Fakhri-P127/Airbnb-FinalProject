using FluentValidation;

namespace Airbnb.Application.Features.Admin.AirCovers.Commands.Update
{
    public class UpdateAirCoverCommandValidator:AbstractValidator<UpdateAirCoverCommand>
    {
        public UpdateAirCoverCommandValidator()
        {
            RuleFor(x => x.Logo).MaximumLength(300);
            RuleFor(x => x.Title).MaximumLength(500);
            RuleFor(x => x.FindMore).MaximumLength(1000);
            RuleFor(x => x.GetWhatYouBookedGuarantee).MaximumLength(1000);
            RuleFor(x => x.FullDaySafetyLine).MaximumLength(1000);
            RuleFor(x => x.CheckInGuarantee).MaximumLength(1000);
            RuleFor(x => x.BookingProtectionGuarantee).MaximumLength(1000);
        }
    }
}
