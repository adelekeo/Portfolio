using CommandDashboardLite.Api.Data;
using CommandDashboardLite.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommandDashboardLite.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly AppDbContext _db;

    public AssetsController(AppDbContext db) => _db = db;

    [HttpGet]
    [Authorize] // any authenticated user
    public async Task<ActionResult<IEnumerable<Asset>>> GetAll()
        => await _db.Assets.AsNoTracking().ToListAsync();

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<Asset>> Get(int id)
    {
        var asset = await _db.Assets.FindAsync(id);
        return asset is null ? NotFound() : asset;
    }

    [HttpPost]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<ActionResult<Asset>> Create(Asset asset)
    {
        _db.Assets.Add(asset);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = asset.Id }, asset);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Update(int id, Asset input)
    {
        var asset = await _db.Assets.FindAsync(id);
        if (asset is null) return NotFound();

        asset.Name = input.Name;
        asset.Type = input.Type;
        asset.Location = input.Location;
        asset.Status = input.Status;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Delete(int id)
    {
        var asset = await _db.Assets.FindAsync(id);
        if (asset is null) return NotFound();

        _db.Assets.Remove(asset);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

