using BudgetCalcAPI.Filters;
using BudgetCalcAPI.Model.Transactions;
using BudgetCalcAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalcAPI.Controllers.Transactions
{
	[Route("api/transactions/types")]
	[ApiController]
	[Authorize]
	[TokenValidation]
	public class TransactionTypeController : ControllerBase
	{
		private readonly ApplicationContext _context;

		public TransactionTypeController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetTypes()
		{
			var familyGroupId = HttpContext.GetFamilyGroupId();

			var types = await _context.TransactionTypes.Where(t => t.OwnerId == familyGroupId).ToListAsync();
			return Ok(types);
		}

		[HttpPost]
		public async Task<IActionResult> CreateType([FromBody] string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
				return BadRequest("Type name is null or empty");

			var familyGroupId = HttpContext.GetFamilyGroupId();

			var newType = new TransactionType { Name = typeName , OwnerId = familyGroupId };
			_context.TransactionTypes.Add(newType);
			await _context.SaveChangesAsync();
			return Ok(newType);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteType(int id)
		{
			if (id <= 0)
				return BadRequest("id is invalid");

			var type = await _context.TransactionTypes.Where(type => type.Id == id).SingleAsync();

			if (type == null)
				return NotFound("Type with given id not found");
			if (type.OwnerId != HttpContext.GetFamilyGroupId())
				return Unauthorized();

			_context.TransactionTypes.Remove(type);
			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}
