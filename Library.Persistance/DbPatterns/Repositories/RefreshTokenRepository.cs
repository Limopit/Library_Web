using Library.Application.Interfaces.Repositories;
using Library.Domain;
using Microsoft.EntityFrameworkCore;

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

    public async Task<RefreshToken?> ValidateRefreshToken(string refreshToken)
    {
        return await _libraryDbContext.RefreshTokens
            .Where(r => r.Token == refreshToken && r.Expires > DateTime.UtcNow)
            .FirstOrDefaultAsync();
    }

    public async Task<RefreshToken?> RevokeToken(string refreshToken)
    {
        return await _libraryDbContext.RefreshTokens
            .SingleOrDefaultAsync(t => t.Token == refreshToken);
    }
}