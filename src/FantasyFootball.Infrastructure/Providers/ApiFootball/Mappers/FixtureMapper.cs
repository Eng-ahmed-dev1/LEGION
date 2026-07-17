namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Mappers;

public class FixtureMapper
{
    public Fixture Map(ApiFixtureResponseDto dto, Guid homeTeamId, Guid awayTeamId, Guid gameweekId)
    {
        var fixture = Fixture.Create(homeTeamId, awayTeamId, gameweekId, dto.Fixture.Date);

        fixture.UpdateSyncData(dto.Fixture.Id);

        if (dto.Fixture.Status.Short == "FT" || dto.Fixture.Status.Short == "AET" || dto.Fixture.Status.Short == "PEN")
        {
            fixture.RecordResult(dto.Goals.Home ?? 0, dto.Goals.Away ?? 0);
        }

        return fixture;
    }

    public void UpdateEntity(Fixture existingFixture, ApiFixtureResponseDto dto)
    {
        if (existingFixture.KickOff != dto.Fixture.Date && !existingFixture.IsFinished)
        {
            existingFixture.RescheduleKickOff(dto.Fixture.Date);
        }

        existingFixture.UpdateSyncData(dto.Fixture.Id);

        if (dto.Fixture.Status.Short == "FT" || dto.Fixture.Status.Short == "AET" || dto.Fixture.Status.Short == "PEN")
        {
            if (existingFixture.IsFinished)
            {
                existingFixture.CorrectResult(dto.Goals.Home ?? 0, dto.Goals.Away ?? 0);
            }
            else
            {
                existingFixture.RecordResult(dto.Goals.Home ?? 0, dto.Goals.Away ?? 0);
            }
        }
    }
}
