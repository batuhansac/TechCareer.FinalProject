using Microsoft.Extensions.DependencyInjection;
using Service.Abstracts;
using Service.BusinessRules.Abstracts;
using Service.BusinessRules.Concretes;
using Service.Concretes;

namespace Service;

public static class ServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<ICategoryRules, CategoryRules>();
        services.AddScoped<ICommentRules, CommentRules>();
        services.AddScoped<IPostRules, PostRules>();
        services.AddScoped<IUserRules, UserRules>();

        return services;
    }
}
