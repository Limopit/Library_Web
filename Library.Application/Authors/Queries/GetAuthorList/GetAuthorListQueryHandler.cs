using Library.Application.Common.Mappings;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorList;

public class GetAuthorListQueryHandler: IRequestHandler<GetAuthorListQuery, AuthorListVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapperService _mapper;

    public GetAuthorListQueryHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorListVm> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
    {
        var authors =
            await _unitOfWork.Authors.GetPaginatedEntityListAsync(request.PageNumber, request.PageSize,
                cancellationToken);

        var authorList = await _mapper.Map<List<Author>, IList<AuthorListDto>>(authors);
        
        return new AuthorListVm { Authors = authorList };
    }
}