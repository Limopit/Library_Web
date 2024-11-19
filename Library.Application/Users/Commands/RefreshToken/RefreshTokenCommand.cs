using MediatR;

namespace Library.Application.Users.Commands.RefreshToken;

public class RefreshTokenCommand: IRequest<string>
{
    public string RefreshToken { get; set; }
}