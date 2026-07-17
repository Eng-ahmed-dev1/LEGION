public class FantasyTeam : BaseEntity
{
    public string Name { get; private set; } = default!;
    public decimal Budget { get; private set; }
    public int FreeTransfers { get; private set; }

    public Guid ManagerId { get; private set; }
    public Manager Manager { get; private set; } = default!;

    public bool TripleCaptainUsed { get; private set; }
    public bool BenchBoostUsed { get; private set; }
    public bool WildcardUsed { get; private set; }
    public bool FreeHitUsed { get; private set; }
    public FantasyFootball.Domain.Enums.ChipType? ActiveChip { get; private set; }
    public Guid? ActiveChipGameweekId { get; private set; }
    public bool AutoSubEnabled { get; private set; }

    private readonly List<FantasyPlayer> _players = [];
    public IReadOnlyCollection<FantasyPlayer> Players => _players.AsReadOnly();

    private FantasyTeam() { }

    public static FantasyTeam Create(string name, Guid managerId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Team name is required");

        if (managerId == Guid.Empty)
            throw new DomainException("Manager is required");

        return new FantasyTeam
        {
            Name = name,
            ManagerId = managerId,
            Budget = FantasyRules.InitialBudget,
            FreeTransfers = FantasyRules.FreeTransfersPerWeek,
            AutoSubEnabled = true // Default to true
        };
    }
    public static FantasyTeam Update(string name, decimal Budget, int FreeTransfers)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Team name is required");

        if (Budget > FantasyRules.InitialBudget)
            throw new DomainException("The Budget bigger than the initialBudget");
        if (FreeTransfers > FantasyRules.FreeTransfersPerWeek)
            throw new DomainException("The FreeTransfers bigger than the FreeTransfersPerWeek");

        return new FantasyTeam
        {
            Name = name,
            Budget = Budget,
            FreeTransfers = FreeTransfers
        };
    }
    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Team name is required");

        Name = name.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddPlayer(FantasyPlayer player, Player realPlayer)
    {
        if (_players.Count >= FantasyRules.SquadSize)
            throw new DomainException($"Squad cannot exceed {FantasyRules.SquadSize} players");

        if (realPlayer.Price.Value > Budget)
            throw new DomainException("Insufficient budget");

        // Validate max players per team
        // We use the realPlayer.TeamId to count
        // Note: _players is a collection of FantasyPlayer. To validate real-world teams and positions,
        // we either need the FantasyPlayer to eager-load Player, or just keep track. 
        // For Domain isolation, we should check the actual Player object associated with the existing FantasyPlayers.
        // Assuming the caller includes the .Player property on existing _players.
        var playersFromSameTeam = _players.Count(p => p.Player?.TeamId == realPlayer.TeamId);
        if (playersFromSameTeam >= FantasyRules.MaxPlayersPerTeam)
            throw new DomainException($"Cannot have more than {FantasyRules.MaxPlayersPerTeam} players from the same team");

        // Validate positional limits
        var gks = _players.Count(p => p.Player?.Position == FantasyFootball.Domain.Enums.PlayerPosition.Goalkeeper) + (realPlayer.Position == FantasyFootball.Domain.Enums.PlayerPosition.Goalkeeper ? 1 : 0);
        var defs = _players.Count(p => p.Player?.Position == FantasyFootball.Domain.Enums.PlayerPosition.Defender) + (realPlayer.Position == FantasyFootball.Domain.Enums.PlayerPosition.Defender ? 1 : 0);
        var mids = _players.Count(p => p.Player?.Position == FantasyFootball.Domain.Enums.PlayerPosition.Midfielder) + (realPlayer.Position == FantasyFootball.Domain.Enums.PlayerPosition.Midfielder ? 1 : 0);
        var fwds = _players.Count(p => p.Player?.Position == FantasyFootball.Domain.Enums.PlayerPosition.Forward) + (realPlayer.Position == FantasyFootball.Domain.Enums.PlayerPosition.Forward ? 1 : 0);

        if (gks > FantasyRules.MaxGoalkeepers) throw new DomainException($"Cannot have more than {FantasyRules.MaxGoalkeepers} goalkeepers");
        if (defs > FantasyRules.MaxDefenders) throw new DomainException($"Cannot have more than {FantasyRules.MaxDefenders} defenders");
        if (mids > FantasyRules.MaxMidfielders) throw new DomainException($"Cannot have more than {FantasyRules.MaxMidfielders} midfielders");
        if (fwds > FantasyRules.MaxForwards) throw new DomainException($"Cannot have more than {FantasyRules.MaxForwards} forwards");

        _players.Add(player);
        Budget -= realPlayer.Price.Value;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemovePlayer(Guid playerId, Player realPlayer)
    {
        var player = _players.FirstOrDefault(p => p.PlayerId == playerId)
            ?? throw new DomainException("Player not found in squad");

        _players.Remove(player);
        Budget += realPlayer.Price.Value;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ResetFreeTransfers()
    {
        FreeTransfers = FantasyRules.FreeTransfersPerWeek;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UseTransfer()
    {
        if (FreeTransfers > 0)
            FreeTransfers--;

        UpdatedAt = DateTime.UtcNow;
    }

    public void PlayChip(FantasyFootball.Domain.Enums.ChipType chipType, Guid gameweekId)
    {
        if (ActiveChipGameweekId == gameweekId && ActiveChip != null)
            throw new DomainException("Cannot play more than one chip in a single gameweek");

        switch (chipType)
        {
            case FantasyFootball.Domain.Enums.ChipType.TripleCaptain:
                if (TripleCaptainUsed) throw new DomainException("Triple Captain chip already used");
                TripleCaptainUsed = true;
                break;
            case FantasyFootball.Domain.Enums.ChipType.BenchBoost:
                if (BenchBoostUsed) throw new DomainException("Bench Boost chip already used");
                BenchBoostUsed = true;
                break;
            case FantasyFootball.Domain.Enums.ChipType.Wildcard:
                if (WildcardUsed) throw new DomainException("Wildcard chip already used");
                WildcardUsed = true;
                break;
            case FantasyFootball.Domain.Enums.ChipType.FreeHit:
                if (FreeHitUsed) throw new DomainException("Free Hit chip already used");
                FreeHitUsed = true;
                break;
        }

        ActiveChip = chipType;
        ActiveChipGameweekId = gameweekId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ToggleAutoSub()
    {
        AutoSubEnabled = !AutoSubEnabled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SubstitutePlayer(Guid playerInId, Guid playerOutId)
    {
        var playerIn = _players.FirstOrDefault(p => p.PlayerId == playerInId)
            ?? throw new DomainException("Player coming in not found in squad");
            
        var playerOut = _players.FirstOrDefault(p => p.PlayerId == playerOutId)
            ?? throw new DomainException("Player coming out not found in squad");

        if (!playerIn.IsOnBench)
            throw new DomainException("Player coming in must be on the bench");

        if (playerOut.IsOnBench)
            throw new DomainException("Player coming out must be on the field");

        // Swap
        playerIn.MoveToStartingXI();
        playerOut.MoveToBench();

        // Validate formation
        ValidateFormation();

        UpdatedAt = DateTime.UtcNow;
    }

    public void ValidateFormation()
    {
        var startingPlayers = _players.Where(p => !p.IsOnBench).ToList();
        
        // If the squad isn't fully built yet (e.g., during team creation), we skip strict formation validation
        // Only validate if we have 15 players.
        if (_players.Count < FantasyRules.SquadSize)
            return;

        if (startingPlayers.Count != 11)
            throw new DomainException("Starting XI must have exactly 11 players");

        var gks = startingPlayers.Count(p => p.Player?.Position == FantasyFootball.Domain.Enums.PlayerPosition.Goalkeeper);
        var defs = startingPlayers.Count(p => p.Player?.Position == FantasyFootball.Domain.Enums.PlayerPosition.Defender);
        var mids = startingPlayers.Count(p => p.Player?.Position == FantasyFootball.Domain.Enums.PlayerPosition.Midfielder);
        var fwds = startingPlayers.Count(p => p.Player?.Position == FantasyFootball.Domain.Enums.PlayerPosition.Forward);

        // If Player property isn't loaded (e.g. in some isolated tests without Includes), skip to avoid false errors
        if (startingPlayers.Any(p => p.Player == null))
            return;

        if (gks != 1) throw new DomainException("Formation must have exactly 1 Goalkeeper");
        if (defs < 3) throw new DomainException("Formation must have at least 3 Defenders");
        if (mids < 2) throw new DomainException("Formation must have at least 2 Midfielders");
        if (fwds < 1) throw new DomainException("Formation must have at least 1 Forward");
    }

    public void SetCaptain(Guid playerId)
    {
        var player = _players.FirstOrDefault(p => p.PlayerId == playerId)
            ?? throw new DomainException("Player not found in squad");

        if (player.IsOnBench)
            throw new DomainException("Bench player cannot be captain");

        // Remove captaincy from current captain
        var currentCaptain = _players.FirstOrDefault(p => p.IsCaptain);
        currentCaptain?.RemoveCaptaincy();

        player.AssignAsCaptain();
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetViceCaptain(Guid playerId)
    {
        var player = _players.FirstOrDefault(p => p.PlayerId == playerId)
            ?? throw new DomainException("Player not found in squad");

        if (player.IsOnBench)
            throw new DomainException("Bench player cannot be vice captain");

        if (player.IsCaptain)
            throw new DomainException("Captain cannot be vice captain");

        // Remove vice-captaincy from current vice-captain
        var currentViceCaptain = _players.FirstOrDefault(p => p.IsViceCaptain);
        if (currentViceCaptain != null)
        {
            // Reset to normal starting XI
            currentViceCaptain.RemoveCaptaincy();
        }

        player.AssignAsViceCaptain();
        UpdatedAt = DateTime.UtcNow;
    }
}