using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, Tresponse> : IPipelineBehavior<TRequest, Tresponse>
    {
        private readonly IEnumerable<IValidator<TRequest>>_validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<Tresponse> Handle(TRequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators.Select(v=>v.Validate(context))
                .SelectMany(result=>result.Errors)
                .Where(f=>f!=null)
                .ToList();
            if (failures.Any())
            {
                var errorMessages = string.Join("; ",failures.Select(f=>f.ErrorMessage));
                throw new ValidationException(errorMessages);
            }
            return await next();
        }
    }
}
