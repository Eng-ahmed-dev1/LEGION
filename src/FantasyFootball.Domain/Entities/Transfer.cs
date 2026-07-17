namespace FantasyFootball.Domain.Entities;

public class Transfer : BaseEntity
{
    public Guid FantasyTeamId { get; private set; }
    public FantasyTeam FantasyTeam { get; private set; } = default!;

    public Guid PlayerInId { get; private set; }
    public Player PlayerIn { get; private set; } = default!;

    public Guid PlayerOutId { get; private set; }
    public Player PlayerOut { get; private set; } = default!;

    public Guid GameweekId { get; private set; }
    public Gameweek Gameweek { get; private set; } = default!;

    public bool IsFree { get; private set; }

    private Transfer() { }

    public static Transfer Create(
        Guid fantasyTeamId,
        Guid playerInId,
        Guid playerOutId,
        Guid gameweekId,
        bool isFree)
    {
        if (playerInId == playerOutId)
            throw new DomainException("Player in and player out cannot be the same.");

        return new Transfer
        {
            FantasyTeamId = fantasyTeamId,
            PlayerInId = playerInId,
            PlayerOutId = playerOutId,
            GameweekId = gameweekId,
            IsFree = isFree
        };
    }
}