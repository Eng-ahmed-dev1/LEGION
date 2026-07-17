namespace FantasyFootball.Infrastructure.Providers.ApiFootball.Mappers;

public interface IApiFootballMapper<in TDto, out TEntity>
{
    TEntity Map(TDto dto);
}
