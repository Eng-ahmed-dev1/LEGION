namespace FantasyFootball.Infrastructure.Providers.ApiFootball;

public static class ApiFootballEndpoints
{
    public static string Teams(int league, int season) => $"teams?league={league}&season={season}";
    public static string Players(int league, int season, int page = 1) => $"players?league={league}&season={season}&page={page}";
    public static string Fixtures(int league, int season) => $"fixtures?league={league}&season={season}";
    public static string Standings(int league, int season) => $"standings?league={league}&season={season}";
    public static string Injuries(int league, int season) => $"injuries?league={league}&season={season}";
    public static string FixtureEvents(int fixture) => $"fixtures/events?fixture={fixture}";
    public static string FixturePlayers(int fixture) => $"fixtures/players?fixture={fixture}";
}
