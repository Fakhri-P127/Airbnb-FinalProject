using FluentValidation;

namespace Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Parameters
{
    public class RefreshTokenParametersValidator:AbstractValidator<RefreshTokenParameters>
    {
        public RefreshTokenParametersValidator()
        {
            RuleFor(x=>x.MinExpiryDate).GreaterThanOrEqualTo(new DateTime(2010,01,01))
                .LessThanOrEqualTo(DateTime.Now.AddMonths(1));
            RuleFor(x=>x.MaxExpiryDate).GreaterThanOrEqualTo(new DateTime(2010,01,01))
                .LessThanOrEqualTo(DateTime.Now.AddMonths(1));

            When(x => x.MinExpiryDate.HasValue, () =>
            {
                RuleFor(x => x.MaxExpiryDate).GreaterThan(x => x.MinExpiryDate);
            });
        }
    }
}
