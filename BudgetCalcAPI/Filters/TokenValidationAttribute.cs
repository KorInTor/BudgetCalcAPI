using Microsoft.AspNetCore.Mvc;

namespace BudgetCalcAPI.Filters
{
	public class TokenValidationAttribute : TypeFilterAttribute
	{
		public TokenValidationAttribute() : base(typeof(TokenValidationFilter))
		{
		}
	}

}
