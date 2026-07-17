namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Mappers;

public class TeamMapper : IApiFootballMapper<ApiTeamResponseDto, Team>
{
    public Team Map(ApiTeamResponseDto dto)
    {
        var teamName = string.IsNullOrWhiteSpace(dto.Team.Name) ? "Unknown" : dto.Team.Name;
        var shortName = string.IsNullOrWhiteSpace(dto.Team.Code) ? teamName.Substring(0, Math.Min(3, teamName.Length)).ToUpper() : dto.Team.Code;

        var team = Team.Create(teamName, shortName, shortName, dto.Team.Logo ?? string.Empty);
        team.UpdateSyncData(dto.Team.Id);
        
        return team;
    }

    public void UpdateEntity(Team existingTeam, ApiTeamResponseDto dto)
    {
        var teamName = string.IsNullOrWhiteSpace(dto.Team.Name) ? existingTeam.Name : dto.Team.Name;
        var shortName = string.IsNullOrWhiteSpace(dto.Team.Code) ? existingTeam.ShortName : dto.Team.Code;

        existingTeam.Update(teamName, shortName, shortName, dto.Team.Logo ?? string.Empty);
        existingTeam.UpdateSyncData(dto.Team.Id);
    }
}
