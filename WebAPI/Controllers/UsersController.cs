using Microsoft.AspNetCore.Mvc;
using Models.DTOs.RequestDTO;
using Service.Abstracts;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] UserAddRequest userAddRequest)
    {
        var result = _userService.Add(userAddRequest);
        return ActionResultInstance(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UserUpdateRequest userUpdateRequest)
    {
        var result = _userService.Update(userUpdateRequest);
        return ActionResultInstance(result);
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] Guid id)
    {
        var result = _userService.Delete(id);
        return ActionResultInstance(result);
    }

    [HttpGet("getbyid")]
    public IActionResult GetById([FromQuery] Guid id)
    {
        var result = _userService.GetById(id);
        return ActionResultInstance(result);
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _userService.GetAll();
        return ActionResultInstance(result);
    }
}
