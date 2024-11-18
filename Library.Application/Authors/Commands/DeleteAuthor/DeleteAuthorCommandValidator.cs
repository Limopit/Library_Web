using FluentValidation;

namespace Library.Application.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandValidator: AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(command => command.AuthorId).NotEmpty()
            .WithMessage("ID is required");
    }
}