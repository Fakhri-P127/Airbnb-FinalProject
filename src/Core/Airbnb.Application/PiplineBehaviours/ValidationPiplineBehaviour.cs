using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Airbnb.Application.PiplineBehaviours
{
    public class ValidationPiplineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationPiplineBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators is not null)
            {
                ValidationContext<TRequest> context = new(request);
                ValidationResult[] validationResults = await Task.WhenAll(_validators
                    .Select(v => v.ValidateAsync(context, cancellationToken)));
                List<ValidationFailure> errors = validationResults
                    .SelectMany(r => r.Errors).Where(f => f != null).ToList();
                
                if (errors.Any())
                    throw new ValidationException(errors);

            }
            return await next();
        }
    }
}