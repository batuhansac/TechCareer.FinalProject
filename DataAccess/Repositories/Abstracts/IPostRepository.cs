using Core.Persistence.Repositories;
using Models.DTOs.ResponseDTO;
using Models.Entities;

namespace DataAccess.Repositories.Abstracts;

public interface IPostRepository : IEntityRepository<Post, int>
{
    List<PostDetailDTO> GetAllPostDetails();
    List<PostDetailDTO> GetDetailsByUserId(Guid userId);
    List<PostDetailDTO> GetDetailsByCategoryId(int categoryId);
    PostDetailDTO GetPostDetail(int id);
}
