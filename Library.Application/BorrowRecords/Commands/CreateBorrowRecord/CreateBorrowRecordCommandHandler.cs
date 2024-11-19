using Library.Application.Common.Exceptions;
using Library.Application.Common.Mappings;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.BorrowRecords.Commands.CreateBorrowRecord;

public class CreateBorrowRecordCommandHandler: IRequestHandler<CreateBorrowRecordCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapperService _mapper;

    public CreateBorrowRecordCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Guid> Handle(CreateBorrowRecordCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetEntityByIdAsync(request.BookId, cancellationToken);

        if (book == null)
        {
            throw new NotFoundException(nameof(Book), request.BookId);
        }

        var record = await _unitOfWork.BorrowRecords
            .GetBorrowRecordByBookIdAsync(request.BookId, cancellationToken);

        if (record != null && record.BookIssueExpirationDate > DateTime.Now)
        {
            throw new AlreadyExistsException(nameof(BorrowRecord), record.BookIssueExpirationDate);
        }

        var user = await _unitOfWork.Users.FindUserByEmail(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        var borrowRecord = await _mapper.Map<CreateBorrowRecordCommand, BorrowRecord>(request);

        await _unitOfWork.BorrowRecords.AddEntityAsync(borrowRecord, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return borrowRecord.RecordId;
    }
}