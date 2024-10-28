using Library.Application.Authors.Queries.GetAuthorBooksList;
using Library.Application.Authors.Queries.GetAuthorById;
using Library.Application.Authors.Queries.GetAuthorList;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Domain;
using Moq;

namespace Library.Tests.Common.Mocks;

public class AuthorMocks
{
    public readonly Mock<IAuthorRepository> AuthorRepositoryMock;

    public AuthorMocks(Mock<IUnitOfWork> unitOfWorkMock)
    {
        AuthorRepositoryMock = new Mock<IAuthorRepository>();
        
        unitOfWorkMock.Setup(uow => uow.Authors).Returns(AuthorRepositoryMock.Object);
    }
    
    
    public void SetupAddAuthorAsync(Author author, CancellationToken cancellationToken = default)
    {
        AuthorRepositoryMock.Setup(repo 
                => repo.AddEntityAsync(author, cancellationToken))
            .Returns(Task.CompletedTask);
    }

    public void SetupGetAuthorInfoByIdAsync(Guid authorId, Author author, CancellationToken cancellationToken = default)
    {
        AuthorRepositoryMock.Setup(repo 
                => repo.GetAuthorInfoByIdAsync(authorId, cancellationToken))!
            .ReturnsAsync(author != null ? new Author()
            {
                author_id = author.author_id,
                author_firstname = author.author_firstname,
                author_lastname = author.author_lastname,
                author_birthday = author.author_birthday,
                author_country = author.author_country,
                books = author.books.Select(b => new Book
                {
                    book_id = b.book_id,
                    book_name = b.book_name,
                    book_genre = b.book_genre
                }).ToList()
            } : null);
    }

    public void SetupGetAuthorBookListAsync(Guid id, List<Book> books, CancellationToken token)
    {
        AuthorRepositoryMock.Setup(repo => repo.GetAuthorBookListAsync(id, token))
            .ReturnsAsync(books);
    }

    public void SetupGetAuthorListAsync(List<Author> authors, CancellationToken token)
    {
        AuthorRepositoryMock.Setup(repo => repo.GetPaginatedEntityListAsync(1, 10, token))
            .ReturnsAsync(authors);
    }

    public void SetupGetAuthorByIdAsync(Guid authorId, Author author)
    {
        AuthorRepositoryMock.Setup(repo => repo.GetEntityByIdAsync(authorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(author);
    }
    
    public void SetupRemoveAuthorAsync(Author author)
    {
        AuthorRepositoryMock.Setup(repo 
                => repo.RemoveEntity(author))
            .Returns(Task.CompletedTask);
    }
}