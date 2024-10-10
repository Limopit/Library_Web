using FluentValidation;

namespace Library.Application.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator: AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(command => command.author_id).NotEmpty()
            .WithMessage("ID is required");
        RuleFor(command => command.author_firstname).NotEmpty()
            .WithMessage("Firstname is required").MaximumLength(64);
        RuleFor(command => command.author_lastname).NotEmpty()
            .WithMessage("Lastname is required").MaximumLength(64);
        RuleFor(command => command.author_country).MaximumLength(32);
        RuleFor(command => command.author_birthday)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Author birthday shouldn`t be in future");
    }
}