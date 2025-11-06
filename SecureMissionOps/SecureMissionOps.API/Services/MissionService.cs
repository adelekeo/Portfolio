using Microsoft.EntityFrameworkCore;
using SecureMissionOps.API.Data;
using SecureMissionOps.API.DTOs;
using SecureMissionOps.API.Models;

namespace SecureMissionOps.API.Services;

public class MissionService(AppDbContext db) : IMissionService
{
    public async Task<IEnumerable<Mission>> GetAllAsync() =>
        await db.Missions.OrderByDescending(m => m.CreatedUtc).ToListAsync();

    public Task<Mission?> GetAsync(int id) =>
        db.Missions.FirstOrDefaultAsync(m => m.Id == id);

    public async Task<Mission> CreateAsync(string ownerUserId, MissionDto dto)
    {
        var m = new Mission
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            OwnerUserId = ownerUserId
        };
        db.Missions.Add(m);
        await db.SaveChangesAsync();
        return m;
    }

    public async Task<bool> UpdateStatusAsync(int id, MissionStatus status, string byUserId)
    {
        var m = await db.Missions.FindAsync(id);
        if (m is null) return false;
        m.Status = status;
        await db.SaveChangesAsync();
        return true;
    }
}

