using MediatR;

namespace Library.Application.Authors.Queries.GetAuhtorList;

public class GetAuthorListQuery: IRequest<AuthorListVm>
{
    public Guid author_id { get; set; }
}