using Library.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.EntityTypeConfigurations;

public class BorrowRecordConfiguration: IEntityTypeConfiguration<BorrowRecord>
{
    public void Configure(EntityTypeBuilder<BorrowRecord> builder)
    {
        builder.HasKey(record => record.recordId);
        builder.Property(record => record.recordId)
            .HasColumnName("record_id")
            .ValueGeneratedOnAdd();
        
        builder.Property(record => record.bookId)
            .HasColumnName("book_id")
            .IsRequired();

        builder.Property(record => record.userId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(record => record.book_issue_date)
            .HasColumnName("book_issue_date")
            .HasColumnType("TEXT");

        builder.Property(record => record.book_issue_expiration_date)
            .HasColumnName("book_issue_expiration_date")
            .HasColumnType("TEXT");

        builder.HasOne(record => record.book)
            .WithMany(book => book.borrowRecords)
            .HasForeignKey(record => record.bookId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(record => record.user)
            .WithMany(user => user.borrowRecords)
            .HasForeignKey(record => record.userId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}