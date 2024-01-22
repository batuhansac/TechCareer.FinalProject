using Core.CrossCuttingConcerns.Exceptions;
using System.Net;
using DataAccess.Repositories.Abstracts;
using Models.DTOs.RequestDTO;
using Models.DTOs.ResponseDTO;
using Models.Entities;
using Moq;
using Service.BusinessRules.Abstracts;
using Service.Concretes;

namespace Service.UnitTest.Posts;

public class PostServiceTests
{
    private PostService _postService;
    private Mock<IPostRepository> _mockRepository;
    private Mock<IPostRules> _mockRules;

    private PostAddRequest postAddRequest;
    private PostUpdateRequest postUpdateRequest;
    private Post post;
    private PostResponseDTO postResponseDTO;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IPostRepository>();
        _mockRules = new Mock<IPostRules>();
        _postService = new PostService(_mockRepository.Object, _mockRules.Object);
        postAddRequest = new PostAddRequest(Title: "Test", Content: "Test", DatePosted: new short(), UserId: new Guid(), CategoryId: new int());
        postUpdateRequest = new PostUpdateRequest(Id: new int(), Title: "Test", Content: "Test", DatePosted: new short(), UserId: new Guid(), CategoryId: new int());
        post = new Post()
        {
            Id = new int(),
            Title = "Test",
            Content = "Test",
            DatePosted = new short(),
            UserId = new Guid(),
            CategoryId = new int()
        };
        postResponseDTO = new PostResponseDTO(Id: new int(), Title: "Test", Content: "Test", DatePosted: new short(), UserId: new Guid(), CategoryId: new int());
    }

    [Test]
    public void Add_WhenTitleAndContentAreValid_ReturnsOk()
    {
        _mockRules.Setup(p => p.PostTitleMustBeValid(postAddRequest.Title));
        _mockRules.Setup(p => p.PostContentMustBeValid(postAddRequest.Content));

        _mockRepository.Setup(p => p.Add(post));

        var result = _postService.Add(postAddRequest);

        Assert.AreEqual(result.Data, postResponseDTO);
        Assert.AreEqual(result.Message, "Gönderi başarıyla oluşturuldu.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
    }

    [Test]
    public void Add_WhenTitleIsNotValid_ReturnsBadRequest()
    {
        _mockRules.Setup(p => p.PostTitleMustBeValid(postAddRequest.Title)).Throws(new BusinessException("Gönderinin başlığı boş olamaz ya da sadece boşluklardan oluşamaz."));

        var result = _postService.Add(postAddRequest);

        Assert.AreEqual(result.Message, "Gönderinin başlığı boş olamaz ya da sadece boşluklardan oluşamaz.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Add_WhenContentIsNotValid_ReturnsBadRequest()
    {
        _mockRules.Setup(p => p.PostContentMustBeValid(postAddRequest.Content)).Throws(new BusinessException("Gönderinin içeriği boş olamaz ya da sadece boşluklardan oluşamaz."));

        var result = _postService.Add(postAddRequest);

        Assert.AreEqual(result.Message, "Gönderinin içeriği boş olamaz ya da sadece boşluklardan oluşamaz.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Delete_WhenPostIsPresent_ReturnsOk()
    {
        int id = new int();

        _mockRules.Setup(p => p.PostIsPresent(id));
        _mockRepository.Setup(p => p.GetById(id, null)).Returns(post);
        _mockRepository.Setup(p => p.Delete(post));

        var result = _postService.Delete(id);

        Assert.AreEqual(result.Data, postResponseDTO);
        Assert.AreEqual(result.Message, "Gönderi başarıyla silindi.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void Delete_WhenCommentIsNotPresent_ReturnsBadRequest()
    {
        int id = new int();

        _mockRules.Setup(p => p.PostIsPresent(id)).Throws(new BusinessException($"ID değeri {id} olan bir gönderi bulunamadı."));

        var result = _postService.Delete(id);

        Assert.AreEqual(result.Message, $"ID değeri {id} olan bir gönderi bulunamadı.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void GetAll_ReturnsOk()
    {
        var posts = new List<Post>()
        {
            post
        };

        var postResponseDTOs = new List<PostResponseDTO>()
        {
            postResponseDTO
        };

        _mockRepository.Setup(p => p.GetAll(null, null)).Returns(posts);

        var result = _postService.GetAll();

        Assert.AreEqual(result.Data, postResponseDTOs);
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void GetAllByDatePosted_ReturnsOk()
    {
        short dateBegin = new short();
        short dateEnd = new short();

        var posts = new List<Post>()
        {
            post
        };

        var postResponseDTOs = new List<PostResponseDTO>()
        {
            postResponseDTO
        };

        _mockRepository.Setup(p => p.GetAll(p => p.DatePosted <= dateBegin && p.DatePosted >= dateEnd, null)).Returns(posts);

        var result = _postService.GetAllByDatePosted(dateBegin, dateEnd);

        Assert.AreEqual(result.Data, postResponseDTOs);
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void GetById_WhenPostIsPresent_ReturnsOk()
    {
        int id = new int();

        _mockRules.Setup(p => p.PostIsPresent(id));
        _mockRepository.Setup(p => p.GetById(id, null)).Returns(post);

        var result = _postService.GetById(id);

        Assert.AreEqual(result.Data, postResponseDTO);
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void GetById_WhenPostIsNotPresent_ReturnsBadRequest()
    {
        int id = new int();

        _mockRules.Setup(p => p.PostIsPresent(id)).Throws(new BusinessException($"ID değeri {id} olan bir gönderi bulunamadı."));

        var result = _postService.GetById(id);

        Assert.AreEqual(result.Message, $"ID değeri {id} olan bir gönderi bulunamadı.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Update_WhenTitleAndContentAreValid_ReturnsOk()
    {
        _mockRules.Setup(p => p.PostTitleMustBeValid(postUpdateRequest.Title));
        _mockRules.Setup(p => p.PostContentMustBeValid(postUpdateRequest.Content));
        _mockRepository.Setup(p => p.Update(post));

        var result = _postService.Update(postUpdateRequest);

        Assert.AreEqual(result.Data, postResponseDTO);
        Assert.AreEqual(result.Message, "Gönderi başarıyla güncellendi.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void Update_WhenTitleIsNotValid_ReturnsBadRequest()
    {
        _mockRules.Setup(p => p.PostTitleMustBeValid(postUpdateRequest.Title)).Throws(new BusinessException("Gönderinin başlığı boş olamaz ya da sadece boşluklardan oluşamaz."));

        var result = _postService.Update(postUpdateRequest);

        Assert.AreEqual(result.Message, "Gönderinin başlığı boş olamaz ya da sadece boşluklardan oluşamaz.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Update_WhenContentIsNotValid_ReturnsBadRequest()
    {
        _mockRules.Setup(p => p.PostContentMustBeValid(postUpdateRequest.Content)).Throws(new BusinessException("Gönderinin içeriği boş olamaz ya da sadece boşluklardan oluşamaz."));

        var result = _postService.Update(postUpdateRequest);

        Assert.AreEqual(result.Message, "Gönderinin içeriği boş olamaz ya da sadece boşluklardan oluşamaz.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }
}
