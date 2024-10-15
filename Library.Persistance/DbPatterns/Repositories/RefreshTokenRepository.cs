using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Domain;

namespace Library.Persistance.DbPatterns.Repositories;

public class RefreshTokenRepository: IRefreshTokenRepository
{
    private readonly LibraryDBContext _libraryDbContext;

    public RefreshTokenRepository(LibraryDBContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }
    
    public async Task SaveRefreshToken(RefreshToken token, CancellationToken cancellationToken)
    {
        await _libraryDbContext.RefreshTokens.AddAsync(token, cancellationToken);
    }

    public string ValidateRefreshToken(string refreshToken)
    {
        var token = _libraryDbContext.RefreshTokens
            .Where(r => r.Token == refreshToken)
            .AsEnumerable()
            .FirstOrDefault(r => r.IsActive);

        if (token == null)
        {
            return null;
        }

        return token.UserId;
    }

    public void RevokeToken(string refreshToken)
    {
        var token = _libraryDbContext.RefreshTokens
            .SingleOrDefault(t => t.Token == refreshToken);

        if (token == null)
        {
            throw new NotFoundException(nameof(RefreshToken), refreshToken);
        }

        token.Revoked = DateTime.UtcNow;
    }
}