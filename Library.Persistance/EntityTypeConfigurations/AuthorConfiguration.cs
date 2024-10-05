using Library.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.EntityTypeConfigurations;

public class AuthorConfiguration: IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(author => author.author_id);
        builder.Property(author => author.author_id)
            .HasColumnName("author_id")
            .ValueGeneratedOnAdd();
        
        builder.Property(author => author.author_firstname)
            .HasColumnName("author_firstname")
            .HasMaxLength(32)
            .IsRequired();
        
        builder.Property(author => author.author_lastname)
            .HasColumnName("author_lastname")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(author => author.author_birthday)
            .HasColumnName("author_birthday")
            .HasMaxLength(32)
            .HasColumnType("TEXT");

        builder.Property(author => author.author_country)
            .HasColumnName("author_country")
            .HasMaxLength(32);
        
        builder.HasMany(author => author.books)
            .WithOne(book => book.author)
            .HasForeignKey(book => book.author_id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}