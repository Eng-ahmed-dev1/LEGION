namespace FantasyFootball.Application.UseCases.Teams.Commands.UpdateTeam
{
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Team Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Team name is required.");

            RuleFor(x => x.ShortName)
                .NotEmpty()
                .WithMessage("Team short name is required.");
        }
    }
}
