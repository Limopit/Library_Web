using Library.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.EntityTypeConfigurations;

public class BorrowRecordConfiguration: IEntityTypeConfiguration<BorrowRecord>
{
    public void Configure(EntityTypeBuilder<BorrowRecord> builder)
    {
        builder.HasKey(record => record.RecordId);
        builder.Property(record => record.RecordId)
            .HasColumnName("record_id")
            .ValueGeneratedOnAdd();
        
        builder.Property(record => record.BookId)
            .HasColumnName("book_id")
            .IsRequired();

        builder.Property(record => record.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(record => record.BookIssueDate)
            .HasColumnName("book_issue_date")
            .HasColumnType("TEXT");

        builder.Property(record => record.BookIssueExpirationDate)
            .HasColumnName("book_issue_expiration_date")
            .HasColumnType("TEXT");

        builder.HasOne(record => record.Book)
            .WithMany(book => book.BorrowRecords)
            .HasForeignKey(record => record.BookId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(record => record.User)
            .WithMany(user => user.BorrowRecords)
            .HasForeignKey(record => record.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}