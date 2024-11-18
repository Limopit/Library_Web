using Library.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.EntityTypeConfigurations;

public class AuthorConfiguration: IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(author => author.AuthorId);
        builder.Property(author => author.AuthorId)
            .HasColumnName("author_id")
            .ValueGeneratedOnAdd();
        
        builder.Property(author => author.AuthorFirstname)
            .HasColumnName("author_firstname")
            .HasMaxLength(32)
            .IsRequired();
        
        builder.Property(author => author.AuthorLastname)
            .HasColumnName("author_lastname")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(author => author.AuthorBirthday)
            .HasColumnName("author_birthday")
            .HasMaxLength(32)
            .HasColumnType("TEXT");

        builder.Property(author => author.AuthorCountry)
            .HasColumnName("author_country")
            .HasMaxLength(32);
        
        builder.HasMany(author => author.Books)
            .WithOne(book => book.Author)
            .HasForeignKey(book => book.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}