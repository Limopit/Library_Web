using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly ILibraryDBContext _libraryDbContext;

    public UpdateAuthorCommandHandler(ILibraryDBContext libraryDbContext)
        => _libraryDbContext = libraryDbContext;
    
    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _libraryDbContext.authors.FirstOrDefaultAsync(author => author.author_id == request.author_id,
                cancellationToken);

        if (entity == null || entity.author_id != request.author_id)
        {
            throw new NotFoundException(nameof(Author), request.author_id);
        }

        entity.author_firstname = request.author_firstname;
        entity.author_lastname = request.author_lastname;
        entity.author_birthday = request.author_birthday;
        entity.author_country = request.author_country;
        entity.books = request.books;
        
        await _libraryDbContext.SaveChangesAsync(cancellationToken);
        
    }
}