﻿using Library.Application.Authors.Queries.GetAuhtorList;
using Library.Application.Authors.Queries.GetAuthorBooksList;
using Library.Application.Authors.Queries.GetAuthorDetails;
using Library.Domain;

namespace Library.Application.Interfaces;

public interface IAuthorRepository
{
    Task AddAuthorAsync(Author author, CancellationToken token);
    Task DeleteAuthorAsync(Author author);
    Task<AuthorBooksListVm> GetAuthorBookListAsync(Guid id, CancellationToken token);
    Task<AuthorDetailsVm> GetAuthorInfoByIdAsync(Guid id, CancellationToken token);
    Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken token);
    Task<AuthorListVm> GetAuthorListAsync(CancellationToken token);
}