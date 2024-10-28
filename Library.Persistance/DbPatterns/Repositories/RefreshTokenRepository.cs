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

    public async Task<RefreshToken?> ValidateRefreshToken(string refreshToken)
    {
        return _libraryDbContext.RefreshTokens
            .Where(r => r.Token == refreshToken)
            .AsEnumerable()
            .FirstOrDefault(r => r.IsActive);
    }

    public async Task<RefreshToken?> RevokeToken(string refreshToken)
    {
        return _libraryDbContext.RefreshTokens
            .SingleOrDefault(t => t.Token == refreshToken);
    }
}