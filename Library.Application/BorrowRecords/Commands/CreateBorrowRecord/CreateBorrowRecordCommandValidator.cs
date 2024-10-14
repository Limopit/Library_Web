using FluentValidation;

namespace Library.Application.BorrowRecords.Commands.CreateBorrowRecord;

public class CreateBorrowRecordCommandValidator: AbstractValidator<CreateBorrowRecordCommand>
{
    public CreateBorrowRecordCommandValidator()
    {
        RuleFor(command => command.Email).NotEmpty();
        RuleFor(command => command.BookId).NotEmpty();
        RuleFor(command => command.ReturnDate).GreaterThan(command => command.IssueDate);
    }
}