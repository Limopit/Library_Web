using FluentValidation;

namespace Library.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandValidator: AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.AuthorFirstname).NotEmpty()
            .WithMessage("Firstname is required").MaximumLength(64);
        RuleFor(command => command.AuthorLastname).NotEmpty()
            .WithMessage("Lastname is required").MaximumLength(64);
        RuleFor(command => command.AuthorCountry).MaximumLength(32);
        RuleFor(command => command.AuthorBirthday)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Author birthday shouldn`t be in future");
    }
}