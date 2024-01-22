using Core.Persistence.Repositories;
using DataAccess.Context;
using DataAccess.Repositories.Abstracts;
using Models.DTOs.ResponseDTO;
using Models.Entities;

namespace DataAccess.Repositories.Concretes;

public class CommentRepository : EFRepositoryBase<BaseDBContext, Comment, int>, ICommentRepository
{
    public CommentRepository(BaseDBContext baseDBContext) : base(baseDBContext)
    {
        
    }

    public List<CommentDetailDTO> GetAllCommentDetails()
    {
        var query = Context.Comments.Join(
            Context.Users,
            c => c.UserId,
            u => u.Id,
            (c, u) => new { c, u })
            .Join(
            Context.Posts,
            cu => cu.c.PostId,
            p => p.Id,
            (cu, p) => new CommentDetailDTO
            {
                Id = cu.c.Id,
                Content = cu.c.Content,
                DatePosted = cu.c.DatePosted,
                UserName = cu.u.UserName,
                PostTitle = p.Title
            }).ToList();

        return query;
    }

    public CommentDetailDTO GetCommentDetail(int id)
    {
        var query = Context.Comments.Join(
            Context.Users,
            c => c.UserId,
            u => u.Id,
            (c, u) => new { c, u })
            .Join(
            Context.Posts,
            cu => cu.c.PostId,
            p => p.Id,
            (cu, p) => new CommentDetailDTO
            {
                Id = cu.c.Id,
                Content = cu.c.Content,
                DatePosted = cu.c.DatePosted,
                UserName = cu.u.UserName,
                PostTitle = p.Title
            }).SingleOrDefault(x => x.Id == id);

        return query;
    }

    public List<CommentDetailDTO> GetDetailsByPostId(int postId)
    {
        var query = Context.Comments.Where(x => x.PostId == postId).Join(
            Context.Users,
            c => c.UserId,
            u => u.Id,
            (c, u) => new { c, u })
            .Join(
            Context.Posts,
            cu => cu.c.PostId,
            p => p.Id,
            (cu, p) => new CommentDetailDTO
            {
                Id = cu.c.Id,
                Content = cu.c.Content,
                DatePosted = cu.c.DatePosted,
                UserName = cu.u.UserName,
                PostTitle = p.Title
            }).ToList();

        return query;
    }

    public List<CommentDetailDTO> GetDetailsByUserId(Guid userId)
    {
        var query = Context.Comments.Where(x => x.UserId == userId).Join(
            Context.Users,
            c => c.UserId,
            u => u.Id,
            (c, u) => new { c, u })
            .Join(
            Context.Posts,
            cu => cu.c.PostId,
            p => p.Id,
            (cu, p) => new CommentDetailDTO
            {
                Id = cu.c.Id,
                Content = cu.c.Content,
                DatePosted = cu.c.DatePosted,
                UserName = cu.u.UserName,
                PostTitle = p.Title
            }).ToList();

        return query;
    }
}
