namespace FantasyFootball.Application.Mappings
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Player, PlayerDto>()
                .Map(dest => dest.Price, src => src.Price.Value)
                .Map(dest => dest.Position, src => src.Position.ToString())
                .Map(dest => dest.TotalPoints, src => src.TotalPoints.Point);
            config.NewConfig<PlayerEvent, PlayerEventDto>()
           .Map(dest => dest.EventType, src => src.EventType.ToString());
        }
    }
}
