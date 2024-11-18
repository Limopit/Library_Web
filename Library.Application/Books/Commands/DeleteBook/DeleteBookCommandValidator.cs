using FluentValidation;

namespace Library.Application.Books.Commands.DeleteBook;

public class DeleteBookCommandValidator: AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(command => command.BookId).NotEmpty().WithMessage("Id is required");
    }
}