using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorById;

public class GetAuthorByIdQuery: IRequest<AuthorDetailsDto>
{
    public Guid AuthorId { get; set; }
}