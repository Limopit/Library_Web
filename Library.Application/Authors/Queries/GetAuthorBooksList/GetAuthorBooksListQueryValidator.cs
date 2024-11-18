using FluentValidation;

namespace Library.Application.Authors.Queries.GetAuthorBooksList;

public class GetAuthorBooksListQueryValidator: AbstractValidator<GetAuthorBooksListQuery>
{
    public GetAuthorBooksListQueryValidator()
    {
        RuleFor(command => command.AuthorId).NotEmpty()
            .WithMessage("Id is required");
    }
}