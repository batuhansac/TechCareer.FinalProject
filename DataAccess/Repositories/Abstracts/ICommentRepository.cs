using Core.Persistence.Repositories;
using Models.DTOs.ResponseDTO;
using Models.Entities;

namespace DataAccess.Repositories.Abstracts;

public interface ICommentRepository : IEntityRepository<Comment, int>
{
    List<CommentDetailDTO> GetAllCommentDetails();
    List<CommentDetailDTO> GetDetailsByUserId(Guid userId);
    List<CommentDetailDTO> GetDetailsByPostId(int postId);
    CommentDetailDTO GetCommentDetail(int id);

}
