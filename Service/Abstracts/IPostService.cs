using Core.Shared;
using Models.DTOs.RequestDTO;
using Models.DTOs.ResponseDTO;

namespace Service.Abstracts;

public interface IPostService
{
    Response<PostResponseDTO> Add(PostAddRequest postAddRequest);
    Response<PostResponseDTO> Update(PostUpdateRequest postUpdateRequest);
    Response<PostResponseDTO> Delete(int id);
    Response<PostResponseDTO> GetById(int id);
    Response<List<PostResponseDTO>> GetAll();
    Response<List<PostResponseDTO>> GetAllByDatePosted(short dateBegin, short dateEnd);
    Response<PostDetailDTO> GetByDetailId(int id);
    Response<List<PostDetailDTO>> GetAllDetails();
    Response<List<PostDetailDTO>> GetAllDetailsByUserId(Guid userId);
    Response<List<PostDetailDTO>> GetAllDetailsByCategoryId(int categoryId);
}
