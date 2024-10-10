using FluentValidation;

namespace Library.Application.Authors.Queries.GetAuthorBooksList;

public class GetAuthorBooksListQueryValidator: AbstractValidator<GetAuthorBooksListQuery>
{
    public GetAuthorBooksListQueryValidator()
    {
        RuleFor(command => command.author_id).NotEmpty()
            .WithMessage("Id is required");
    }
}