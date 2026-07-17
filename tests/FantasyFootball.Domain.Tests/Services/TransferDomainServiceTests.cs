namespace FantasyFootball.Domain.Tests.Services
{
    public class TransferDomainServiceTests
    {
        private readonly TransferDomainService _sut;
        private readonly Manager _manager;

        public TransferDomainServiceTests()
        {
            _sut = new TransferDomainService();
            _manager = Manager.Create("Test Team", Guid.NewGuid(), "testuser");
        }

        private Player CreatePlayer(PlayerPosition position, decimal priceAmount)
        {
            return Player.Create("First", "Last", position, new FantasyFootball.Domain.ValueObjects.Price(priceAmount), Guid.NewGuid());
        }

        private void AddPlayerToTeam(FantasyTeam team, Player player)
        {
            var fp = FantasyPlayer.Create(team.Id, player.Id, false);
            var field = typeof(FantasyPlayer).GetField("<Player>k__BackingField", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null) field.SetValue(fp, player);
            else typeof(FantasyPlayer).GetProperty("Player")!.SetValue(fp, player);
            team.AddPlayer(fp, player);
        }

        [Fact]
        public void Execute_ValidTransferWithFreeTransfers_ShouldReturnTrueAndDeductTransfer()
        {
            // Arrange
            var team = FantasyTeam.Create("My Team", _manager.Id);
            team.ResetFreeTransfers(); // Ensure 1 free transfer
            
            var playerOut = CreatePlayer(PlayerPosition.Forward, 10.0m);
            var playerIn = CreatePlayer(PlayerPosition.Forward, 9.0m);
            
            AddPlayerToTeam(team, playerOut);
            decimal initialBudget = team.Budget;

            var gameweek = Gameweek.Create(1, DateTime.UtcNow.AddDays(1));

            // Act
            bool isFree = _sut.Execute(team, playerIn, playerOut, gameweek);

            // Assert
            isFree.Should().BeTrue();
            team.FreeTransfers.Should().Be(0);
            team.Players.Should().ContainSingle(p => p.PlayerId == playerIn.Id);
            team.Players.Should().NotContain(p => p.PlayerId == playerOut.Id);
            team.Budget.Should().Be(initialBudget + 1.0m);
        }

        [Fact]
        public void Execute_ValidTransferWithoutFreeTransfers_ShouldReturnFalse()
        {
            // Arrange
            var team = FantasyTeam.Create("My Team", _manager.Id);
            
            // Use up free transfers
            while(team.FreeTransfers > 0) team.UseTransfer();

            var playerOut = CreatePlayer(PlayerPosition.Forward, 10.0m);
            var playerIn = CreatePlayer(PlayerPosition.Forward, 9.0m);
            
            AddPlayerToTeam(team, playerOut);

            var gameweek = Gameweek.Create(1, DateTime.UtcNow.AddDays(1));

            // Act
            bool isFree = _sut.Execute(team, playerIn, playerOut, gameweek);

            // Assert
            isFree.Should().BeFalse();
        }

        [Fact]
        public void Execute_DifferentPositions_ShouldThrowDomainException()
        {
            var team = FantasyTeam.Create("My Team", _manager.Id);
            var playerOut = CreatePlayer(PlayerPosition.Forward, 10.0m);
            var playerIn = CreatePlayer(PlayerPosition.Midfielder, 9.0m);
            
            AddPlayerToTeam(team, playerOut);
            var gameweek = Gameweek.Create(1, DateTime.UtcNow.AddDays(1));

            Action act = () => _sut.Execute(team, playerIn, playerOut, gameweek);
            act.Should().Throw<DomainException>().WithMessage("*same position*");
        }

        [Fact]
        public void Execute_InsufficientBudget_ShouldThrowDomainException()
        {
            var team = FantasyTeam.Create("My Team", _manager.Id);
            
            var budgetField = typeof(FantasyTeam).GetField("<Budget>k__BackingField", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (budgetField != null) budgetField.SetValue(team, 5.0m);
            else typeof(FantasyTeam).GetProperty("Budget")!.SetValue(team, 5.0m);

            var playerOut = CreatePlayer(PlayerPosition.Forward, 5.0m); // Leaves 0
            AddPlayerToTeam(team, playerOut);

            var expensivePlayerIn = CreatePlayer(PlayerPosition.Forward, 15.0m); // Need 15, have 5
            
            var gameweek = Gameweek.Create(1, DateTime.UtcNow.AddDays(1));

            Action act = () => _sut.Execute(team, expensivePlayerIn, playerOut, gameweek);
            act.Should().Throw<DomainException>().WithMessage("*Insufficient budget*");
        }

        [Fact]
        public void Execute_GameweekFinished_ShouldThrowDomainException()
        {
            var team = FantasyTeam.Create("My Team", _manager.Id);
            var playerOut = CreatePlayer(PlayerPosition.Forward, 10.0m);
            var playerIn = CreatePlayer(PlayerPosition.Forward, 9.0m);
            AddPlayerToTeam(team, playerOut);
            
            var gameweek = Gameweek.Create(1, DateTime.UtcNow.AddDays(1));
            // Force finish (usually requires activation first, but let's assume we can mock or just activate then finish)
            var fixture = Fixture.Create(gameweek.Id, Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddDays(2));
            gameweek.AddFixture(fixture);
            gameweek.Activate();
            fixture.RecordResult(1, 0);
            gameweek.Finish();

            Action act = () => _sut.Execute(team, playerIn, playerOut, gameweek);
            act.Should().Throw<DomainException>().WithMessage("*finished*");
        }
    }
}
