namespace FantasyFootball.IntegrationTests
{
    public class PlayerControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PlayerControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllPlayers_WhenUnauthenticated_ReturnsUnauthorized()
        {
            // Act
            var response = await _client.GetAsync("/api/Player");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
