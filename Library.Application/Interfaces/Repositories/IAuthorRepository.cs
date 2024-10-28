using Library.Application.Authors.Queries.GetAuthorBooksList;
using Library.Application.Authors.Queries.GetAuthorById;
using Library.Application.Authors.Queries.GetAuthorList;
using Library.Domain;

namespace Library.Application.Interfaces.Repositories;

public interface IAuthorRepository: IBaseRepository<Author>
{
    Task<List<Book>> GetAuthorBookListAsync(Guid id, CancellationToken token);
    Task<Author?> GetAuthorInfoByIdAsync(Guid id, CancellationToken token);
    Task<List<Author>> GetPaginatedEntityListAsync(int pageNumber, int pageSize, CancellationToken token);
}