using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BudgetCalcAPI.Filters
{
	public class TokenValidationFilter : IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var userIdClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
			var familyGroupIdClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "familyId")?.Value;

			if (userIdClaim == null || familyGroupIdClaim == null)
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			if (!int.TryParse(userIdClaim, out _) || !int.TryParse(familyGroupIdClaim, out _))
			{
				context.Result = new UnauthorizedResult();
				return;
			}
		}
	}
}
