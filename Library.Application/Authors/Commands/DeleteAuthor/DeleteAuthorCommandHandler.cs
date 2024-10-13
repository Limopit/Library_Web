using System.Diagnostics;
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
        var author = await _unitOfWork.Authors.GetAuthorByIdAsync(request.author_id, cancellationToken);

        if (author == null || author.author_id != request.author_id)
        {
            throw new NotFoundException(nameof(Author), request.author_id);
        }

        await _unitOfWork.Authors.DeleteAuthorAsync(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    
}