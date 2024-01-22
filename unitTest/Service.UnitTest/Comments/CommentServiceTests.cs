using System.Net;
using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstracts;
using Models.DTOs.RequestDTO;
using Models.DTOs.ResponseDTO;
using Models.Entities;
using Moq;
using Service.BusinessRules.Abstracts;
using Service.Concretes;

namespace Service.UnitTest.Comments;

public class CommentServiceTests
{
    private CommentService _commentService;
    private Mock<ICommentRepository> _mockRepository;
    private Mock<ICommentRules> _mockRules;

    private CommentAddRequest commentAddRequest;
    private CommentUpdateRequest commentUpdateRequest;
    private Comment comment;
    private CommentResponseDTO commentResponseDTO;


    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<ICommentRepository>();
        _mockRules = new Mock<ICommentRules>();
        _commentService = new CommentService(_mockRepository.Object, _mockRules.Object);
        commentAddRequest = new CommentAddRequest(Content: "Test", DatePosted: new short(), UserId: new Guid(), PostId: new int());
        commentUpdateRequest = new CommentUpdateRequest(Id: new int(), Content: "Test", DatePosted: new short(), UserId: new Guid(), PostId: new int());
        comment = new Comment()
        {
            Id = new int(),
            Content = "Test",
            DatePosted = new short(),
            UserId = new Guid(),
            PostId = new int()
        };
        commentResponseDTO = new CommentResponseDTO(Id: new int(), Content: "Test", DatePosted: new short(), UserId: new Guid(), PostId: new int());

    }

    [Test]
    public void Add_WhenCommentContentIsValid_ReturnsOk()
    {
        _mockRules.Setup(c => c.CommentContentMustBeValid(commentAddRequest.Content));

        _mockRepository.Setup(c => c.Add(comment));

        var result = _commentService.Add(commentAddRequest);

        Assert.AreEqual(result.Data, commentResponseDTO);
        Assert.AreEqual(result.Message, "Yorum başarıyla oluşturuldu.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
    }

    [Test]
    public void Add_WhenCommentContentIsNotValid_ReturnsBadRequest()
    {
        _mockRules.Setup(c => c.CommentContentMustBeValid(commentAddRequest.Content)).Throws(new BusinessException("İçerik boş olamaz ya da boşluklardan oluşamaz."));

        var result = _commentService.Add(commentAddRequest);

        Assert.AreEqual(result.Message, "İçerik boş olamaz ya da boşluklardan oluşamaz.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Delete_WhenCommentIsPresent_ReturnsOk()
    {
        int id = new int();

        _mockRules.Setup(c => c.CommentIsPresent(id));
        _mockRepository.Setup(c => c.GetById(id, null)).Returns(comment);
        _mockRepository.Setup(c => c.Delete(comment));

        var result = _commentService.Delete(id);

        Assert.AreEqual(result.Data, commentResponseDTO);
        Assert.AreEqual(result.Message, "Yorum başarıyla silindi.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void Delete_WhenCommentIsNotPresent_ReturnsBadRequest()
    {
        int id = new int();

        _mockRules.Setup(c => c.CommentIsPresent(id)).Throws(new BusinessException($"ID değeri {id} olan bir yorum bulunamadı."));

        var result = _commentService.Delete(id);

        Assert.AreEqual(result.Message, $"ID değeri {id} olan bir yorum bulunamadı.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void GetAll_ReturnsOk()
    {
        var comments = new List<Comment>()
        {
            comment
        };

        var commentResponseDTOs = new List<CommentResponseDTO>()
        {
            commentResponseDTO
        };

        _mockRepository.Setup(c => c.GetAll(null, null)).Returns(comments);

        var result = _commentService.GetAll();

        Assert.AreEqual(result.Data, commentResponseDTOs);
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void GetAllByDatePosted_ReturnsOk()
    {
        short dateBegin = new short();
        short dateEnd = new short();

        var comments = new List<Comment>()
        {
            comment
        };

        var commentResponseDTOs = new List<CommentResponseDTO>()
        {
            commentResponseDTO
        };

        _mockRepository.Setup(c => c.GetAll(p => p.DatePosted <= dateBegin && p.DatePosted >= dateEnd, null)).Returns(comments);

        var result = _commentService.GetAllByDatePosted(dateBegin, dateEnd);

        Assert.AreEqual(result.Data, commentResponseDTOs);
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void GetById_WhenCommentIsPresent_ReturnsOk()
    {
        int id = new int();

        _mockRules.Setup(c => c.CommentIsPresent(id));
        _mockRepository.Setup(c => c.GetById(id, null)).Returns(comment);

        var result = _commentService.GetById(id);

        Assert.AreEqual(result.Data, commentResponseDTO);
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void GetById_WhenCommentIsNotPresent_ReturnsBadRequest()
    {
        int id = new int();

        _mockRules.Setup(c => c.CommentIsPresent(id)).Throws(new BusinessException($"ID değeri {id} olan bir yorum bulunamadı."));

        var result = _commentService.GetById(id);

        Assert.AreEqual(result.Message, $"ID değeri {id} olan bir yorum bulunamadı.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Update_WhenCommentContentIsValid_ReturnsOk()
    {
        _mockRules.Setup(c => c.CommentContentMustBeValid(commentUpdateRequest.Content));
        _mockRepository.Setup(c => c.Update(comment));

        var result = _commentService.Update(commentUpdateRequest);

        Assert.AreEqual(result.Data, commentResponseDTO);
        Assert.AreEqual(result.Message, "Yorum başarıyla güncellendi.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void Update_WhenCommentContentIsNotValid_ReturnsBadRequest()
    {
        _mockRules.Setup(c => c.CommentContentMustBeValid(commentUpdateRequest.Content)).Throws(new BusinessException("İçerik boş olamaz ya da boşluklardan oluşamaz."));

        var result = _commentService.Update(commentUpdateRequest);

        Assert.AreEqual(result.Message, "İçerik boş olamaz ya da boşluklardan oluşamaz.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }
}
