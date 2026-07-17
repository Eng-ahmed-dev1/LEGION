
namespace FantasyFootball.Application.Common.Abstractions.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return new OkObjectResult(result);
            if (result.ValidationErrors.Any())
                return new BadRequestObjectResult(result.ValidationErrors);
            return new BadRequestObjectResult(result.Error);
        }
    }
}
