namespace FantasyFootball.Domain.Tests.Entities
{
    public class FantasyTeamTests
    {
        private readonly Manager _manager;

        public FantasyTeamTests()
        {
            _manager = Manager.Create("Test Team", Guid.NewGuid(), "testuser");
        }

        private Player CreatePlayer(string name, PlayerPosition position, decimal priceAmount, Guid? clubId = null)
        {
            // Assuming Player requires Guid.NewGuid() for ClubId if not provided
            var club = clubId ?? Guid.NewGuid();
            return Player.Create(name, "Last", position, new FantasyFootball.Domain.ValueObjects.Price(priceAmount), club);
        }

        private void AddPlayerToTeam(FantasyTeam team, Player player, bool isOnBench = false)
        {
            var fp = FantasyPlayer.Create(team.Id, player.Id, isOnBench);
            var field = typeof(FantasyPlayer).GetField("<Player>k__BackingField", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null) field.SetValue(fp, player);
            else typeof(FantasyPlayer).GetProperty("Player")!.SetValue(fp, player);
            team.AddPlayer(fp, player);
        }

        [Fact]
        public void AddPlayer_WithinBudget_ShouldSucceed()
        {
            // Arrange
            var team = FantasyTeam.Create("My Dream Team", _manager.Id);
            var player = CreatePlayer("Salah", PlayerPosition.Midfielder, 10.0m);

            // Act
            AddPlayerToTeam(team, player);

            // Assert
            team.Players.Should().HaveCount(1);
            team.Budget.Should().Be(Constants.FantasyRules.InitialBudget - 10.0m);
        }

        [Fact]
        public void AddPlayer_ExceedsBudget_ShouldThrowDomainException()
        {
            // Arrange
            var team = FantasyTeam.Create("My Dream Team", _manager.Id);
            
            // Add 6 players at max price (15.0m) = 90.0m
            AddPlayerToTeam(team, CreatePlayer("P1", PlayerPosition.Goalkeeper, 15.0m));
            AddPlayerToTeam(team, CreatePlayer("P2", PlayerPosition.Goalkeeper, 15.0m));
            AddPlayerToTeam(team, CreatePlayer("P3", PlayerPosition.Defender, 15.0m));
            AddPlayerToTeam(team, CreatePlayer("P4", PlayerPosition.Defender, 15.0m));
            AddPlayerToTeam(team, CreatePlayer("P5", PlayerPosition.Defender, 15.0m));
            AddPlayerToTeam(team, CreatePlayer("P6", PlayerPosition.Midfielder, 15.0m));

            var player = CreatePlayer("Expensive Player", PlayerPosition.Forward, 15.0m);

            // Act & Assert
            Action act = () => AddPlayerToTeam(team, player);
            act.Should().Throw<DomainException>().WithMessage("*budget*");
        }

        [Fact]
        public void AddPlayer_ExceedsSquadSize_ShouldThrowDomainException()
        {
            // Arrange
            var team = FantasyTeam.Create("My Dream Team", _manager.Id);
            for (int i = 0; i < 15; i++)
            {
                // Add 2 GKP, 5 DEF, 5 MID, 3 FWD to respect position limits
                PlayerPosition pos = i < 2 ? PlayerPosition.Goalkeeper :
                                     i < 7 ? PlayerPosition.Defender :
                                     i < 12 ? PlayerPosition.Midfielder : PlayerPosition.Forward;
                                     
                AddPlayerToTeam(team, CreatePlayer($"Player {i}", pos, 5.0m));
            }

            var extraPlayer = CreatePlayer("Extra Player", PlayerPosition.Midfielder, 5.0m);

            // Act & Assert
            Action act = () => AddPlayerToTeam(team, extraPlayer);
            act.Should().Throw<DomainException>().WithMessage("*15 players*");
        }

        [Fact]
        public void AddPlayer_ExceedsSameClubLimit_ShouldThrowDomainException()
        {
            // Arrange
            var team = FantasyTeam.Create("My Dream Team", _manager.Id);
            var clubId = Guid.NewGuid();

            AddPlayerToTeam(team, CreatePlayer("Player 1", PlayerPosition.Defender, 5.0m, clubId));
            AddPlayerToTeam(team, CreatePlayer("Player 2", PlayerPosition.Defender, 5.0m, clubId));
            AddPlayerToTeam(team, CreatePlayer("Player 3", PlayerPosition.Defender, 5.0m, clubId));

            var fourthPlayer = CreatePlayer("Player 4", PlayerPosition.Defender, 5.0m, clubId);

            // Act & Assert
            Action act = () => AddPlayerToTeam(team, fourthPlayer);
            act.Should().Throw<DomainException>().WithMessage("*more than 3 players*");
        }

        [Fact]
        public void AddPlayer_ExceedsPositionLimit_ShouldThrowDomainException()
        {
            // Arrange
            var team = FantasyTeam.Create("My Dream Team", _manager.Id);
            AddPlayerToTeam(team, CreatePlayer("GKP 1", PlayerPosition.Goalkeeper, 5.0m));
            AddPlayerToTeam(team, CreatePlayer("GKP 2", PlayerPosition.Goalkeeper, 5.0m));

            var thirdGkp = CreatePlayer("GKP 3", PlayerPosition.Goalkeeper, 5.0m);

            // Act & Assert
            Action act = () => AddPlayerToTeam(team, thirdGkp);
            act.Should().Throw<DomainException>().WithMessage("*goalkeepers*");
        }

