namespace FantasyFootball.Domain.Services
{
    public class TransferDomainService
    {
        public bool Execute(FantasyTeam fantasyTeam, Player playerIn, Player playerOut, Gameweek gameweek)
        {
            if (gameweek.IsFinished)
                throw new DomainException("Cannot transfer after gameweek is finished");

            if (playerIn.Position != playerOut.Position)
                throw new DomainException("Transfers must be for players of the same position");

            if (fantasyTeam.Budget + playerOut.Price.Value < playerIn.Price.Value)
                throw new DomainException("Insufficient budget for this transfer");

            var existingFantasyPlayer = fantasyTeam.Players.FirstOrDefault(p => p.PlayerId == playerOut.Id)
                ?? throw new DomainException("Player to transfer out is not in the squad");

            bool wasOnBench = existingFantasyPlayer.IsOnBench;
            
            // Apply transfer
            fantasyTeam.RemovePlayer(playerOut.Id, playerOut);
            
            var newFantasyPlayer = FantasyPlayer.Create(fantasyTeam.Id, playerIn.Id, wasOnBench);
            fantasyTeam.AddPlayer(newFantasyPlayer, playerIn);

            bool isFree = true;
            if (fantasyTeam.FreeTransfers > 0)
            {
                fantasyTeam.UseTransfer();
            }
            else
            {
                isFree = false;
            }

            return isFree;
        }
    }
}