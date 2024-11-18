using Library.Application.Authors.Commands.CreateAuthor;
using Library.Application.Authors.Commands.UpdateAuthor;
using Library.Application.Authors.Queries.GetAuthorBooksList;
using Library.Application.Authors.Queries.GetAuthorById;
using Library.Application.Authors.Queries.GetAuthorList;
using Library.Application.Common.Mappings;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Domain;
using Moq;

namespace Library.Tests.Common.Mocks;

public class AuthorMocks
{
    public readonly Mock<IAuthorRepository> AuthorRepositoryMock;
    public readonly Mock<IMapperService> _mapperMock;

    public AuthorMocks(Mock<IUnitOfWork> unitOfWorkMock)
    {
        AuthorRepositoryMock = new Mock<IAuthorRepository>();
        
        unitOfWorkMock.Setup(uow => uow.Authors).Returns(AuthorRepositoryMock.Object);
        
        _mapperMock = new Mock<IMapperService>();
    }
    
    
    public void SetupAddAuthorAsync(CreateAuthorCommand command, Author author, CancellationToken cancellationToken = default)
    {
        AuthorRepositoryMock
            .Setup(repo => repo.AddEntityAsync(author, cancellationToken))
            .Returns(Task.CompletedTask);

        _mapperMock
            .Setup(map => map.Map<CreateAuthorCommand, Author>(It.IsAny<CreateAuthorCommand>()))
            .ReturnsAsync(new Author
            {
                AuthorId = Guid.NewGuid(), // Убедитесь, что AuthorId не равен Guid.Empty
                AuthorFirstname = command.AuthorFirstname,
                AuthorLastname = command.AuthorLastname,
                AuthorBirthday = command.AuthorBirthday,
                AuthorCountry = command.AuthorCountry,
            });
    }

    public void SetupGetAuthorInfoByIdAsync(Guid authorId, Author author, CancellationToken cancellationToken = default)
    {
        AuthorRepositoryMock.Setup(repo 
                => repo.GetAuthorInfoByIdAsync(authorId, cancellationToken))!
            .ReturnsAsync(author != null ? new Author()
            {
                AuthorId = author.AuthorId,
                AuthorFirstname = author.AuthorFirstname,
                AuthorLastname = author.AuthorLastname,
                AuthorBirthday = author.AuthorBirthday,
                AuthorCountry = author.AuthorCountry,
                Books = author.Books.Select(b => new Book
                {
                    BookId = b.BookId,
                    BookName = b.BookName,
                    BookGenre = b.BookGenre
                }).ToList()
            }: null);

        _mapperMock.Setup(map => map.Map<Author, AuthorDetailsDto>(It.IsAny<Author>()))
            .Returns((Author src) => Task.FromResult(new AuthorDetailsDto
            {
                AuthorId = src.AuthorId,
                AuthorFirstname = src.AuthorFirstname,
                AuthorLastname = src.AuthorLastname,
                AuthorBirthday = src.AuthorBirthday,
                AuthorCountry = src.AuthorCountry
            }));
    }

    public void SetupGetAuthorBookListAsync(Guid id, List<Book> books, IList<AuthorBooksListDto> booksDto , CancellationToken token)
    {
        AuthorRepositoryMock.Setup(repo => repo.GetAuthorBookListAsync(id, token))
            .ReturnsAsync(books);

        _mapperMock.Setup(map => map.Map<List<Book>, IList<AuthorBooksListDto>>(books)).ReturnsAsync(booksDto);
    }

    public void SetupGetAuthorListAsync(List<Author> authors, IList<AuthorListDto> authorListDto, CancellationToken token)
    {
        AuthorRepositoryMock.Setup(repo => repo.GetPaginatedEntityListAsync(1, 10, token))
            .ReturnsAsync(authors);
        
        _mapperMock.Setup(map => map.Map<List<Author>, IList<AuthorListDto>>(authors)).ReturnsAsync(authorListDto);
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
    
    public void SetupMapperForUpdate(UpdateAuthorCommand command, Author author)
    {
        _mapperMock
            .Setup(mapper => mapper.Update(command, It.IsAny<Author>()))
            .Callback<UpdateAuthorCommand, Author>((cmd, auth) =>
            {
                auth.AuthorFirstname = cmd.AuthorFirstname;
                auth.AuthorLastname = cmd.AuthorLastname;
                auth.AuthorCountry = cmd.AuthorCountry;
                auth.AuthorBirthday = cmd.AuthorBirthday;
            })
            .ReturnsAsync(author);
    }

}