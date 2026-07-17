namespace FantasyFootball.Domain.Tests.Services
{
    public class ScoringDomainServiceTests
    {
        private readonly ScoringDomainService _sut;

        public ScoringDomainServiceTests()
        {
            _sut = new ScoringDomainService();
        }

        private Player CreatePlayer(PlayerPosition position)
        {
            return Player.Create("First", "Last", position, new FantasyFootball.Domain.ValueObjects.Price(5.0m), Guid.NewGuid());
        }

        private void AddPlayerToTeam(FantasyTeam team, Player player)
        {
            var fp = FantasyPlayer.Create(team.Id, player.Id, false);
            var field = typeof(FantasyPlayer).GetField("<Player>k__BackingField", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null) field.SetValue(fp, player);
            else typeof(FantasyPlayer).GetProperty("Player")!.SetValue(fp, player);
            team.AddPlayer(fp, player);
        }

        private PlayerEvent CreateEvent(Player player, EventType eventType, int points = 0)
        {
            // PlayerEvent.Create assumes points is precalculated or passed. For cards it's negative.
            // Oh wait, in ScoringDomainService, for Bonus and MinutesPlayed it reads ev.Points!
            // Wait, for Goals, it calculates based on position. 
            return PlayerEvent.Create(player.Id, Guid.NewGuid(), eventType, points);
        }

        [Theory]
        [InlineData(PlayerPosition.Goalkeeper, 6)]
        [InlineData(PlayerPosition.Defender, 6)]
        [InlineData(PlayerPosition.Midfielder, 5)]
        [InlineData(PlayerPosition.Forward, 4)]
        public void CalculatePoints_Goal_ReturnsCorrectPoints(PlayerPosition position, int expectedPoints)
        {
            // Arrange
            var player = CreatePlayer(position);
            var events = new[] { CreateEvent(player, EventType.Goal) };

            // Act
            int points = _sut.CalculatePoints(player, events);

            // Assert
            points.Should().Be(expectedPoints);
        }

        [Fact]
        public void CalculatePoints_Assist_ReturnsThreePoints()
        {
            var player = CreatePlayer(PlayerPosition.Forward);
            var events = new[] { CreateEvent(player, EventType.Assist) };

            int points = _sut.CalculatePoints(player, events);
            points.Should().Be(3);
        }

        [Theory]
        [InlineData(PlayerPosition.Goalkeeper, 6)]
        [InlineData(PlayerPosition.Defender, 6)]
        [InlineData(PlayerPosition.Midfielder, 1)]
        [InlineData(PlayerPosition.Forward, 0)]
        public void CalculatePoints_CleanSheet_ReturnsCorrectPoints(PlayerPosition position, int expectedPoints)
        {
            var player = CreatePlayer(position);
            var events = new[] { CreateEvent(player, EventType.CleanSheet) };

            int points = _sut.CalculatePoints(player, events);
            points.Should().Be(expectedPoints);
        }

        [Fact]
        public void CalculatePoints_YellowCard_ReturnsMinusOne()
        {
            var player = CreatePlayer(PlayerPosition.Midfielder);
            var events = new[] { CreateEvent(player, EventType.YellowCard, -1) }; // points passed as -1 because PlayerEvent takes it, but ScoringDomainService actually hardcodes -1

            int points = _sut.CalculatePoints(player, events);
            points.Should().Be(-1);
        }

        [Fact]
        public void CalculatePoints_RedCard_ReturnsMinusThree()
        {
            var player = CreatePlayer(PlayerPosition.Midfielder);
            var events = new[] { CreateEvent(player, EventType.RedCard, -3) };

            int points = _sut.CalculatePoints(player, events);
            points.Should().Be(-3);
        }

        [Fact]
        public void CalculatePoints_MinutesPlayed_UsesEventPoints()
        {
            var player = CreatePlayer(PlayerPosition.Midfielder);
            var events = new[] { CreateEvent(player, EventType.MinutesPlayed, 2) }; // 2 points for > 60 mins

            int points = _sut.CalculatePoints(player, events);
            points.Should().Be(2);
        }

        [Fact]
        public void CalculatePoints_Bonus_UsesEventPoints()
        {
            var player = CreatePlayer(PlayerPosition.Midfielder);
            var events = new[] { CreateEvent(player, EventType.Bonus, 3) }; // 3 bonus points

            int points = _sut.CalculatePoints(player, events);
            points.Should().Be(3);
        }

        [Fact]
        public void CalculateGameweekScore_CaptainMultipliesByTwo()
        {
            // Arrange
            var managerId = Guid.NewGuid();
            var team = FantasyTeam.Create("My Team", managerId);
            var player = CreatePlayer(PlayerPosition.Midfielder);
            AddPlayerToTeam(team, player);
            team.SetCaptain(player.Id);

            var events = new[] { 
                CreateEvent(player, EventType.MinutesPlayed, 2),
                CreateEvent(player, EventType.Goal, 0) // Midfielder goal = 5
            }; // Total = 7

            // Act
            int score = _sut.CalculateGameweekScore(team, events, false, false);

            // Assert
            score.Should().Be(14); // (2 + 5) * 2
        }

        [Fact]
        public void CalculateGameweekScore_TripleCaptainMultipliesByThree()
        {
            // Arrange
            var managerId = Guid.NewGuid();
            var team = FantasyTeam.Create("My Team", managerId);
            var player = CreatePlayer(PlayerPosition.Midfielder);
            AddPlayerToTeam(team, player);
            team.SetCaptain(player.Id);

            var events = new[] { 
                CreateEvent(player, EventType.MinutesPlayed, 2),
                CreateEvent(player, EventType.Goal, 0) // 5 pts
            };

            // Act
            int score = _sut.CalculateGameweekScore(team, events, false, isTripleCaptainActive: true);

            // Assert
            score.Should().Be(21); // (5 + 2) * 3
        }

        [Fact]
        public void CalculateGameweekScore_ViceCaptainTakesOverIfCaptainDoesNotPlay()
        {
            // Arrange
            var managerId = Guid.NewGuid();
            var team = FantasyTeam.Create("My Team", managerId);
            
            var captain = CreatePlayer(PlayerPosition.Forward);
            var viceCaptain = CreatePlayer(PlayerPosition.Midfielder);
            
            AddPlayerToTeam(team, captain);
            AddPlayerToTeam(team, viceCaptain);
            team.SetCaptain(captain.Id);
            team.SetViceCaptain(viceCaptain.Id);

            // Captain has NO events (0 mins)
            // Vice Captain scores a goal (5 pts) and played mins (2 pts)
            var events = new[] { 
                CreateEvent(viceCaptain, EventType.Goal, 0),
                CreateEvent(viceCaptain, EventType.MinutesPlayed, 2)
            };

            // Act
            int score = _sut.CalculateGameweekScore(team, events, false, false);

            // Assert
            score.Should().Be(14); // Vice Captain points (5 + 2) * 2 = 14
        }
    }
}
