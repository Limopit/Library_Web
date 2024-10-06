using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorDetails;

public class GetAuthorDetailsQuery: IRequest<AuthorDetailsVm>
{
    public Guid author_id { get; set; }
}