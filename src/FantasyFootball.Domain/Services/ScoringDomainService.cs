namespace FantasyFootball.Domain.Services
{
    public class ScoringDomainService
    {
        public int CalculatePoints(Player player, IEnumerable<PlayerEvent> events)
        {
            int points = 0;

            foreach (var ev in events)
            {
                points += ev.EventType switch
                {
                    EventType.Goal => player.Position switch
                    {
                        PlayerPosition.Goalkeeper => 6,
                        PlayerPosition.Defender => 6,
                        PlayerPosition.Midfielder => 5,
                        PlayerPosition.Forward => 4,
                        _ => 0
                    },
                    EventType.Assist => 3,
                    EventType.CleanSheet => player.Position switch
                    {
                        PlayerPosition.Goalkeeper => 6,
                        PlayerPosition.Defender => 6,
                        PlayerPosition.Midfielder => 1,
                        _ => 0
                    },
                    EventType.YellowCard => -1,
                    EventType.RedCard => -3,
                    EventType.PenaltyMiss => -2,
                    EventType.PenaltySave => 5,
                    EventType.OwnGoal => -2,
                    EventType.Bonus => ev.Points, // Bonus points are variable and passed in the event
                    EventType.MinutesPlayed => ev.Points, // Minutes points are 1 or 2 based on playtime, passed in the event
                    EventType.Saves => 1, // assuming 1 point per 3 saves is calculated before creating the event
                    _ => 0
                };
            }

            return points;
        }

        public int CalculateGameweekScore(FantasyTeam team, IEnumerable<PlayerEvent> gameweekEvents, bool isBenchBoostActive, bool isTripleCaptainActive)
        {
            var allPlayers = team.Players.ToList();
            var startingXI = allPlayers.Where(p => !p.IsOnBench).ToList();
            var bench = allPlayers.Where(p => p.IsOnBench).ToList();

            // 1. Auto Substitution
            if (team.AutoSubEnabled && !isBenchBoostActive)
            {
                var startersWhoDidNotPlay = startingXI.Where(p => !DidPlayerPlay(p.PlayerId, gameweekEvents)).ToList();
                var availableBench = bench.Where(p => DidPlayerPlay(p.PlayerId, gameweekEvents)).ToList();

                foreach (var starter in startersWhoDidNotPlay)
                {
                    // Find eligible sub
                    var eligibleSub = availableBench.FirstOrDefault(sub => IsValidFormation(startingXI, starter, sub));
                    if (eligibleSub != null)
                    {
                        // Perform memory swap
                        startingXI.Remove(starter);
                        startingXI.Add(eligibleSub);
                        availableBench.Remove(eligibleSub);
                    }
                }
            }

            // If BenchBoost is active, everyone scores
            var scoringPlayers = isBenchBoostActive ? allPlayers : startingXI;
            
            // 2. Calculate Base Points
            int totalPoints = 0;
            foreach (var p in scoringPlayers)
            {
                var playerEvents = gameweekEvents.Where(e => e.PlayerId == p.PlayerId);
                int points = CalculatePoints(p.Player, playerEvents);

                // 3. Captain Multiplier
                if (p.IsCaptain)
                {
                    // If captain didn't play, multiplier goes to vice-captain
                    if (DidPlayerPlay(p.PlayerId, gameweekEvents))
                    {
                        points *= isTripleCaptainActive ? 3 : 2;
                    }
                }
                else if (p.IsViceCaptain)
                {
                    var captain = allPlayers.FirstOrDefault(c => c.IsCaptain);
                    if (captain == null || !DidPlayerPlay(captain.PlayerId, gameweekEvents))
                    {
                        // Vice captain takes over
                        points *= isTripleCaptainActive ? 3 : 2;
                    }
                }

                totalPoints += points;
            }

            return totalPoints;
        }

        private bool DidPlayerPlay(Guid playerId, IEnumerable<PlayerEvent> events)
        {
            // A player played if they have a MinutesPlayed event with Points > 0 (meaning > 0 mins)
            return events.Any(e => e.PlayerId == playerId && e.EventType == EventType.MinutesPlayed);
        }

        private bool IsValidFormation(List<FantasyPlayer> currentXI, FantasyPlayer playerOut, FantasyPlayer playerIn)
        {
            if (playerOut.Player?.Position == PlayerPosition.Goalkeeper && playerIn.Player?.Position != PlayerPosition.Goalkeeper)
                return false;
            
            if (playerOut.Player?.Position != PlayerPosition.Goalkeeper && playerIn.Player?.Position == PlayerPosition.Goalkeeper)
                return false;

            var proposedXI = currentXI.Where(p => p.PlayerId != playerOut.PlayerId).ToList();
            proposedXI.Add(playerIn);

            var defs = proposedXI.Count(p => p.Player?.Position == PlayerPosition.Defender);
            var mids = proposedXI.Count(p => p.Player?.Position == PlayerPosition.Midfielder);
            var fwds = proposedXI.Count(p => p.Player?.Position == PlayerPosition.Forward);

            if (defs < 3 || mids < 2 || fwds < 1)
                return false;

            return true;
        }
    }
}