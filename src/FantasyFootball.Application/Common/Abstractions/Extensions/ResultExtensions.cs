namespace FantasyFootball.API.Extensions
{
    public static class ResultExtensions
    {
        public static ActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return new OkObjectResult(result.Value);

            if (result.ValidationErrors.Any())
            {
                var modelState = new ModelStateDictionary();
                foreach (var error in result.ValidationErrors)
                    modelState.AddModelError(error.Property, error.Message);

                return new BadRequestObjectResult(new ValidationProblemDetails(modelState));
            }

            return new BadRequestObjectResult(new ProblemDetails
            {
                Title = "Request failed",
                Detail = result.Error,
                Status = StatusCodes.Status400BadRequest
            });
        }
    }
}
