using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.User.Commands.Update
{
    public class UpdateUserCommandValidator:AbstractValidator<UpdateUserCommand>
    {
        private DateTime _minDateTime = DateTime.Now.AddYears(-100);
        private DateTime _maxDateTime = DateTime.Now.AddYears(-18);
        public UpdateUserCommandValidator()
        {
          
            RuleFor(x => x.Firstname).Length(2, 15);
            RuleFor(x => x.Lastname).Length(2, 15);
            RuleFor(x => x.Work).Length(5, 50);
            RuleFor(x => x.About).Length(15, 250);
            RuleFor(x => x.DateOfBirth).GreaterThanOrEqualTo(_minDateTime)
                .LessThanOrEqualTo(_maxDateTime).When(x=>x.DateOfBirth != null);
            RuleFor(x => x.PhoneNumber)
                .Matches("^\\(?(\\+994)?\\)?[\\s\\-]?0?(50|51|55|70|77|12)[\\s\\-]?\\d{3}[\\s\\-]?\\d{2}[\\s\\-]?\\d{2}[\\s\\-]?$")
                .WithMessage("{PropertyName} {PropertyValue} is not in the correct format. We only accept Azerbaijan numbers");
        }
    }
}
