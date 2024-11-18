using Library.Application.Common.Exceptions;
using Library.Application.Common.Mappings;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapperService _mapper;

    public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetEntityByIdAsync(request.AuthorId, cancellationToken);

        if (author == null || author.AuthorId != request.AuthorId)
        {
            throw new NotFoundException(nameof(Author), request.AuthorId);
        }
        
        await _mapper.Update(request, author);
        await _unitOfWork.Authors.UpdateAsync(author);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}