using System.Diagnostics;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandHandler: IRequestHandler<DeleteAuthorCommand>
{
    private readonly ILibraryDBContext _libraryDbContext;

    public DeleteAuthorCommandHandler(ILibraryDBContext libraryDbContext)
        => _libraryDbContext = libraryDbContext;
    
    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _libraryDbContext.authors
            .FindAsync(new object?[] { request.author_id }, cancellationToken);

        if (entity == null || entity.author_id != request.author_id)
        {
            throw new NotFoundException(nameof(Author), request.author_id);
        }

        _libraryDbContext.authors.Remove(entity);
        
        await _libraryDbContext.SaveChangesAsync(cancellationToken);
    }
    
    
}