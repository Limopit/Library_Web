using FluentValidation;

namespace Library.Application.Authors.Queries.GetAuthorDetails;

public class GetAuthorByIdQueryValidator: AbstractValidator<GetAuthorByIdQuery>
{
    public GetAuthorByIdQueryValidator()
    {
        RuleFor(command => command.author_id).NotEmpty()
            .WithMessage("Id is required");
    }
}