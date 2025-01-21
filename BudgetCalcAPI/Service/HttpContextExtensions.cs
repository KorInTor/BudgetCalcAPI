using System.Security.Claims;

namespace BudgetCalcAPI.Service
{
	public static class HttpContextExtensions
	{
		public static int GetUserId(this HttpContext context)
		{
			return int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);
		}

		public static int GetFamilyGroupId(this HttpContext context)
		{
			return int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "familyId")?.Value);
		}

	}

}
