using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.DbPatterns.Repositories;

public class BaseRepository<T>: IBaseRepository<T> where T: class
{
    protected readonly LibraryDBContext _libraryDbContext;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(LibraryDBContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
        _dbSet = _libraryDbContext.Set<T>();
    }

    public async Task<T?> GetEntityByIdAsync(Guid id, CancellationToken token)
    {
        return await _dbSet.FindAsync(new object?[] { id }, token);
    }

    public async Task AddEntityAsync(T entity, CancellationToken token)
    {
        await _dbSet.AddAsync(entity, token);
    }

    public async Task RemoveEntity(T entity)
    {
        _dbSet.Remove(entity);
    }
}