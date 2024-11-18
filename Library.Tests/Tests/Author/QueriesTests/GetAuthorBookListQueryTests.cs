using FluentAssertions;
using Library.Application.Authors.Queries.GetAuthorBooksList;
using Library.Application.Common.Exceptions;
using Library.Domain;
using Library.Tests.Common;
using Library.Tests.Common.Mocks;
using Xunit;

namespace Library.Tests.Tests.Author.QueriesTests;

public class GetAuthorBookListQueryTests: BaseTestCommand
{
    private readonly GetAuthorBooksListQueryHandler _handler;
    private readonly AuthorMocks _mocks;
    

    public GetAuthorBookListQueryTests()
    {
        _mocks = new AuthorMocks(Context.UnitOfWorkMock);
        _handler = new GetAuthorBooksListQueryHandler(Context.UnitOfWorkMock.Object, _mocks._mapperMock.Object);
    }
    
    [Fact]
    public async Task GetAuthorBookListTest_Success()
    {
        //Arrange
        var authorId = Guid.NewGuid();
        var bookID = Guid.NewGuid();
        var expectedAuthor = new Domain.Author
        {
            AuthorId = authorId,
            AuthorFirstname = "Some",
            AuthorLastname = "Author",
            AuthorCountry = "Some Country",
            Books = new List<Book>()
        };
        expectedAuthor.Books.Add(new Book
        {
            Author = expectedAuthor,
            AuthorId = expectedAuthor.AuthorId,
            BookDescription = "qqq",
            BookGenre = "ggg",
            BookName = "name",
            BookId = bookID,
            ISBN = "123",
            BorrowRecords = new List<BorrowRecord>(),
            ImageUrls = ""
        });
        
        List<AuthorBooksListDto> bookListDto = new List<AuthorBooksListDto>()
        {
            new AuthorBooksListDto()
            {
                BookDescription = expectedAuthor.Books.First().BookDescription,
                BookGenre = expectedAuthor.Books.First().BookGenre,
                BookName = expectedAuthor.Books.First().BookName,
                ISBN = expectedAuthor.Books.First().ISBN
            }
        }; 

        CancellationToken token = new CancellationToken();

        _mocks.SetupGetAuthorByIdAsync(authorId, expectedAuthor);
        _mocks.SetupGetAuthorBookListAsync(authorId, expectedAuthor.Books.ToList(), bookListDto, token);

        // Act
        var getAuthorCommand = new GetAuthorBooksListQuery { AuthorId = authorId };
        var result = await _handler.Handle(getAuthorCommand, token);
        
        // Assert
        result.Should().NotBeNull();
        result.Books.First().BookName.Should().Be("name");
    }
    
    [Fact]
    public async Task GetAuthorBookListTest_AuthorNotFound()
    {
        //Arrange
        var authorId = Guid.NewGuid();
        var bookID = Guid.NewGuid();
        var expectedAuthor = new Domain.Author
        {
            AuthorId = authorId,
            AuthorFirstname = "Some",
            AuthorLastname = "Author",
            AuthorCountry = "Some Country",
            Books = new List<Book>()
        };
        expectedAuthor.Books.Add(new Book
        {
            Author = expectedAuthor,
            AuthorId = expectedAuthor.AuthorId,
            BookDescription = "qqq",
            BookGenre = "ggg",
            BookName = "name",
            BookId = bookID,
            ISBN = "123",
            BorrowRecords = new List<BorrowRecord>(),
            ImageUrls = ""
        });

        CancellationToken token = new CancellationToken();

        _mocks.SetupGetAuthorBookListAsync(authorId, expectedAuthor.Books.ToList(), new List<AuthorBooksListDto>(), token);

        // Act & assert
        var command = new GetAuthorBooksListQuery { AuthorId = authorId };
        await _handler
            .Invoking(async h => await h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<NotFoundException>();
        
    }
}