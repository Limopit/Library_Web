using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorDetails;

public class GetAuthorByIdQuery: IRequest<AuthorDetailsVm>
{
    public Guid author_id { get; set; }
}