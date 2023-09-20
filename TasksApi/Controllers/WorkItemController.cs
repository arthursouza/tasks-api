using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace tasks_api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class WorkItemController : Controller
{
    private readonly ILogger<WorkItemController> _logger;
    private readonly WorkItemService _workItemService;

    public WorkItemController(ILogger<WorkItemController> logger, WorkItemService workItemService)
    {
        _logger = logger;
        _workItemService = workItemService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorkItemModel model)
    {
        try
        {
            var result = await _workItemService.CreateAsync(model, GetUserId());
            return Ok(result);
        }
        catch (WorkItemValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] WorkItemModel model)
    {
        try
        {
            await _workItemService.UpdateAsync(model, GetUserId());
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (WorkItemValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        try
        {
            _workItemService.Remove(id, GetUserId());
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    public IActionResult Get(int id)
    {
        try
        {
            var result = _workItemService.Get(id, GetUserId());
            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_workItemService.GetAll(GetUserId()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
