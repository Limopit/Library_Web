using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.BorrowRecords.Commands.CreateBorrowRecord;

public class CreateBorrowRecordCommandHandler: IRequestHandler<CreateBorrowRecordCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBorrowRecordCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> Handle(CreateBorrowRecordCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetBookByIdAsync(request.BookId, cancellationToken);

        if (book == null)
        {
            throw new NotFoundException(nameof(Book), request.BookId);
        }

        var user = await _unitOfWork.Users.FindUserByEmail(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }
        
        var borrowRecord = new BorrowRecord
        {
            bookId = request.BookId,
            userId = user.Id,
            book_issue_date = request.IssueDate,
            book_issue_expiration_date = request.ReturnDate
        };

        await _unitOfWork.BorrowRecords.AddRecordAsync(borrowRecord, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return borrowRecord.recordId;
    }
}