using FluentAssertions;
using Library.Application.Authors.Commands.UpdateAuthor;
using Library.Application.Common.Exceptions;
using Library.Domain;
using Library.Tests.Common;
using Library.Tests.Common.Mocks;
using Xunit;

namespace Library.Tests.Tests.Author.CommandsTests;

public class UpdateAuthorCommandHandlerTests: BaseTestCommand
{
    private readonly UpdateAuthorCommandHandler _handler;
    private readonly AuthorMocks _mocks;

    public UpdateAuthorCommandHandlerTests()
    {
        _handler = new UpdateAuthorCommandHandler(Context.UnitOfWorkMock.Object);
        _mocks = new AuthorMocks(Context.UnitOfWorkMock);
    }

    [Fact]
    public async Task UpdateAuthorTest_Success()
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

        _mocks.SetupGetAuthorByIdAsync(authorId, expectedAuthor);
        
        //Act
        var command = new UpdateAuthorCommand
        {
            author_id = authorId,
            author_firstname = "Not",
            author_lastname = "Author"
        };

        await _handler.Handle(command, token);

        //Assert
        expectedAuthor.author_firstname.Should().Be("Not");
    }
    
    [Fact]
    public async Task UpdateAuthorTest_AuthorNotFound()
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

        _mocks.SetupGetAuthorByIdAsync(authorId, expectedAuthor);

        //Act & Assert
        var command = new UpdateAuthorCommand
        {
            author_firstname = "Not",
            author_lastname = "Author"
        };

        await _handler
            .Invoking(async h => await h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<NotFoundException>();
    }
}