using Library.Application.Interfaces;
using Library.Domain;
using Library.Persistance.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance;

public sealed class LibraryDBContext: IdentityDbContext<User>, ILibraryDBContext
{
    public DbSet<Book> books { get; set; }
    public DbSet<Author> authors { get; set; }
    public DbSet<User> Users { get; set; }

    public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}