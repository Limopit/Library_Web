using MediatR;

namespace Library.Application.BorrowRecords.Commands.CreateBorrowRecord;

public class CreateBorrowRecordCommand: IRequest<Guid>
{
    public Guid BookId { get; set; }
    public string Email { get; set; }
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    public DateTime ReturnDate { get; set; } = DateTime.UtcNow.AddMonths(1);
}