        [Fact]
        public void SetCaptain_ReplacesOldCaptain()
        {
            // Arrange
            var team = FantasyTeam.Create("My Dream Team", _manager.Id);
            var player1 = CreatePlayer("Salah", PlayerPosition.Midfielder, 10.0m);
            var player2 = CreatePlayer("Haaland", PlayerPosition.Forward, 10.0m);

            AddPlayerToTeam(team, player1);
            AddPlayerToTeam(team, player2);

            team.SetCaptain(player1.Id);

            // Act
            team.SetCaptain(player2.Id);

            // Assert
            team.Players.First(p => p.PlayerId == player2.Id).IsCaptain.Should().BeTrue();
            team.Players.First(p => p.PlayerId == player1.Id).IsCaptain.Should().BeFalse();
        }

        [Fact]
        public void SetCaptain_PlayerNotInTeam_ShouldThrowDomainException()
        {
            // Arrange
            var team = FantasyTeam.Create("My Dream Team", _manager.Id);

            // Act & Assert
            Action act = () => team.SetCaptain(Guid.NewGuid());
            act.Should().Throw<DomainException>().WithMessage("*squad*");
        }

        [Fact]
        public void SubstitutePlayer_LegalSubstitution_ShouldSucceed()
        {
            // Arrange
            var team = FantasyTeam.Create("My Dream Team", _manager.Id);
            
            // Build a valid 15-man squad
            var starters = new List<Player>();
            var bench = new List<Player>();
            
            // 1 GKP, 3 DEF, 4 MID, 3 FWD (Valid 3-4-3)
            starters.Add(CreatePlayer("S_GKP", PlayerPosition.Goalkeeper, 5.0m));
            for (int i=0; i<3; i++) starters.Add(CreatePlayer($"S_DEF_{i}", PlayerPosition.Defender, 5.0m));
            for (int i=0; i<4; i++) starters.Add(CreatePlayer($"S_MID_{i}", PlayerPosition.Midfielder, 5.0m));
            for (int i=0; i<3; i++) starters.Add(CreatePlayer($"S_FWD_{i}", PlayerPosition.Forward, 5.0m));

            // Bench: 1 GKP, 2 DEF, 1 MID
            bench.Add(CreatePlayer("B_GKP", PlayerPosition.Goalkeeper, 4.0m));
            bench.Add(CreatePlayer("B_DEF_1", PlayerPosition.Defender, 4.0m));
            bench.Add(CreatePlayer("B_DEF_2", PlayerPosition.Defender, 4.0m));
            bench.Add(CreatePlayer("B_MID_1", PlayerPosition.Midfielder, 4.0m));

            foreach(var p in starters) AddPlayerToTeam(team, p, false);
            foreach(var p in bench) AddPlayerToTeam(team, p, true);

            var playerOut = starters.First(p => p.Position == PlayerPosition.Midfielder);
            var playerIn = bench.First(p => p.Position == PlayerPosition.Midfielder);

            // Act
            team.SubstitutePlayer(playerIn.Id, playerOut.Id);

            // Assert
            team.Players.First(p => p.PlayerId == playerOut.Id).IsOnBench.Should().BeTrue();
            team.Players.First(p => p.PlayerId == playerIn.Id).IsOnBench.Should().BeFalse();
        }

        [Fact]
        public void SubstitutePlayer_BreaksFormation_ShouldThrowDomainException()
        {
            // Arrange
            var team = FantasyTeam.Create("My Dream Team", _manager.Id);
            
            var starters = new List<Player>();
            var bench = new List<Player>();
            
            // 1 GKP, 3 DEF, 4 MID, 3 FWD (Valid 3-4-3)
            starters.Add(CreatePlayer("S_GKP", PlayerPosition.Goalkeeper, 5.0m));
            for (int i=0; i<3; i++) starters.Add(CreatePlayer($"S_DEF_{i}", PlayerPosition.Defender, 5.0m)); // Exactly 3 defenders
            for (int i=0; i<4; i++) starters.Add(CreatePlayer($"S_MID_{i}", PlayerPosition.Midfielder, 5.0m));
            for (int i=0; i<3; i++) starters.Add(CreatePlayer($"S_FWD_{i}", PlayerPosition.Forward, 5.0m));

            // Bench: 1 GKP, 2 DEF, 1 MID
            bench.Add(CreatePlayer("B_GKP", PlayerPosition.Goalkeeper, 4.0m));
            bench.Add(CreatePlayer("B_DEF_1", PlayerPosition.Defender, 4.0m));
            bench.Add(CreatePlayer("B_DEF_2", PlayerPosition.Defender, 4.0m));
            bench.Add(CreatePlayer("B_MID_1", PlayerPosition.Midfielder, 4.0m));

            foreach(var p in starters) AddPlayerToTeam(team, p, false);
            foreach(var p in bench) AddPlayerToTeam(team, p, true);

            // Sub out a defender (leaving 2) and sub in a midfielder (making 5). Formation 2-5-3 is invalid!
            var playerOut = starters.First(p => p.Position == PlayerPosition.Defender);
            var playerIn = bench.First(p => p.Position == PlayerPosition.Midfielder);

            // Act & Assert
            Action act = () => team.SubstitutePlayer(playerIn.Id, playerOut.Id);
            act.Should().Throw<DomainException>().WithMessage("*formation*");
        }
    }
}
