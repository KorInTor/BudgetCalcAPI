using BudgetCalcAPI.Model.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalcAPI.Controllers.Transactions
{
	[Route("api/transactions/types")]
	[ApiController]
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
			var types = await _context.TransactionTypes.ToListAsync();
			return Ok(types);
		}

		[HttpPost]
		public async Task<IActionResult> CreateType([FromBody] string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				return BadRequest("Type name is null or empty");
			}

			var newType = new TransactionType { Name = typeName };
			_context.TransactionTypes.Add(newType);
			await _context.SaveChangesAsync();
			return Ok(newType);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteType(int id)
		{
			if (id <= 0)
			{
				return BadRequest("id is invalid");
			}

			var type = await _context.TransactionTypes.Where(type => type.Id == id).SingleAsync();

			if (type == null)
			{
				return NotFound("Type with given id not found");
			}

			_context.TransactionTypes.Remove(type);
			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}
