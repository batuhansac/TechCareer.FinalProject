using System.Net;
using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstracts;
using Models.DTOs.RequestDTO;
using Models.DTOs.ResponseDTO;
using Models.Entities;
using Moq;
using Service.BusinessRules.Abstracts;
using Service.Concretes;

namespace Service.UnitTest.Categories;

public class CategoryServiceTests
{
    private CategoryService _categoryService;
    private Mock<ICategoryRepository> _mockRepository;
    private Mock<ICategoryRules> _mockRules;

    private CategoryAddRequest categoryAddRequest;
    private CategoryUpdateRequest categoryUpdateRequest;
    private Category category;
    private CategoryResponseDTO categoryResponseDTO;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<ICategoryRepository>();
        _mockRules = new Mock<ICategoryRules>();
        _categoryService = new CategoryService(_mockRepository.Object, _mockRules.Object);
        categoryAddRequest = new CategoryAddRequest(Name: "Test");
        categoryUpdateRequest = new CategoryUpdateRequest(Id: new int(), Name: "Test");
        category = new Category
        {
            Id = new int(),
            Name = "Test"
        };
        categoryResponseDTO = new CategoryResponseDTO(Id: new int(), Name: "Test");
    }

    [Test]
    public void Add_WhenNameIsUniqueAndValid_ReturnsCreated()
    {
        _mockRules.Setup(c => c.CategoryNameMustBeUnique(categoryAddRequest.Name));
        _mockRules.Setup(c => c.CategoryNameMustBeValid(categoryAddRequest.Name));
        _mockRepository.Setup(c => c.Add(category));

        var result = _categoryService.Add(categoryAddRequest);

        Assert.AreEqual(result.Data, categoryResponseDTO);
        Assert.AreEqual(result.Message, "Kategori başarıyla oluşturuldu.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
    }

    [Test]
    public void Add_WhenNameIsNotUnique_ReturnsBadRequest()
    {
        _mockRules.Setup(c => c.CategoryNameMustBeUnique(categoryAddRequest.Name)).Throws(new BusinessException("Kategori ismi benzersiz olmalıdır."));

        var result = _categoryService.Add(categoryAddRequest);

        Assert.AreEqual(result.Message, "Kategori ismi benzersiz olmalıdır.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Add_WhenNameIsNotValid_ReturnsBadRequest()
    {
        _mockRules.Setup(c => c.CategoryNameMustBeValid(categoryAddRequest.Name)).Throws(new BusinessException("Geçerli bir kategori ismi girilmelidir."));

        var result = _categoryService.Add(categoryAddRequest);

        Assert.AreEqual(result.Message, "Geçerli bir kategori ismi girilmelidir.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Delete_WhenCategoryIsPresent_ReturnsOk()
    {
        int id = new int();

        _mockRules.Setup(c => c.CategoryIsPresent(id));
        _mockRepository.Setup(c => c.GetById(id, null)).Returns(category);
        _mockRepository.Setup(c => c.Delete(category));

        var result = _categoryService.Delete(id);

        Assert.AreEqual(result.Data, categoryResponseDTO);
        Assert.AreEqual(result.Message, "Kategori başarıyla silindi.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void Delete_WhenCategoryIsNotPresent_ReturnsBadRequest()
    {
        int id = new int();

        _mockRules.Setup(c => c.CategoryIsPresent(id)).Throws(new BusinessException($"ID değeri {id} olan bir kategori bulunamadı."));

        var result = _categoryService.Delete(id);

        Assert.AreEqual(result.Message, $"ID değeri {id} olan bir kategori bulunamadı.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void GetAll_ReturnsOk()
    {
        var categories = new List<Category>()
        {
            category
        };

        var categoryResponseDTOs = new List<CategoryResponseDTO>()
        {
            categoryResponseDTO
        };

        _mockRepository.Setup(c => c.GetAll(null, null)).Returns(categories);

        var result = _categoryService.GetAll();

        Assert.AreEqual(result.Data, categoryResponseDTOs);
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void GetById_WhenCategoryIsPresent_ReturnsOk()
    {
        int id = new int();

        _mockRules.Setup(u => u.CategoryIsPresent(id));
        _mockRepository.Setup(u => u.GetById(id, null)).Returns(category);

        var result = _categoryService.GetById(id);

        Assert.AreEqual(result.Data, categoryResponseDTO);
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void GetById_WhenCategoryIsNotPresent_ReturnsBadRequest()
    {
        int id = new int();

        _mockRules.Setup(u => u.CategoryIsPresent(id)).Throws(new BusinessException($"ID değeri {id} olan bir kategori bulunamadı."));

        var result = _categoryService.GetById(id);

        Assert.AreEqual(result.Message, $"ID değeri {id} olan bir kategori bulunamadı.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Update_WhenNameIsValid_ReturnsOk()
    {
        _mockRules.Setup(u => u.CategoryNameMustBeValid(categoryUpdateRequest.Name));
        _mockRepository.Setup(u => u.Update(category));

        var result = _categoryService.Update(categoryUpdateRequest);

        Assert.AreEqual(result.Data, categoryResponseDTO);
        Assert.AreEqual(result.Message, "Kategori başarıyla güncellendi.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void Update_WhenNameIsNotValid_ReturnsBadRequest()
    {
        _mockRules.Setup(u => u.CategoryNameMustBeValid(categoryUpdateRequest.Name)).Throws(new BusinessException("Geçerli bir kategori ismi girilmelidir."));

        var result = _categoryService.Update(categoryUpdateRequest);

        Assert.AreEqual(result.Message, "Geçerli bir kategori ismi girilmelidir.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }
}
