using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorList;

public class GetAuthorListQuery: IRequest<AuthorListVm>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}