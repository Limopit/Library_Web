using FluentValidation;

namespace Library.Application.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandValidator: AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(command => command.author_id).NotEmpty()
            .WithMessage("ID is required");
    }
}