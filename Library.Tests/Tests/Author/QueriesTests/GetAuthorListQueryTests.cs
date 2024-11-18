using FluentAssertions;
using Library.Application.Authors.Queries.GetAuthorList;
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
        _mocks = new AuthorMocks(Context.UnitOfWorkMock);
        _handler = new GetAuthorListQueryHandler(Context.UnitOfWorkMock.Object, _mocks._mapperMock.Object);
    }
    
    [Fact]
    public async Task GetAuthorByIdTest_Success()
    {
        //Arrange
        var authorIdA = Guid.NewGuid();
        var expectedAuthorA = new Domain.Author
        {
            AuthorId = authorIdA,
            AuthorFirstname = "Some",
            AuthorLastname = "Author",
            AuthorCountry = "Some Country",
        };
        var authorIdB = Guid.NewGuid();
        var expectedAuthorB = new Domain.Author
        {
            AuthorId = authorIdB,
            AuthorFirstname = "Not",
            AuthorLastname = "Author",
            AuthorCountry = "Some Country",
        };

        var authorDtoA = new AuthorListDto()
        {
            AuthorFirstname = expectedAuthorA.AuthorFirstname,
            AuthorId = expectedAuthorA.AuthorId,
            AuthorLastname = expectedAuthorA.AuthorLastname
        };
        
        var authorDtoB = new AuthorListDto()
        {
            AuthorFirstname = expectedAuthorB.AuthorFirstname,
            AuthorId = expectedAuthorB.AuthorId,
            AuthorLastname = expectedAuthorB.AuthorLastname
        };
        
        var authors = new List<Domain.Author> { expectedAuthorA, expectedAuthorB };
        var authorDtos = new List<AuthorListDto> { authorDtoA, authorDtoB };

        CancellationToken token = new CancellationToken();

        _mocks.SetupGetAuthorListAsync(authors, authorDtos, token);

        // Act
        var getAuthorCommand = new GetAuthorListQuery{PageNumber = 1, PageSize = 10};
        var result = await _handler.Handle(getAuthorCommand, token);
        
        // Assert
        result.Should().NotBeNull();
        result.Authors.First().AuthorFirstname.Should().Be("Some");
        result.Authors.Last().AuthorFirstname.Should().Be("Not");
    }
}