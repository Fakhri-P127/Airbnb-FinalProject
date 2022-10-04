using Airbnb.Application.Contracts.v1.Admin.RefreshTokens.Parameters;
using FluentValidation;

namespace Airbnb.Application.Features.Admin.RefreshTokens.Queries.GetAll
{
    public class GetAllRefreshTokensQueryValidator:AbstractValidator<GetAllRefreshTokensQuery>
    {
        public GetAllRefreshTokensQueryValidator()
        {
            RuleFor(x => x.Parameters).SetValidator(new RefreshTokenParametersValidator());
        }
    }
}
