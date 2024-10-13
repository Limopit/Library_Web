using MediatR;

namespace Library.Application.Books.Commands.AddImage;

public class AddImageCommand: IRequest
{
    public Guid BookId { get; set; }
    public string ImagePath { get; set; }
}