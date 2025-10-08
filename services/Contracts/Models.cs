namespace Contracts;

public record MissionDto(int Id, string Name, string? Terrain, DateTimeOffset StartTime, DateTimeOffset? EndTime);
public record CreateMissionDto(string Name, string? Terrain, DateTimeOffset? StartTime, DateTimeOffset? EndTime);
public record UnitDto(int Id, string Callsign, string Side, int Strength, int? MissionId);
public record CreateUnitDto(string Callsign, string Side, int Strength, int? MissionId);
public record SimulationResultDto(int MissionId, decimal SuccessProbability, int FriendlyCasualties, int AdversaryCasualties, string? Notes);
