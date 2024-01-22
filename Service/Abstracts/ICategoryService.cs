using Core.Shared;
using Models.DTOs.RequestDTO;
using Models.DTOs.ResponseDTO;

namespace Service.Abstracts;

public interface ICategoryService
{
    Response<CategoryResponseDTO> Add(CategoryAddRequest categoryAddRequest);
    Response<CategoryResponseDTO> Update(CategoryUpdateRequest categoryUpdateRequest);
    Response<CategoryResponseDTO> Delete(int id);
    Response<CategoryResponseDTO> GetById(int id);
    Response<List<CategoryResponseDTO>> GetAll();
}