// src/Api/Controllers/ProjectsController.cs
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[ApiController, Route("api/projects")]
public class ProjectsController(AppDbContext db) : ControllerBase
{
    [HttpGet] public Task<List<Project>> Get() => db.Projects.AsNoTracking().ToListAsync();

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Project>> GetById(Guid id)
    {
        var p = await db.Projects.Include(x => x.Tasks).FirstOrDefaultAsync(x => x.Id == id);
        return p is null ? NotFound() : Ok(p);
    }

    [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
    [HttpPost]
    public async Task<ActionResult<Project>> Create(Project p)
    {
        db.Projects.Add(p); await db.SaveChangesAsync(); return CreatedAtAction(nameof(GetById), new { id = p.Id }, p);
    }
}
