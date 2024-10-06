using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandHandler: IRequestHandler<CreateAuthorCommand, Guid>
{
    private readonly ILibraryDBContext _libraryDbContext;

    public CreateAuthorCommandHandler(ILibraryDBContext libraryDbContext)
        => _libraryDbContext = libraryDbContext;
    
    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author()
        {
            author_firstname = request.author_firstname,
            author_lastname = request.author_lastname,
            author_birthday = request.author_birthday,
            author_country = request.author_country,
            books = request.books
        };

        await _libraryDbContext.authors.AddAsync(author, cancellationToken);
        await _libraryDbContext.SaveChangesAsync(cancellationToken);
        
        return author.author_id;
    }
}