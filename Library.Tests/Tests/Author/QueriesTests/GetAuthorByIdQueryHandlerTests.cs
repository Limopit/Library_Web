using FluentAssertions;
using Library.Application.Authors.Queries.GetAuthorById;
using Library.Domain;
using Library.Tests.Common;
using Library.Tests.Common.Mocks;
using Xunit;

namespace Library.Tests.Tests.Author.QueriesTests;

public class GetAuthorByIdQueryHandlerTests: BaseTestCommand
{
    private readonly GetAuthorByIdQueryHandler _handler;
    private readonly AuthorMocks _mocks;

    public GetAuthorByIdQueryHandlerTests()
    {
        _handler = new GetAuthorByIdQueryHandler(Context.UnitOfWorkMock.Object);
        _mocks = new AuthorMocks(Context.UnitOfWorkMock);
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

        _mocks.SetupGetAuthorInfoByIdAsync(authorId, expectedAuthor, token);

        // Act
        var getAuthorCommand = new GetAuthorByIdQuery { author_id = authorId };
        var result = await _handler.Handle(getAuthorCommand, token);
        
        // Assert
        result.Should().NotBeNull();
        result.author_id.Should().Be(expectedAuthor.author_id);
        result.author_firstname.Should().Be(expectedAuthor.author_firstname);
        result.author_lastname.Should().Be(expectedAuthor.author_lastname);
    }
    
    [Fact]
    public async Task GetAuthorByIdTest_Failure()
    {
        //Arrange
        var authorId = Guid.NewGuid();

        CancellationToken token = new CancellationToken();

        _mocks.SetupGetAuthorInfoByIdAsync(authorId, null, token);

        // Act
        var getAuthorCommand = new GetAuthorByIdQuery { author_id = authorId };
        var result = await _handler.Handle(getAuthorCommand, token);
        
        // Assert
        result.Should().BeNull();
    }
}