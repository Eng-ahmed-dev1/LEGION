namespace FantasyFootball.Application.Common.Abstractions.Extensions
{
      public static class ResultExstension
    {
        public static ActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return new OkObjectResult(result);
            if (result.ValidationErrors.Any())
            {
                var modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
                foreach (var error in result.ValidationErrors)
                {
                    modelState.AddModelError(error.Property, error.Message);
                }
                return new BadRequestObjectResult(modelState);
            }
            return new BadRequestObjectResult(result.Error);
        }

    }
}


  

