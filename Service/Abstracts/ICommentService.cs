using Core.Shared;
using Models.DTOs.RequestDTO;
using Models.DTOs.ResponseDTO;

namespace Service.Abstracts;

public interface ICommentService
{
    Response<CommentResponseDTO> Add(CommentAddRequest commentAddRequest);
    Response<CommentResponseDTO> Update(CommentUpdateRequest commentUpdateRequest);
    Response<CommentResponseDTO> Delete(int id);
    Response<CommentResponseDTO> GetById(int id);
    Response<List<CommentResponseDTO>> GetAll();
    Response<List<CommentResponseDTO>> GetAllByDatePosted(short dateBegin, short dateEnd);
    Response<CommentDetailDTO> GetByDetailId(int id);
    Response<List<CommentDetailDTO>> GetAllDetails();
    Response<List<CommentDetailDTO>> GetAllDetailsByUserId(Guid userId);
    Response<List<CommentDetailDTO>> GetAllDetailsByPostId(int postId);
}
