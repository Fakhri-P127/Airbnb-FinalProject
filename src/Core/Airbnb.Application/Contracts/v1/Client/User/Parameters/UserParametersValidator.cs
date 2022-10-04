using FluentValidation;

namespace Airbnb.Application.Contracts.v1.Client.User.Parameters
{
    public class UserParametersValidator:AbstractValidator<UserParameters>
    {
        public UserParametersValidator()
        {
            RuleFor(x => x.Email).Length(6, 30);
            RuleFor(x => x.Firstname).Length(2, 15);
            RuleFor(x => x.Lastname).Length(2, 15);
            RuleFor(x => x.PhoneNumber)
              .Matches("^\\(?(\\+994)?\\)?[\\s\\-]?0?(50|51|55|70|77|12)[\\s\\-]?\\d{3}[\\s\\-]?\\d{2}[\\s\\-]?\\d{2}[\\s\\-]?$")
              .WithMessage("{PropertyName} {PropertyValue} is not in the correct format. We only accept Azerbaijan numbers");
            
            RuleFor(x => x.MinDateOfBirth).GreaterThanOrEqualTo(DateTime.Now.AddYears(-100))
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18));
            RuleFor(x => x.MaxDateOfBirth).GreaterThanOrEqualTo(DateTime.Now.AddYears(-100))
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18));

            When(x => x.MinDateOfBirth.HasValue, () =>
            {
                RuleFor(x => x.MaxDateOfBirth).GreaterThan(x => x.MinDateOfBirth);
            });

            RuleForEach(x => x.LanguageCodes).NotEmpty().Length(2, 5);

            RuleFor(x=>x.MinCountForReviewsAboutYou).GreaterThanOrEqualTo(0).LessThanOrEqualTo(10000);
            RuleFor(x=>x.MaxCountForReviewsAboutYou).GreaterThanOrEqualTo(0).LessThanOrEqualTo(10000);
            When(x => x.MinCountForReviewsAboutYou.HasValue, () =>
            {
                RuleFor(x => x.MaxCountForReviewsAboutYou).GreaterThan(x => x.MinCountForReviewsAboutYou);
            });

            RuleFor(x => x.MinCountForReviewsByYou).GreaterThanOrEqualTo(0).LessThanOrEqualTo(10000);
            RuleFor(x => x.MaxCountForReviewsByYou).GreaterThanOrEqualTo(0).LessThanOrEqualTo(10000);
            When(x => x.MinCountForReviewsByYou.HasValue, () =>
            {
                RuleFor(x => x.MaxCountForReviewsByYou).GreaterThan(x => x.MinCountForReviewsByYou);
            });

            RuleFor(x => x.MinCountForReservationsYouMade).GreaterThanOrEqualTo(0).LessThanOrEqualTo(10000);
            RuleFor(x => x.MaxCountForReservationsYouMade).GreaterThanOrEqualTo(0).LessThanOrEqualTo(10000);
            When(x => x.MinCountForReservationsYouMade.HasValue, () =>
            {
                RuleFor(x => x.MaxCountForReservationsYouMade).GreaterThan(x => x.MinCountForReservationsYouMade);
            });
        }
    }
}
