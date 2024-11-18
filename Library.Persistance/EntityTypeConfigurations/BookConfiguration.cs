using Library.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.EntityTypeConfigurations;

public class BookConfiguration: IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(book => book.BookId);
        builder.Property(book => book.BookId)
            .HasColumnName("book_id")
            .ValueGeneratedOnAdd();
        
        builder.Property(book => book.ISBN)
            .HasColumnName("book_ISBN")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(book => book.BookName)
            .HasColumnName("book_name")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(book => book.BookGenre)
            .HasColumnName("book_genre")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(book => book.BookDescription)
            .HasColumnName("book_descr")
            .HasMaxLength(128);

        builder.HasOne(book => book.Author)
            .WithMany(author => author.Books)
            .HasForeignKey(book => book.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(book => book.BorrowRecords)
            .WithOne(record => record.Book)
            .HasForeignKey(record => record.BookId);

    }
}