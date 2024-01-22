using Core.Shared;
using Models.DTOs.RequestDTO;
using Models.DTOs.ResponseDTO;

namespace Service.Abstracts;

public interface IUserService
{
    Response<UserResponseDTO> Add(UserAddRequest userAddRequest);
    Response<UserResponseDTO> Update(UserUpdateRequest userUpdateRequest);
    Response<UserResponseDTO> Delete(Guid id);
    Response<UserResponseDTO> GetById(Guid id);
    Response<List<UserResponseDTO>> GetAll();
}
