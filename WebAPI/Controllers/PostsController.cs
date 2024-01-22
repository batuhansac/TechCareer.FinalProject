using Microsoft.AspNetCore.Mvc;
using Models.DTOs.RequestDTO;
using Service.Abstracts;
namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : BaseController
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] PostAddRequest postAddRequest)
    {
        var result = _postService.Add(postAddRequest);
        return ActionResultInstance(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] PostUpdateRequest postUpdateRequest)
    {
        var result = _postService.Update(postUpdateRequest);
        return ActionResultInstance(result);
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] int id)
    {
        var result = _postService.Delete(id);
        return ActionResultInstance(result);
    }

    [HttpGet("getbyid")]
    public IActionResult GetById([FromQuery] int id)
    {
        var result = _postService.GetById(id);
        return ActionResultInstance(result);
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _postService.GetAll();
        return ActionResultInstance(result);
    }

    [HttpGet("getbydateposted")]
    public IActionResult GetAllByDatePosted([FromQuery] short dateBegin, [FromQuery] short dateEnd)
    {
        var result = _postService.GetAllByDatePosted(dateBegin, dateEnd);
        return ActionResultInstance(result);
    }

    [HttpGet("getbydetailid")]
    public IActionResult GetByDetailId([FromQuery] int id)
    {
        var result = _postService.GetByDetailId(id);
        return ActionResultInstance(result);
    }

    [HttpGet("getalldetails")]
    public IActionResult GetAllDetails()
    {
        var result = _postService.GetAllDetails();
        return ActionResultInstance(result);
    }

    [HttpGet("getalldetailsbycategory")]
    public IActionResult GetAllDetailsByCategoryId([FromQuery] int categoryId)
    {
        var result = _postService.GetAllDetailsByCategoryId(categoryId);
        return ActionResultInstance(result);
    }

    [HttpGet("getalldetailsbyuser")]
    public IActionResult GetAllDetailsByUserId([FromQuery] Guid userId)
    {
        var result = _postService.GetAllDetailsByUserId(userId);
        return ActionResultInstance(result);
    }
}
