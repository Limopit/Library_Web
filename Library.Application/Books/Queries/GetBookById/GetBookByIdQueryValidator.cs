using FluentValidation;

namespace Library.Application.Books.Queries.GetBookById;

public class GetBookByIdQueryValidator: AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {
        RuleFor(command 
            => command.book_id).NotEmpty().WithMessage("Id is required");
    }
}