using Library.Application.Authors.Commands.CreateAuthor;
using Library.Application.Authors.Queries.GetAuthorById;
using Library.Domain;
using Library.Tests.Common;
using Library.Tests.Common.Mocks;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Library.Tests.Tests.Author;

public class GetAuthorByIdTests: BaseTestCommand
{
    private readonly GetAuthorByIdQueryHandler _handler;
    private readonly AuthorMocks _mocks;

    public GetAuthorByIdTests()
    {
        _handler = new GetAuthorByIdQueryHandler(_context._unitOfWorkMock.Object);
        _mocks = new AuthorMocks(_context._unitOfWorkMock);
    }
    
    [Fact]
    public async Task GetAuthorByIdTest_Success()
    {
        //Arrange
        var authorId = Guid.NewGuid();
        var expectedAuthor = new Domain.Author
        {
            author_id = authorId,
            author_firstname = "Some",
            author_lastname = "Author",
            author_country = "Some Country",
            books = new List<Book>()
        };

        CancellationToken token = new CancellationToken();

        _mocks.SetupGetAuthorByIdAsync(authorId, expectedAuthor, token);

        // Act
        var getAuthorCommand = new GetAuthorByIdQuery { author_id = authorId };
        var result = await _handler.Handle(getAuthorCommand, token);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedAuthor.author_id, result.author_id);
        Assert.Equal(expectedAuthor.author_firstname, result.author_firstname);
        Assert.Equal(expectedAuthor.author_lastname, result.author_lastname);
    }
    
    [Fact]
    public async Task GetAuthorByIdTest_Failure()
    {
        //Arrange
        var authorId = Guid.NewGuid();

        CancellationToken token = new CancellationToken();

        _mocks.SetupGetAuthorByIdAsync(authorId, null, token);

        // Act
        var getAuthorCommand = new GetAuthorByIdQuery { author_id = authorId };
        var result = await _handler.Handle(getAuthorCommand, token);
        
        // Assert
        Assert.Null(result);
    }
}