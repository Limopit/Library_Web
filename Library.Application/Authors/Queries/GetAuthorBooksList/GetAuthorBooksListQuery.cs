using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorBooksList;

public class GetAuthorBooksListQuery: IRequest<AuthorBooksListVm>
{
    public Guid AuthorId { get; set; }
}