using FluentValidation;

namespace Airbnb.Application.Features.Admin.CancellationPolicies.Commands.Create
{
    public class CreateCancellationPolicyCommandValidator:AbstractValidator<CreateCancellationPolicyCommand>
    {
        public CreateCancellationPolicyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(2, 50);
            RuleFor(x => x.NoRefund).NotEmpty().Length(2, 300);
            RuleFor(x => x.PartialRefund).NotEmpty().Length(2, 300);
            RuleFor(x => x.FullRefund).NotEmpty().Length(2, 300);
        }
    }
}
