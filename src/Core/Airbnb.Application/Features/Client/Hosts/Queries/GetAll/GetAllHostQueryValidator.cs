using Airbnb.Application.Contracts.v1.Client.Host.Parameters;
using FluentValidation;

namespace Airbnb.Application.Features.Client.Hosts.Queries.GetAll
{
    public class GetAllHostQueryValidator:AbstractValidator<GetAllHostQuery>
    {
        public GetAllHostQueryValidator()
        {
            RuleFor(x => x.Parameters).SetValidator(new HostParametersValidator());
        }
    }
}
