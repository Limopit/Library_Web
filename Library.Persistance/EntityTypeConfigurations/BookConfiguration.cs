using Library.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.EntityTypeConfigurations;

public class BookConfiguration: IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(book => book.book_id);
        builder.Property(book => book.book_id)
            .HasColumnName("book_id")
            .ValueGeneratedOnAdd();
        
        builder.Property(book => book.ISBN)
            .HasColumnName("book_ISBN")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(book => book.book_name)
            .HasColumnName("book_name")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(book => book.book_genre)
            .HasColumnName("book_genre")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(book => book.book_description)
            .HasColumnName("book_descr")
            .HasMaxLength(128);

        builder.Property(book => book.book_issue_date)
            .HasColumnName("book_issue_date")
            .HasColumnType("TEXT");

        builder.Property(book => book.book_issue_expiration_date)
            .HasColumnName("book_issue_expiration_date")
            .HasColumnType("TEXT");

        builder.HasOne(book => book.author)
            .WithMany(author => author.books)
            .HasForeignKey(book => book.author_id)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}