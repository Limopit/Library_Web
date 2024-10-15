using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandHandler: IRequestHandler<CreateAuthorCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthorCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author()
        {
            author_id = Guid.NewGuid(),
            author_firstname = request.author_firstname,
            author_lastname = request.author_lastname,
            author_birthday = request.author_birthday,
            author_country = request.author_country,
            books = request.books
        };

        await _unitOfWork.Authors.AddEntityAsync(author, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return author.author_id;
    }
}