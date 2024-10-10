using FluentValidation;

namespace Library.Application.Books.Queries.GetBookByISBN;

public class GetBookByISBNQueryValidator: AbstractValidator<GetBookByISBNQuery>
{
    public GetBookByISBNQueryValidator()
    {
        RuleFor(command => command.ISBN).NotEmpty().WithMessage("ISBN is required");
    }
}