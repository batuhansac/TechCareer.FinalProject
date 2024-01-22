using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category_db").HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("category_id").IsRequired();
        builder.Property(c => c.Name).HasColumnName("category_name");

        builder.HasMany(c => c.Posts).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);
    }
}
