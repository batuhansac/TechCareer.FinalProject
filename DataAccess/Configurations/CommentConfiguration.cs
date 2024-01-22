using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DataAccess.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comment_db").HasKey(c => c.Id);
        builder.Property(c =>  c.Id).HasColumnName("comment_id").IsRequired();
        builder.Property(c => c.Content).HasColumnName("comment_content");
        builder.Property(c => c.DatePosted).HasColumnName("comment_dateposted");

        builder.HasOne(c => c.User).WithMany(u => u.Comments).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.Post).WithMany(p => p.Comments).HasForeignKey(c => c.PostId);
    }
}
