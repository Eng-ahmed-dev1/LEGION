namespace FantasyFootball.UnitTests
{
    public class ToggleAutoSubCommandHandlerTests
    {
        private readonly Mock<IFantasyTeamRepository> _teamRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ToggleAutoSubCommandHandler _handler;

        public ToggleAutoSubCommandHandlerTests()
        {
            _teamRepositoryMock = new Mock<IFantasyTeamRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new ToggleAutoSubCommandHandler(_teamRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_WhenTeamExists_TogglesAutoSubAndReturnsSuccess()
        {
            // Arrange
            var teamId = Guid.NewGuid();
            var team = FantasyTeam.Create("My Team", Guid.NewGuid());
            
            // Assume default is true for newly created team, let's explicitly verify
            var initialState = team.AutoSubEnabled;

            _teamRepositoryMock.Setup(repo => repo.GetByIdAsync(teamId))
                .ReturnsAsync(team);

            var command = new ToggleAutoSubCommand(teamId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            team.AutoSubEnabled.Should().Be(!initialState);
            _teamRepositoryMock.Verify(repo => repo.Update(team), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenTeamDoesNotExist_ReturnsFailure()
        {
            // Arrange
            var teamId = Guid.NewGuid();
            _teamRepositoryMock.Setup(repo => repo.GetByIdAsync(teamId))
                .ReturnsAsync((FantasyTeam)null!);

            var command = new ToggleAutoSubCommand(teamId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error!.Code.Should().Be("FantasyTeamNotFound");
            _teamRepositoryMock.Verify(repo => repo.Update(It.IsAny<FantasyTeam>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
