using Microsoft.AspNetCore.Mvc;
using SystemMonitor.Models.Dtos;
using SystemMonitor.Models.Entities;
using SystemMonitor.Server.Services;

namespace SystemMonitor.Server.Controllers;

[ApiController]
[Route("api/computer")]
public class ComputerDetailsController (ComputerService computerService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComputerDetails>>> Get()
    {
        var computerDetails = await computerService.Get();
        return Ok(computerDetails);
    }
}


