using MediatR;

namespace Library.Application.Users.Commands.RefreshToken;

public class RefreshTokenCommand: IRequest<(string, string)>
{
    public string refreshToken { get; set; }
}