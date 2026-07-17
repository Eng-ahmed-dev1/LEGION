namespace FantasyFootball.Application.UseCases.FantasyTeams.Commands.SubstitutePlayer;

public class SubstitutePlayerCommandValidator : AbstractValidator<SubstitutePlayerCommand>
{
    public SubstitutePlayerCommandValidator()
    {
        RuleFor(x => x.FantasyTeamId)
            .NotEmpty().WithMessage("FantasyTeamId is required.");

        RuleFor(x => x.PlayerInId)
            .NotEmpty().WithMessage("PlayerInId is required.");

        RuleFor(x => x.PlayerOutId)
            .NotEmpty().WithMessage("PlayerOutId is required.");

        RuleFor(x => x)
            .Must(x => x.PlayerInId != x.PlayerOutId)
            .WithMessage("Player coming in and out cannot be the same.");
    }
}
