using Library.Application.Authors.Queries.GetAuthorById;
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
                => repo.AddEntityAsync(It.IsAny<Author>(), cancellationToken))
            .Callback<Author, CancellationToken>((a, _) =>
            {
                if (a.author_id == Guid.Empty)
                {
                    a.author_id = Guid.NewGuid();
                }
            })
            .Returns(Task.CompletedTask);
    }

    public void SetupGetAuthorByIdAsync(Guid authorId, Author author, CancellationToken cancellationToken = default)
    {
        AuthorRepositoryMock.Setup(repo 
                => repo.GetAuthorInfoByIdAsync(authorId, cancellationToken))!
            .ReturnsAsync(author != null ? new AuthorDetailsVm
            {
                author_id = author.author_id,
                author_firstname = author.author_firstname,
                author_lastname = author.author_lastname,
                author_birthday = author.author_birthday,
                author_country = author.author_country,
                books = author.books.Select(b 
                    => new BookListDto { book_id = b.book_id, book_name = b.book_name,
                        book_genre = b.book_genre}).ToList()
            } : null);
    }
}