using FluentValidation;

namespace Library.Application.Books.Commands.AddImage;

public class AddImageCommandValidator: AbstractValidator<AddImageCommand>
{
    private readonly string[] _allowedExtensions = { ".jpg", ".png", ".jpeg" };
    
    public AddImageCommandValidator()
    {
        RuleFor(command => command.ImagePath).NotEmpty().Must(HaveValidExtension);
    }
    
    private bool HaveValidExtension(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        return _allowedExtensions.Contains(extension);
    }
}