using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserService _userService;

    public UserController(
        ILogger<UserController> logger,
        UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuthModel model)
    {
        try
        {
            var identityResult = await _userService.CreateAsync(model);
            if (identityResult.Succeeded)
                return Ok();

            return BadRequest(identityResult.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("auth")]
    public async Task<IActionResult> Authenticate([FromBody] AuthModel model)
    {
        try
        {
            var token = await _userService.Authenticate(model);
            if (!string.IsNullOrEmpty(token))
                return Ok(token);

            return Unauthorized();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
