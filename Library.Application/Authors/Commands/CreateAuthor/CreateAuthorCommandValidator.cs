using FluentValidation;

namespace Library.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandValidator: AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.author_firstname).NotEmpty()
            .WithMessage("Firstname is required").MaximumLength(64);
        RuleFor(command => command.author_lastname).NotEmpty()
            .WithMessage("Lastname is required").MaximumLength(64);
        RuleFor(command => command.author_country).MaximumLength(32);
        RuleFor(command => command.author_birthday)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Author birthday shouldn`t be in future");
    }
}