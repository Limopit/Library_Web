using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorById;

public class GetAuthorByIdQuery: IRequest<AuthorDetailsVm>
{
    public Guid author_id { get; set; }
}