namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Mappers;

public class PlayerMapper
{
    public Player Map(ApiPlayerResponseDto dto, Guid teamId)
    {
        var positionStr = dto.Statistics.FirstOrDefault()?.Games.Position;
        var position = MapPosition(positionStr);

        var player = Player.Create(
            firstName: dto.Player.Firstname,
            lastName: dto.Player.Lastname,
            position: position,
            price: new Price(5.0m), // Default price
            teamId: teamId
        );

        player.UpdateSyncData(dto.Player.Id);

        var status = dto.Player.Injured ? AvailabilityStatus.Injured : AvailabilityStatus.Available;
        var chance = dto.Player.Injured ? 0 : 100;
        player.UpdateAvailability(status, chance);

        return player;
    }

    public void UpdateEntity(Player existingPlayer, ApiPlayerResponseDto dto, Guid teamId)
    {
        existingPlayer.Update(dto.Player.Firstname, dto.Player.Lastname, existingPlayer.Price);
        existingPlayer.UpdateSyncData(dto.Player.Id);

        var status = dto.Player.Injured ? AvailabilityStatus.Injured : AvailabilityStatus.Available;
        var chance = dto.Player.Injured ? 0 : 100;
        existingPlayer.UpdateAvailability(status, chance);
    }

    private PlayerPosition MapPosition(string? apiPosition)
    {
        return apiPosition?.ToLower() switch
        {
            "goalkeeper" => PlayerPosition.Goalkeeper,
            "defender" => PlayerPosition.Defender,
            "midfielder" => PlayerPosition.Midfielder,
            "attacker" => PlayerPosition.Forward,
            _ => PlayerPosition.Midfielder // Default
        };
    }
}
