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
            author_id = authorId,
            author_firstname = "Some",
            author_lastname = "Author",
            author_country = "Some Country",
            books = new List<Book>()
        };
        expectedAuthor.books.Add(new Book
        {
            author = expectedAuthor,
            author_id = expectedAuthor.author_id,
            book_description = "qqq",
            book_genre = "ggg",
            book_name = "name",
            book_id = bookID,
            ISBN = "123",
            borrowRecords = new List<BorrowRecord>(),
            imageUrls = ""
        });
        
        List<AuthorBooksListDto> bookListDto = new List<AuthorBooksListDto>()
        {
            new AuthorBooksListDto()
            {
                book_description = expectedAuthor.books.First().book_description,
                book_genre = expectedAuthor.books.First().book_genre,
                book_name = expectedAuthor.books.First().book_name,
                ISBN = expectedAuthor.books.First().ISBN
            }
        }; 

        CancellationToken token = new CancellationToken();

        _mocks.SetupGetAuthorByIdAsync(authorId, expectedAuthor);
        _mocks.SetupGetAuthorBookListAsync(authorId, expectedAuthor.books.ToList(), bookListDto, token);

        // Act
        var getAuthorCommand = new GetAuthorBooksListQuery { author_id = authorId };
        var result = await _handler.Handle(getAuthorCommand, token);
        
        // Assert
        result.Should().NotBeNull();
        result.Books.First().book_name.Should().Be("name");
    }
    
    [Fact]
    public async Task GetAuthorBookListTest_AuthorNotFound()
    {
        //Arrange
        var authorId = Guid.NewGuid();
        var bookID = Guid.NewGuid();
        var expectedAuthor = new Domain.Author
        {
            author_id = authorId,
            author_firstname = "Some",
            author_lastname = "Author",
            author_country = "Some Country",
            books = new List<Book>()
        };
        expectedAuthor.books.Add(new Book
        {
            author = expectedAuthor,
            author_id = expectedAuthor.author_id,
            book_description = "qqq",
            book_genre = "ggg",
            book_name = "name",
            book_id = bookID,
            ISBN = "123",
            borrowRecords = new List<BorrowRecord>(),
            imageUrls = ""
        });

        CancellationToken token = new CancellationToken();

        _mocks.SetupGetAuthorBookListAsync(authorId, expectedAuthor.books.ToList(), new List<AuthorBooksListDto>(), token);

        // Act & assert
        var command = new GetAuthorBooksListQuery { author_id = authorId };
        await _handler
            .Invoking(async h => await h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<NotFoundException>();
        
    }
}