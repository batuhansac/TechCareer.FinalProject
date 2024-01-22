using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DataAccess.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Post_db").HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("post_id").IsRequired();
        builder.Property(p => p.Title).HasColumnName("post_title");
        builder.Property(p => p.Content).HasColumnName("post_content");
        builder.Property(p => p.DatePosted).HasColumnName("post_dateposted");

        builder.HasOne(p => p.User).WithMany(u => u.Posts).HasForeignKey(p => p.UserId);
        builder.HasMany(p => p.Comments).WithOne(c => c.Post).HasForeignKey(c => c.PostId);
        builder.HasOne(p => p.Category).WithMany(c => c.Posts).HasForeignKey(p => p.CategoryId);
    }
}
