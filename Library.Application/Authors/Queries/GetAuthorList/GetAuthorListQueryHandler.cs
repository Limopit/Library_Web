using AutoMapper;
using Library.Application.Interfaces;
using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorList;

public class GetAuthorListQueryHandler: IRequestHandler<GetAuthorListQuery, AuthorListVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAuthorListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorListVm> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
    {
        var authors =
            await _unitOfWork.Authors.GetPaginatedEntityListAsync(request.PageNumber, request.PageSize,
                cancellationToken);

        var authorList = _mapper.Map<IList<AuthorListDto>>(authors);
        
        return new AuthorListVm { Authors = authorList };
    }
}