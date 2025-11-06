using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureMissionOps.API.DTOs;
using SecureMissionOps.API.Models;
using SecureMissionOps.API.Services;
using System.Security.Claims;

namespace SecureMissionOps.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MissionController(IMissionService svc) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await svc.GetAllAsync());

    [Authorize(Roles = "Admin,Operator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MissionDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.Identity?.Name ?? "user";
        var created = await svc.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:int}/status/{status}")]
    public async Task<IActionResult> UpdateStatus(int id, MissionStatus status)
        => await svc.UpdateStatusAsync(id, status, "admin") ? NoContent() : NotFound();
}

