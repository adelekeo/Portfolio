using SecureMissionOps.API.DTOs;
using SecureMissionOps.API.Models;

namespace SecureMissionOps.API.Services;

public interface IMissionService
{
    Task<IEnumerable<Mission>> GetAllAsync();
    Task<Mission?> GetAsync(int id);
    Task<Mission> CreateAsync(string ownerUserId, MissionDto dto);
    Task<bool> UpdateStatusAsync(int id, MissionStatus status, string byUserId);
}
