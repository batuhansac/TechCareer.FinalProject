using DataAccess.Context;
using DataAccess.Repositories.Abstracts;
using DataAccess.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class DataAccessDependencies
{
    public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<BaseDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

        return services;
    }
}
