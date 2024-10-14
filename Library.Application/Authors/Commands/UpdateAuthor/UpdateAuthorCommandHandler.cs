using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetAuthorByIdAsync(request.author_id, cancellationToken);

        if (author == null || author.author_id != request.author_id)
        {
            throw new NotFoundException(nameof(Author), request.author_id);
        }

        author.author_firstname = request.author_firstname;
        author.author_lastname = request.author_lastname;
        author.author_birthday = request.author_birthday;
        author.author_country = request.author_country;
        author.books = request.books;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}