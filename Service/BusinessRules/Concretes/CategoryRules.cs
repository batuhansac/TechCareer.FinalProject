using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstracts;
using Service.BusinessRules.Abstracts;

namespace Service.BusinessRules.Concretes;

public class CategoryRules : ICategoryRules
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryRules(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public void CategoryIsPresent(int id)
    {
        var category = _categoryRepository.GetById(id);

        if (category == null)
        {
            throw new BusinessException($"ID değeri {id} olan bir kategori bulunamadı.");
        }
    }

    public void CategoryNameMustBeUnique(string categoryName)
    {
        var category = _categoryRepository.GetByFilter(x => x.Name == categoryName);

        if (category != null)
        {
            throw new BusinessException("Kategori ismi benzersiz olmalıdır.");
        }
    }

    public void CategoryNameMustBeValid(string categoryName)
    {
        var category = _categoryRepository.GetByFilter(x => x.Name == categoryName);

        if (string.IsNullOrWhiteSpace(categoryName))
        {
            throw new BusinessException("Geçerli bir kategori ismi girilmelidir.");
        }
    }
}
