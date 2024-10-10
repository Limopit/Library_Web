using FluentValidation;
using FluentValidation.Results;

namespace Library.Application.Common.Exceptions;

public class CustomValidationException: ValidationException
{
    public List<string> errors { get; set; }

    public CustomValidationException(IEnumerable<ValidationFailure> failures)
        : base("Validation failed")
    {
        errors = new List<string>();
        foreach (var failure in failures)
        {
            errors.Add($"Property {failure.PropertyName} failed validation. Error {failure.ErrorMessage}");
        }
    }
}