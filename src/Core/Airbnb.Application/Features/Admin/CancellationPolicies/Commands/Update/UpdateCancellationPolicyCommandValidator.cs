using FluentValidation;

namespace Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Update
{
    public class UpdateCancellationPolicyCommandValidator:AbstractValidator<UpdateCancellationPolicyCommand>
    {
        public UpdateCancellationPolicyCommandValidator()
        {
            RuleFor(x => x.Name).Length(2, 50);
            RuleFor(x => x.NoRefund).Length(2, 300);
            RuleFor(x => x.PartialRefund).Length(2, 300);
            RuleFor(x => x.FullRefund).Length(2, 300);
        }
    }
}
