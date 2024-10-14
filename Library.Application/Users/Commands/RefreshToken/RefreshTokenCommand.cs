using Library.Application.Interfaces;
using MediatR;

namespace Library.Application.Tokens.RefreshToken;

public class RefreshTokenCommand: IRequest<(string, string)>
{
    public string refreshToken { get; set; }
}