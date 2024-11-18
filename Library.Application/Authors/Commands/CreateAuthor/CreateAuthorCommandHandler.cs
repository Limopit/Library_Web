using Library.Application.Common.Mappings;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandHandler: IRequestHandler<CreateAuthorCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapperService _mapper;


    public CreateAuthorCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _mapper.Map<CreateAuthorCommand, Author>(request);

        await _unitOfWork.Authors.AddEntityAsync(author, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return author.AuthorId;
    }
}