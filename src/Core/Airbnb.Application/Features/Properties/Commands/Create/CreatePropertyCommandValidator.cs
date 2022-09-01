using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Properties.Commands.Create
{
    public class CreatePropertyCommandValidator:AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(x => x.Price).GreaterThanOrEqualTo(10).LessThanOrEqualTo(100000).NotEmpty();
            
        }
    }
}
