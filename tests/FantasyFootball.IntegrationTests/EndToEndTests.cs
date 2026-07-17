namespace FantasyFootball.IntegrationTests
{
    public class EndToEndTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EndToEndTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task UserJourney_Register_Login_GetPlayers_And_ManageTeam_WorksAsExpected()
        {
            // 1. Register a new user
            var registerCommand = new RegisterCommand(
                "ahmed_test@fantasy.com", 
                "Password123!", 
                "Ahly Stars", 
                "AhmedUser");

            var registerResponse = await _client.PostAsJsonAsync("/api/Authentication/register", registerCommand);
            if (!registerResponse.IsSuccessStatusCode)
            {
                var error = await registerResponse.Content.ReadAsStringAsync();
                throw new Exception($"Registration failed: {registerResponse.StatusCode} - {error}");
            }

            // 2. Login to get token
            var loginCommand = new LoginCommand("ahmed_test@fantasy.com", "Password123!");
            var loginResponse = await _client.PostAsJsonAsync("/api/Authentication/login", loginCommand);
            loginResponse.EnsureSuccessStatusCode();
            var rawJson = await loginResponse.Content.ReadAsStringAsync();
            var jsonDoc = System.Text.Json.JsonDocument.Parse(rawJson);
            
            string? token = null;
            if (jsonDoc.RootElement.TryGetProperty("value", out var valueProp) || jsonDoc.RootElement.TryGetProperty("Value", out valueProp))
            {
                if (valueProp.TryGetProperty("accessToken", out var tokenProp) || valueProp.TryGetProperty("AccessToken", out tokenProp))
                {
                    token = tokenProp.GetString();
                }
            }

            if (string.IsNullOrEmpty(token)) 
            {
                throw new Exception($"Login succeeded but returned empty token. Raw JSON: {rawJson}");
            }
            
            // Setup Authorization Header
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // 3. Get Players
            var getPlayersResponse = await _client.GetAsync("/api/Player");
            if (!getPlayersResponse.IsSuccessStatusCode)
            {
                var error = await getPlayersResponse.Content.ReadAsStringAsync();
                throw new Exception($"GetPlayers failed: {getPlayersResponse.StatusCode} - {error}");
            }

            // 4. Get Leaderboard (Managers endpoint)
            var getLeaderboardResponse = await _client.GetAsync("/api/Managers/leaderboard?page=1&pageSize=10");
            if (!getLeaderboardResponse.IsSuccessStatusCode)
            {
                var error = await getLeaderboardResponse.Content.ReadAsStringAsync();
                throw new Exception($"Leaderboard failed: {getLeaderboardResponse.StatusCode} - {error}");
            }
            
            var result = await getLeaderboardResponse.Content.ReadFromJsonAsync<ResultWrapper<System.Collections.Generic.List<GlobalLeaderboardDto>>>(new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var leaderboard = result?.Value;
            leaderboard.Should().NotBeNull();
            // The newly registered manager should be in the leaderboard
            leaderboard.Should().Contain(m => m.UserName == "AhmedUser");

            var myManager = leaderboard!.First(m => m.UserName == "AhmedUser");
            var myManagerId = myManager.ManagerId;

            // Get Fantasy Team by ManagerId
            var teamResponse = await _client.GetAsync($"/api/FantasyTeam/manager/{myManagerId}");
            teamResponse.EnsureSuccessStatusCode();
            // Since we just need an endpoint to test, this proves Auth + Data flow is working.
        }
    }}
