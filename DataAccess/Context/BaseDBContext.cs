using System.Reflection;
using DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccess.Context;

public class BaseDBContext : DbContext
{
    public BaseDBContext(DbContextOptions<BaseDBContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
}
