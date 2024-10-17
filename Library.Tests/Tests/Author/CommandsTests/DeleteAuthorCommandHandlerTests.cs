using FluentAssertions;
using Library.Application.Authors.Commands.DeleteAuthor;
using Library.Application.Common.Exceptions;
using Library.Domain;
using Library.Tests.Common;
using Library.Tests.Common.Mocks;
using Moq;
using Xunit;

namespace Library.Tests.Tests.Author.CommandsTests;

public class DeleteAuthorCommandHandlerTests: BaseTestCommand
{
    private readonly DeleteAuthorCommandHandler _handler;
    private readonly AuthorMocks _mocks;

    public DeleteAuthorCommandHandlerTests()
    {
        _handler = new DeleteAuthorCommandHandler(Context.UnitOfWorkMock.Object);
        _mocks = new AuthorMocks(Context.UnitOfWorkMock);
    }

    [Fact]
    public async Task DeleteAuthorTask_Success()
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
        _mocks.SetupRemoveAuthorAsync(expectedAuthor);

        //Act
        var command = new DeleteAuthorCommand { author_id = expectedAuthor.author_id };
        await _handler.Handle(command, token);
        
        //Assert
        _mocks.AuthorRepositoryMock.Verify(repo => repo.RemoveEntity(expectedAuthor), Times.Once);
    }

    [Fact]
    public async Task DeleteAuthorTask_AuthorNotFound()
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

        _mocks.SetupRemoveAuthorAsync(expectedAuthor);

        //Act & Assert
        var command = new DeleteAuthorCommand { author_id = expectedAuthor.author_id };

        await _handler
            .Invoking(async h => await h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<NotFoundException>();
        
        _mocks.AuthorRepositoryMock.Verify(repo => repo.RemoveEntity(expectedAuthor), Times.Never);
    }
}