using Library.Application.Authors.Queries.GetAuthorBooksList;
using Library.Application.Authors.Queries.GetAuthorById;
using Library.Application.Authors.Queries.GetAuthorList;
using Library.Domain;

namespace Library.Application.Interfaces.Repositories;

public interface IAuthorRepository: IBaseRepository<Author>
{
    Task<AuthorBooksListVm> GetAuthorBookListAsync(Guid id, CancellationToken token);
    Task<AuthorDetailsVm> GetAuthorInfoByIdAsync(Guid id, CancellationToken token);
    Task<AuthorListVm> GetPaginatedEntityListAsync(int pageNumber, int pageSize, CancellationToken token);
}