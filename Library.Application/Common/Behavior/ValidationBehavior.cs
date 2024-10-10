using FluentValidation;
using Library.Application.Common.Exceptions;
using MediatR;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Library.Application.Common.Behavior;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest,TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;
    
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(val => val.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .ToList();
        if (failures.Count != 0)
        {
            throw new CustomValidationException(failures);
        }

        return next();
    }
} 