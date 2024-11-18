using FluentAssertions;
using Library.Application.Authors.Queries.GetAuthorById;
using Library.Application.Common.Exceptions;
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
        _mocks = new AuthorMocks(Context.UnitOfWorkMock);
        _handler = new GetAuthorByIdQueryHandler(Context.UnitOfWorkMock.Object, _mocks._mapperMock.Object);
    }
    
    [Fact]
    public async Task GetAuthorByIdTest_Success()
    {
        //Arrange
        var authorId = Guid.NewGuid();
        var expectedAuthor = new Domain.Author
        {
            AuthorId = authorId,
            AuthorFirstname = "Some",
            AuthorLastname = "Author",
            AuthorCountry = "Some Country",
            Books = new List<Book>()
        };

        var expectedAuthorDto = new AuthorDetailsDto
        {
            AuthorId = expectedAuthor.AuthorId,
            AuthorFirstname = expectedAuthor.AuthorFirstname,
            AuthorLastname = expectedAuthor.AuthorLastname,
            AuthorCountry = expectedAuthor.AuthorCountry,
            Books = new List<BookListDto>()
        };

        CancellationToken token = new CancellationToken();

        _mocks.SetupGetAuthorInfoByIdAsync(authorId, expectedAuthor, token);

        // Act
        var getAuthorCommand = new GetAuthorByIdQuery { AuthorId = authorId };
        var result = await _handler.Handle(getAuthorCommand, token);
        
        // Assert
        result.Should().NotBeNull();
        result.AuthorId.Should().Be(expectedAuthor.AuthorId);
        result.AuthorFirstname.Should().Be(expectedAuthor.AuthorFirstname);
        result.AuthorLastname.Should().Be(expectedAuthor.AuthorLastname);
    }
    
    [Fact]
    public async Task GetAuthorByIdTest_Failure()
    {
        //Arrange
        var authorId = Guid.NewGuid();

        CancellationToken token = new CancellationToken();

        _mocks.SetupGetAuthorInfoByIdAsync(authorId, null, token);

        // Act & Assert
        var getAuthorCommand = new GetAuthorByIdQuery { AuthorId = authorId };
        await _handler
            .Invoking(async h => await h.Handle(getAuthorCommand, CancellationToken.None))
            .Should()
            .ThrowAsync<NotFoundException>();
    }
}