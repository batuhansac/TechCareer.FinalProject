using Core.CrossCuttingConcerns.Exceptions;
using Core.Shared;
using DataAccess.Repositories.Abstracts;
using Models.DTOs.RequestDTO;
using Models.DTOs.ResponseDTO;
using Models.Entities;
using Service.Abstracts;
using Service.BusinessRules.Abstracts;

namespace Service.Concretes;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryRules _categoryRules;

    public CategoryService(ICategoryRepository categoryRepository, ICategoryRules categoryRules)
    {
        _categoryRepository = categoryRepository;
        _categoryRules = categoryRules;
    }
    public Response<CategoryResponseDTO> Add(CategoryAddRequest categoryAddRequest)
    {
        try
        {
            Category category = categoryAddRequest;

            _categoryRules.CategoryNameMustBeUnique(category.Name);
            _categoryRules.CategoryNameMustBeValid(category.Name);

            _categoryRepository.Add(category);

            CategoryResponseDTO categoryResponseDTO = category;
            
            return new Response<CategoryResponseDTO>()
            {
                Data = categoryResponseDTO,
                Message = "Kategori başarıyla oluşturuldu.",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
        catch (BusinessException ex)
        {
            return new Response<CategoryResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<CategoryResponseDTO> Delete(int id)
    {
        try
        {
            _categoryRules.CategoryIsPresent(id);

            Category category = _categoryRepository.GetById(id);

            _categoryRepository.Delete(category);

            CategoryResponseDTO categoryResponseDTO = category;

            return new Response<CategoryResponseDTO>()
            {
                Data= categoryResponseDTO,
                Message = "Kategori başarıyla silindi.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<CategoryResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<List<CategoryResponseDTO>> GetAll()
    {
        List<Category> categories = _categoryRepository.GetAll();

        List<CategoryResponseDTO> categoryResponseDTOs = categories.Select(c => (CategoryResponseDTO)c).ToList();

        return new Response<List<CategoryResponseDTO>>()
        {
            Data = categoryResponseDTOs,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }

    public Response<CategoryResponseDTO> GetById(int id)
    {
        try
        {
            _categoryRules.CategoryIsPresent(id);

            Category? category = _categoryRepository.GetById(id);

            CategoryResponseDTO categoryResponseDTO = category;

            return new Response<CategoryResponseDTO>()
            {
                Data = categoryResponseDTO,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<CategoryResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }

    public Response<CategoryResponseDTO> Update(CategoryUpdateRequest categoryUpdateRequest)
    {
        try
        {
            Category category = categoryUpdateRequest;

            _categoryRules.CategoryNameMustBeValid(category.Name);

            _categoryRepository.Update(category);

            CategoryResponseDTO categoryResponseDTO = category;

            return new Response<CategoryResponseDTO>()
            {
                Data = categoryResponseDTO,
                Message = "Kategori başarıyla güncellendi.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (BusinessException ex)
        {
            return new Response<CategoryResponseDTO>()
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
    }
}
