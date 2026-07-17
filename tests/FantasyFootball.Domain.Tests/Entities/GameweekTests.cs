namespace FantasyFootball.Domain.Tests.Entities
{
    public class GameweekTests
    {
        [Fact]
        public void Create_WithValidData_ShouldSucceed()
        {
            var gameweek = Gameweek.Create(1, DateTime.UtcNow.AddDays(1));


            gameweek.Number.Should().Be(1);
            gameweek.IsActive.Should().BeFalse();
            gameweek.IsFinished.Should().BeFalse();
        }

        [Fact]
        public void Activate_NoFixtures_ShouldThrowDomainException()
        {
            var gameweek = Gameweek.Create(1, DateTime.UtcNow.AddDays(1));
            
            Action act = () => gameweek.Activate();
            
            act.Should().Throw<DomainException>().WithMessage("*fixtures*");
        }

        [Fact]
        public void Activate_WithFixtures_ShouldSucceed()
        {
            var gameweek = Gameweek.Create(1, DateTime.UtcNow.AddDays(1));
            var fixture = Fixture.Create(gameweek.Id, Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddDays(2));
            gameweek.AddFixture(fixture);

            gameweek.Activate();

            gameweek.IsActive.Should().BeTrue();
        }

        [Fact]
        public void Finish_WhenNotActive_ShouldThrowDomainException()
        {
            var gameweek = Gameweek.Create(1, DateTime.UtcNow.AddDays(1));

            Action act = () => gameweek.Finish();

            act.Should().Throw<DomainException>().WithMessage("*inactive*");
        }

        [Fact]
        public void Finish_WhenActive_ShouldSucceed()
        {
            var gameweek = Gameweek.Create(1, DateTime.UtcNow.AddDays(1));
            var fixture = Fixture.Create(gameweek.Id, Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddDays(2));
            gameweek.AddFixture(fixture);
            gameweek.Activate();
            
            fixture.RecordResult(1, 0);

            gameweek.Finish();

            gameweek.IsActive.Should().BeFalse();
            gameweek.IsFinished.Should().BeTrue();
        }

        [Fact]
        public void RescheduleDeadline_PastDate_ShouldThrowDomainException()
        {
            var gameweek = Gameweek.Create(1, DateTime.UtcNow.AddDays(1));

            Action act = () => gameweek.RescheduleDeadline(DateTime.UtcNow.AddDays(-1));

            act.Should().Throw<DomainException>().WithMessage("*past*");
        }
    }
}
