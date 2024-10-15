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
        var book = await _unitOfWork.Books.GetEntityByIdAsync(request.BookId, cancellationToken);

        if (book == null)
        {
            throw new NotFoundException(nameof(Book), request.BookId);
        }

        var record = await _unitOfWork.BorrowRecords
            .GetBorrowRecordByBookIdAsync(request.BookId, cancellationToken);

        if (record != null && record.book_issue_expiration_date > DateTime.Now)
        {
            throw new Exception("Book is issued already, come back later");
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

        await _unitOfWork.BorrowRecords.AddEntityAsync(borrowRecord, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return borrowRecord.recordId;
    }
}