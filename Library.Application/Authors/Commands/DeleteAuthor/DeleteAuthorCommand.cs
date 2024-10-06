using MediatR;

namespace Library.Application.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommand: IRequest
{
    public Guid author_id { get; set; }
}