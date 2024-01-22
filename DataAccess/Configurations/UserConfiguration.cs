using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User_db").HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("user_id").IsRequired();
        builder.Property(u => u.UserName).HasColumnName("user_username");
        builder.Property(u => u.Email).HasColumnName("user_email");

        builder.HasMany(u => u.Posts).WithOne(p => p.User).HasForeignKey(p => p.UserId);
        builder.HasMany(u => u.Comments).WithOne(p => p.User).HasForeignKey(p => p.UserId);
    }
}
