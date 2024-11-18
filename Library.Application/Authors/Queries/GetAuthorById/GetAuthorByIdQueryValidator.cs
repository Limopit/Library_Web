using FluentValidation;

namespace Library.Application.Authors.Queries.GetAuthorById;

public class GetAuthorByIdQueryValidator: AbstractValidator<GetAuthorByIdQuery>
{
    public GetAuthorByIdQueryValidator()
    {
        RuleFor(command => command.AuthorId).NotEmpty()
            .WithMessage("Id is required");
    }
}