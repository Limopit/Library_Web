using FluentValidation;

namespace Library.Application.Books.Queries.GetBookById;

public class GetBookByIdQueryValidator: AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {
        RuleFor(command 
            => command.BookId).NotEmpty().WithMessage("Id is required");
    }
}