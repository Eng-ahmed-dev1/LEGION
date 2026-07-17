namespace FantasyFootball.IntegrationTests
{
    public class ComprehensiveEndpointTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private static string _token = string.Empty;
        public ComprehensiveEndpointTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        private static readonly SemaphoreSlim _authLock = new SemaphoreSlim(1, 1);

        private async Task AuthenticateAsync()
        {
            if (!string.IsNullOrEmpty(_token)) 
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                return;
            }

            await _authLock.WaitAsync();
            try
            {
                if (!string.IsNullOrEmpty(_token)) 
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    return;
                }

                var email = "comprehensive@test.com";
                var registerCommand = new RegisterCommand(email, "Password123!", "Comp Team", "CompUser");
                var registerResponse = await _client.PostAsJsonAsync("/api/Authentication/register", registerCommand);
                if (!registerResponse.IsSuccessStatusCode)
                {
                    // Ignore already taken error in case of weird test retry/concurrency
                    var error = await registerResponse.Content.ReadAsStringAsync();
                    if (!error.Contains("already taken"))
                    {
                        throw new Exception($"Register failed: {error}");
                    }
                }

                var loginCommand = new LoginCommand(email, "Password123!");
                var loginResponse = await _client.PostAsJsonAsync("/api/Authentication/login", loginCommand);
                var rawLoginJson = await loginResponse.Content.ReadAsStringAsync();
                
                if (!loginResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"Login failed: {loginResponse.StatusCode} - {rawLoginJson}");
                }
            
            var rawJson = rawLoginJson;
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
            _token = token;
            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            }
            finally
            {
                _authLock.Release();
            }
        }

        [Theory]
        // Players
        [InlineData("GET", "/api/Player")]
        [InlineData("GET", "/api/Player/11111111-1111-1111-1111-111111111111")]
        [InlineData("GET", "/api/Player/team/11111111-1111-1111-1111-111111111111")]
        [InlineData("POST", "/api/Player", "{\"firstName\":\"Test\",\"lastName\":\"Player\",\"position\":\"Forward\",\"price\":10.5,\"teamId\":\"11111111-1111-1111-1111-111111111111\"}")]
        [InlineData("PUT", "/api/Player/11111111-1111-1111-1111-111111111111", "{\"firstName\":\"Updated\",\"lastName\":\"Player\",\"position\":\"Midfielder\",\"price\":9.5,\"teamId\":\"11111111-1111-1111-1111-111111111111\"}")]
        [InlineData("DELETE", "/api/Player/11111111-1111-1111-1111-111111111111")]
        // Teams
        [InlineData("GET", "/api/Teams")]
        [InlineData("GET", "/api/Teams/11111111-1111-1111-1111-111111111111")]
        [InlineData("POST", "/api/Teams", "{\"name\":\"Test Team\",\"shortName\":\"TST\"}")]
        // Fantasy Teams
        [InlineData("GET", "/api/FantasyTeam")]
        [InlineData("GET", "/api/FantasyTeam/11111111-1111-1111-1111-111111111111")]
        [InlineData("GET", "/api/FantasyTeam/manager/11111111-1111-1111-1111-111111111111")]
        [InlineData("POST", "/api/FantasyTeam/11111111-1111-1111-1111-111111111111/players", "{\"playerId\":\"22222222-2222-2222-2222-222222222222\",\"isOnBench\":true}")]
        [InlineData("DELETE", "/api/FantasyTeam/11111111-1111-1111-1111-111111111111/players/22222222-2222-2222-2222-222222222222")]
        [InlineData("POST", "/api/FantasyTeam/11111111-1111-1111-1111-111111111111/chips", "{\"chipType\":0,\"gameweekId\":\"11111111-1111-1111-1111-111111111111\"}")]
        [InlineData("POST", "/api/FantasyTeam/11111111-1111-1111-1111-111111111111/toggle-auto-sub")]
        // Leagues
        [InlineData("GET", "/api/Leagues")]
        [InlineData("GET", "/api/Leagues/11111111-1111-1111-1111-111111111111")]
        [InlineData("POST", "/api/Leagues", "{\"name\":\"Test League\",\"createdById\":\"11111111-1111-1111-1111-111111111111\"}")]
        [InlineData("POST", "/api/Leagues/join", "{\"joinCode\":\"123456\"}")]
        // Gameweeks
        [InlineData("GET", "/api/Gameweeks")]
        [InlineData("POST", "/api/Gameweeks", "{\"number\":1,\"deadline\":\"2026-08-01T00:00:00Z\"}")]
        // Fixtures
        [InlineData("GET", "/api/Fixture")]
        [InlineData("POST", "/api/Fixture", "{\"gameweekId\":\"11111111-1111-1111-1111-111111111111\",\"homeTeamId\":\"22222222-2222-2222-2222-222222222222\",\"awayTeamId\":\"33333333-3333-3333-3333-333333333333\",\"kickOff\":\"2026-08-01T00:00:00Z\"}")]
        // Managers
        [InlineData("GET", "/api/Managers")]
        [InlineData("GET", "/api/Managers/leaderboard")]
        public async Task Endpoint_WithDummyData_ShouldNotThrow500_And_BeAuthenticated(string method, string route, string jsonBody = "")
        {
            // Arrange
            await AuthenticateAsync();

            var request = new HttpRequestMessage(new HttpMethod(method), route);
            if (!string.IsNullOrEmpty(jsonBody))
            {
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            }

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            // 401 means Authorization failed (which is bad since we sent a token)
            // 500 means the server crashed (which is very bad, bug in code)
            // 400 or 404 are EXPECTED because we are sending fake GUIDs and fake data!
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new Exception($"Endpoint crashed with 500: {errorBody}");
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new Exception($"Endpoint rejected valid token: {errorBody}");
            }
        }
    }
}
