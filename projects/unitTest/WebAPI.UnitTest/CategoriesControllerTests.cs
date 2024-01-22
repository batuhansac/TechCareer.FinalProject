using Microsoft.AspNetCore.Mvc;
using Models.DTOs.RequestDTO;
using Moq;
using Service.Abstracts;
using WebAPI.Controllers;

namespace WebAPI.UnitTest;

public class CategoriesControllerTests
{
    private CategoriesController _categoriesController;
    private Mock<ICategoryService> _mockService;

    private CategoryAddRequest categoryAddRequest;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<ICategoryService>();
        _categoriesController = new CategoriesController(_mockService.Object);
        categoryAddRequest = new CategoryAddRequest(Name: "Test");
    }

    [Test]
    public void AddCategory_ReturnsOk()
    {
        _mockService.Setup(c => c.Add(categoryAddRequest));

        var result = _categoriesController.Add(categoryAddRequest);

        Assert.IsInstanceOf<CreatedResult>(result);
    }
}
