using FluentValidation;

namespace Library.Application.Books.Commands.CreateBook;

public class CreateBookQueryValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookQueryValidator()
    {
        RuleFor(command => command.ISBN)
            .NotEmpty().WithMessage("ISBN is required")
            .Length(13,17).WithMessage("ISBN must be 13-17 char");
        RuleFor(command => command.BookName).NotEmpty()
            .WithMessage("Name is required").MaximumLength(64);
        RuleFor(command => command.BookGenre).MaximumLength(32);
        RuleFor(command => command.BookDescription).MaximumLength(256);
        RuleFor(command => command.AuthorId).NotEmpty().WithMessage("Author is required");
    }

}