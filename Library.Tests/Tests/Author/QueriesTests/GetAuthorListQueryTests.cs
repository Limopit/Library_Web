using FluentAssertions;
using Library.Application.Authors.Queries.GetAuthorById;
using Library.Application.Authors.Queries.GetAuthorList;
using Library.Domain;
using Library.Tests.Common;
using Library.Tests.Common.Mocks;
using Xunit;

namespace Library.Tests.Tests.Author.QueriesTests;

public class GetAuthorListQueryTests: BaseTestCommand
{
    private readonly GetAuthorListQueryHandler _handler;
    private readonly AuthorMocks _mocks;

    public GetAuthorListQueryTests()
    {
        _handler = new GetAuthorListQueryHandler(Context.UnitOfWorkMock.Object);
        _mocks = new AuthorMocks(Context.UnitOfWorkMock);
    }
    
    [Fact]
    public async Task GetAuthorByIdTest_Success()
    {
        //Arrange
        var authorIdA = Guid.NewGuid();
        var expectedAuthorA = new Domain.Author
        {
            author_id = authorIdA,
            author_firstname = "Some",
            author_lastname = "Author",
            author_country = "Some Country",
        };
        var authorIdB = Guid.NewGuid();
        var expectedAuthorB = new Domain.Author
        {
            author_id = authorIdB,
            author_firstname = "Not",
            author_lastname = "Author",
            author_country = "Some Country",
        };
        var authors = new List<Domain.Author> { expectedAuthorA, expectedAuthorB };

        CancellationToken token = new CancellationToken();

        _mocks.SetupGetAuthorListAsync(authors, token);

        // Act
        var getAuthorCommand = new GetAuthorListQuery{PageNumber = 1, PageSize = 10};
        var result = await _handler.Handle(getAuthorCommand, token);
        
        // Assert
        result.Should().NotBeNull();
        result.Authors.First().AuthorFirstname.Should().Be("Some");
        result.Authors.Last().AuthorFirstname.Should().Be("Not");
    }
}