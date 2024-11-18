using Library.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.EntityTypeConfigurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(prop => prop.FirstName)
            .HasMaxLength(64);

        builder.Property(prop => prop.LastName)
            .HasMaxLength(64);
        
        builder.Property(prop => prop.Birthday)
            .HasColumnType("TEXT")
            .IsRequired();
        
        builder.HasMany(user => user.BorrowRecords)
            .WithOne(record => record.User)
            .HasForeignKey(record => record.UserId);
        
        builder.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}