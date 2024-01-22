using Core.Persistence.Repositories;
using DataAccess.Context;
using DataAccess.Repositories.Abstracts;
using Models.DTOs.ResponseDTO;
using Models.Entities;

namespace DataAccess.Repositories.Concretes;

public class PostRepository : EFRepositoryBase<BaseDBContext, Post, int>, IPostRepository
{
    public PostRepository(BaseDBContext baseDBContext) : base(baseDBContext)
    {
        
    }

    public List<PostDetailDTO> GetAllPostDetails()
    {
        var query = Context.Posts.Join(
            Context.Users,
            p => p.UserId,
            u => u.Id,
            (p, u) => new { p, u })
            .Join(
            Context.Categories,
            pu => pu.p.CategoryId,
            c => c.Id,
            (pu, c) => new PostDetailDTO
            {
                Id = pu.p.Id,
                Title = pu.p.Title,
                Content = pu.p.Content,
                DatePosted = pu.p.DatePosted,
                UserName = pu.u.UserName,
                CategoryName = c.Name
            }).ToList();

        return query;
    }

    public List<PostDetailDTO> GetDetailsByCategoryId(int categoryId)
    {
        var query = Context.Posts.Where(x => x.CategoryId == categoryId).Join(
            Context.Users,
            p => p.UserId,
            u => u.Id,
            (p, u) => new { p, u })
            .Join(
            Context.Categories,
            pu => pu.p.CategoryId,
            c => c.Id,
            (pu, c) => new PostDetailDTO
            {
                Id = pu.p.Id,
                Title = pu.p.Title,
                Content = pu.p.Content,
                DatePosted = pu.p.DatePosted,
                UserName = pu.u.UserName,
                CategoryName = c.Name
            }).ToList();

        return query;
    }

    public List<PostDetailDTO> GetDetailsByUserId(Guid userId)
    {
        var query = Context.Posts.Where(x => x.UserId == userId).Join(
            Context.Users,
            p => p.UserId,
            u => u.Id,
            (p, u) => new { p, u })
            .Join(
            Context.Categories,
            pu => pu.p.CategoryId,
            c => c.Id,
            (pu, c) => new PostDetailDTO
            {
                Id = pu.p.Id,
                Title = pu.p.Title,
                Content = pu.p.Content,
                DatePosted = pu.p.DatePosted,
                UserName = pu.u.UserName,
                CategoryName = c.Name
            }).ToList();

        return query;
    }

    public PostDetailDTO GetPostDetail(int id)
    {
        var query = Context.Posts.Join(
            Context.Users,
            p => p.UserId,
            u => u.Id,
            (p, u) => new { p, u })
            .Join(
            Context.Categories,
            pu => pu.p.CategoryId,
            c => c.Id,
            (pu, c) => new PostDetailDTO
            {
                Id = pu.p.Id,
                Title = pu.p.Title,
                Content = pu.p.Content,
                DatePosted = pu.p.DatePosted,
                UserName = pu.u.UserName,
                CategoryName = c.Name
            }).SingleOrDefault(x => x.Id == id);

        return query;
    }
}
