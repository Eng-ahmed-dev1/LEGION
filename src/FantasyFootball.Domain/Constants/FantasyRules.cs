namespace FantasyFootball.Domain.Constants
{
    public static class FantasyRules
    {
        public const int SquadSize = 15;
        public const int MaxPlayersPerTeam = 3;
        public const decimal InitialBudget = 100.0m;
        public const int FreeTransfersPerWeek = 1;
        public const int TransferPenaltyPoints = 4;
        public const decimal MinPlayerPrice = 4.0m;
        public const decimal MaxPlayerPrice = 15.0m;
        
        // Squad limits
        public const int MaxGoalkeepers = 2;
        public const int MaxDefenders = 5;
        public const int MaxMidfielders = 5;
        public const int MaxForwards = 3;
    }
}