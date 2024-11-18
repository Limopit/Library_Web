using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandHandler: IRequestHandler<DeleteAuthorCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetEntityByIdAsync(request.AuthorId, cancellationToken);

        if (author == null || author.AuthorId != request.AuthorId)
        {
            throw new NotFoundException(nameof(Author), request.AuthorId);
        }

        await _unitOfWork.Authors.RemoveEntity(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    
}