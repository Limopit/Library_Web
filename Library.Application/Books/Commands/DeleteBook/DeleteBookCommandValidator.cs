using FluentValidation;

namespace Library.Application.Books.Commands.DeleteBook;

public class DeleteBookCommandValidator: AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(command => command.book_id).NotEmpty().WithMessage("Id is required");
    }
}