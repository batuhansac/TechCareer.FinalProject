using Microsoft.AspNetCore.Mvc;
using Models.DTOs.RequestDTO;
using Service.Abstracts;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : BaseController
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] CommentAddRequest commentAddRequest)
    {
        var result = _commentService.Add(commentAddRequest);
        return ActionResultInstance(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] CommentUpdateRequest commentUpdateRequest)
    {
        var result = _commentService.Update(commentUpdateRequest);
        return ActionResultInstance(result);
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] int id)
    {
        var result = _commentService.Delete(id);
        return ActionResultInstance(result);
    }

    [HttpGet("getbyid")]
    public IActionResult GetById([FromQuery] int id)
    {
        var result = _commentService.GetById(id);
        return ActionResultInstance(result);
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _commentService.GetAll();
        return ActionResultInstance(result);
    }

    [HttpGet("getbydateposted")]
    public IActionResult GetAllByDatePosted([FromQuery] short dateBegin, [FromQuery] short dateEnd)
    {
        var result = _commentService.GetAllByDatePosted(dateBegin, dateEnd);
        return ActionResultInstance(result);
    }

    [HttpGet("getbydetailid")]
    public IActionResult GetByDetailId([FromQuery] int id)
    {
        var result = _commentService.GetByDetailId(id);
        return ActionResultInstance(result);
    }

    [HttpGet("getalldetails")]
    public IActionResult GetAllDetails()
    {
        var result = _commentService.GetAllDetails();
        return ActionResultInstance(result);
    }

    [HttpGet("getalldetailsbypost")]
    public IActionResult GetAllDetailsByPostId([FromQuery] int postId)
    {
        var result = _commentService.GetAllDetailsByPostId(postId);
        return ActionResultInstance(result);
    }

    [HttpGet("getalldetailsbyuser")]
    public IActionResult GetAllDetailsByUserId([FromQuery] Guid userId)
    {
        var result = _commentService.GetAllDetailsByUserId(userId);
        return ActionResultInstance(result);
    }
}
