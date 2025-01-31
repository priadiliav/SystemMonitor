using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemMonitor.Models.Entities;
using SystemMonitor.Server.Services;

namespace SystemMonitor.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/computers")]
public class ComputerController(ComputerService computerService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComputerDetails>>> Get()
    {
        var computers = await computerService.Get();

        if (computers.Count == 0)
            return NotFound();

        return Ok(computers);
    }
}