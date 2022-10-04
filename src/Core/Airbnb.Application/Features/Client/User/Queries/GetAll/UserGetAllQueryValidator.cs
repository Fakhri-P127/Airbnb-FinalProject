using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.User.Parameters;
using FluentValidation;

namespace Airbnb.Application.Features.Client.User.Queries.GetAll
{
    public class UserGetAllQueryValidator:AbstractValidator<UserGetAllQuery>
    {

        public UserGetAllQueryValidator()
        {
            RuleFor(x => x.Parameters).SetValidator(new UserParametersValidator());
        }
    }
}
