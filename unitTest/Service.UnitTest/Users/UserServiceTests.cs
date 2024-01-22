using System.Net;
using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstracts;
using Models.DTOs.RequestDTO;
using Models.DTOs.ResponseDTO;
using Models.Entities;
using Moq;
using Service.BusinessRules.Abstracts;
using Service.Concretes;

namespace Service.UnitTest.Users;

public class UserServiceTests
{
    private UserService _userService;
    private Mock<IUserRepository> _mockRepository;
    private Mock<IUserRules> _mockRules;

    private UserAddRequest userAddRequest;
    private UserUpdateRequest userUpdateRequest;
    private User user;
    private UserResponseDTO userResponseDTO;

    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IUserRepository>();
        _mockRules = new Mock<IUserRules>();
        _userService = new UserService(_mockRepository.Object, _mockRules.Object);
        userAddRequest = new UserAddRequest(UserName: "Test", Email: "test@example.com");
        userUpdateRequest = new UserUpdateRequest(Id: new Guid(), UserName: "Test", Email: "test@example.com");
        user = new User
        {
            Id = new Guid(),
            UserName = "Test",
            Email = "test@example.com"
        };
        userResponseDTO = new UserResponseDTO(Id: new Guid(), UserName: "Test", Email: "test@example.com");
    }

    [Test]
    public void Add_WhenUserNameAndEmailIsUniqueAndValid_ReturnsOk()
    {
        _mockRules.Setup(u => u.UserNameMustBeUnique(userAddRequest.UserName));
        _mockRules.Setup(u => u.UserNameMustBeValid(userAddRequest.UserName));
        _mockRules.Setup(u => u.UserEmailMustBeUnique(userAddRequest.Email));
        _mockRules.Setup(u => u.UserEmailMustBeValid(userAddRequest.Email));
        _mockRepository.Setup(u => u.Add(user));

        var result = _userService.Add(userAddRequest);

        Assert.AreEqual(result.Data, userResponseDTO);
        Assert.AreEqual(result.Message, "Kullanıcı başarıyla oluşturuldu.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
    }

    [Test]
    public void Add_WhenUserNameIsNotUnique_ReturnsBadRequest()
    {
        _mockRules.Setup(u => u.UserNameMustBeUnique(userAddRequest.UserName)).Throws(new BusinessException("Kullanıcı adı benzersiz olmalıdır."));

        var result = _userService.Add(userAddRequest);

        Assert.AreEqual(result.Message, "Kullanıcı adı benzersiz olmalıdır.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Add_WhenUserNameIsNotValid_ReturnsBadRequest()
    {
        _mockRules.Setup(u => u.UserNameMustBeValid(userAddRequest.UserName)).Throws(new BusinessException("Geçerli bir kullanıcı adı girilmelidir."));

        var result = _userService.Add(userAddRequest);

        Assert.AreEqual(result.Message, "Geçerli bir kullanıcı adı girilmelidir.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Add_WhenUserEmailIsNotUnique_ReturnsBadRequest()
    {
        _mockRules.Setup(u => u.UserEmailMustBeUnique(userAddRequest.Email)).Throws(new BusinessException("Kullanıcı e-mail adresi benzersiz olmalıdır."));
        
        var result = _userService.Add(userAddRequest);

        Assert.AreEqual(result.Message, "Kullanıcı e-mail adresi benzersiz olmalıdır.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Add_WhenUserEmailIsNotValid_ReturnsBadRequest()
    {
        _mockRules.Setup(u => u.UserEmailMustBeValid(userAddRequest.Email)).Throws(new BusinessException("Geçerli bir e-mail adresi girilmelidir."));

        var result = _userService.Add(userAddRequest);

        Assert.AreEqual(result.Message, "Geçerli bir e-mail adresi girilmelidir.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Delete_WhenUserIsPresent_ReturnsOk()
    {
        Guid id = new Guid();

        _mockRules.Setup(u => u.UserIsPresent(id));
        _mockRepository.Setup(u => u.GetById(id, null)).Returns(user);
        _mockRepository.Setup(u => u.Delete(user));

        var result = _userService.Delete(id);

        Assert.AreEqual(result.Data, userResponseDTO);
        Assert.AreEqual(result.Message, "Kullanıcı başarıyla silindi.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void Delete_WhenUserIsNotPresent_ReturnsBadRequest()
    {
        Guid id = new Guid();

        _mockRules.Setup(u => u.UserIsPresent(id)).Throws(new BusinessException($"ID değeri {id} olan bir kullanıcı bulunamadı."));

        var result = _userService.Delete(id);

        Assert.AreEqual(result.Message, $"ID değeri {id} olan bir kullanıcı bulunamadı.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void GetAll_ReturnsOk()
    {
        var users = new List<User>()
        {
            user
        };

        var userResponseDTOs = new List<UserResponseDTO>()
        {
            userResponseDTO
        };

        _mockRepository.Setup(u => u.GetAll(null, null)).Returns(users);

        var result = _userService.GetAll();

        Assert.AreEqual(result.Data, userResponseDTOs);
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void GetById_WhenUserIsPresent_ReturnsOk()
    {
        Guid id = new Guid();

        _mockRules.Setup(u => u.UserIsPresent(id));
        _mockRepository.Setup(u => u.GetById(id, null)).Returns(user);

        var result = _userService.GetById(id);

        Assert.AreEqual(result.Data, userResponseDTO);
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void GetById_WhenUserIsNotPresent_ReturnsBadRequest()
    {
        Guid id = new Guid();

        _mockRules.Setup(u => u.UserIsPresent(id)).Throws(new BusinessException($"ID değeri {id} olan bir kullanıcı bulunamadı."));

        var result = _userService.GetById(id);

        Assert.AreEqual(result.Message, $"ID değeri {id} olan bir kullanıcı bulunamadı.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Update_WhenUserNameAndEmailAreValid_ReturnsOk()
    {
        _mockRules.Setup(u => u.UserNameMustBeValid(userUpdateRequest.UserName));
        _mockRules.Setup(u => u.UserEmailMustBeValid(userUpdateRequest.Email));
        _mockRepository.Setup(u => u.Update(user));

        var result = _userService.Update(userUpdateRequest);

        Assert.AreEqual(result.Data, userResponseDTO);
        Assert.AreEqual(result.Message, "Kullanıcı başarıyla güncellendi.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void Update_WhenUserNameIsNotUnique_ReturnsBadRequest()
    {
        _mockRules.Setup(u => u.UserNameMustBeValid(userUpdateRequest.UserName)).Throws(new BusinessException("Geçerli bir kullanıcı adı girilmelidir."));

        var result = _userService.Update(userUpdateRequest);

        Assert.AreEqual(result.Message, "Geçerli bir kullanıcı adı girilmelidir.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Update_WhenUserEmailIsNotUnique_ReturnsBadRequest()
    {
        _mockRules.Setup(u => u.UserEmailMustBeValid(userUpdateRequest.Email)).Throws(new BusinessException("Geçerli bir e-mail adresi girilmelidir."));

        var result = _userService.Update(userUpdateRequest);

        Assert.AreEqual(result.Message, "Geçerli bir e-mail adresi girilmelidir.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }
}