using Library.Application.Interfaces;
using Library.Domain;
using Library.Persistance.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance;

public sealed class LibraryDBContext: DbContext, ILibraryDBContext
{
    public DbSet<Book> books { get; set; }
    public DbSet<Author> authors { get; set; }

    public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}