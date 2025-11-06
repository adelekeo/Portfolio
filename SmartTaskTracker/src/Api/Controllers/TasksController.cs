// src/Api/Controllers/TasksController.cs
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[ApiController, Route("api/tasks")]
public class TasksController(AppDbContext db) : ControllerBase
{
    [HttpGet] public Task<List<TaskItem>> Get() => db.Tasks.AsNoTracking().ToListAsync();

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskItem>> GetById(Guid id)
    {
        var t = await db.Tasks.FindAsync(id); return t is null ? NotFound() : Ok(t);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskItem t)
    {
        db.Tasks.Add(t); await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = t.Id }, t);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, TaskItem dto)
    {
        var t = await db.Tasks.FindAsync(id); if (t is null) return NotFound();
        t.Title = dto.Title; t.Description = dto.Description; t.Status = dto.Status; t.DueUtc = dto.DueUtc; t.AssigneeId = dto.AssigneeId;
        await db.SaveChangesAsync(); return NoContent();
    }

    [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var t = await db.Tasks.FindAsync(id); if (t is null) return NotFound();
        db.Tasks.Remove(t); await db.SaveChangesAsync(); return NoContent();
    }
}

