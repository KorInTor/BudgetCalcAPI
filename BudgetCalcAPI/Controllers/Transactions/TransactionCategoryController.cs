using BudgetCalcAPI.Filters;
using BudgetCalcAPI.Model;
using BudgetCalcAPI.Model.Transactions;
using BudgetCalcAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalcAPI.Controllers.Transactions
{
	[Route("api/transactions/categories")]
	[ApiController]
	[Authorize]
	[TokenValidation]
	public class TransactionCategoryController : ControllerBase
	{
		private readonly ApplicationContext _context;

		public TransactionCategoryController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetCategories()
		{
			var familyGroupId = HttpContext.GetFamilyGroupId();

			var categories = await _context.TransactionCategories.Where(c => c.Type.OwnerId == familyGroupId).Include(c => c.Type).ToListAsync();
			return Ok(categories);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory([FromBody] TransactionCategory transactionCategory)
		{
			if (transactionCategory == null)
				return BadRequest("Category is null");
			if (transactionCategory.TypeId == null)
				return BadRequest("TypeId cannot be null");

			var type = await _context.TransactionTypes.Where(t => t.Id == transactionCategory.TypeId).SingleOrDefaultAsync();

			if (type == null)
				return NotFound("Type does not exists");
			if (type.OwnerId != HttpContext.GetFamilyGroupId())
				return Unauthorized();

			transactionCategory.Id = transactionCategory.Id != 0 ? 0 : transactionCategory.Id;
			_context.TransactionCategories.Add(transactionCategory);
			await _context.SaveChangesAsync();
			return Ok(transactionCategory);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			if (id <= 0)
			{
				return BadRequest("Category id is invalid");
			}

			var foundedCategory = await _context.TransactionCategories.Where(category => category.Id == id).Include(c => c.Type).SingleAsync();

			if (foundedCategory == null)
				return NotFound("Category not found");
			if (foundedCategory.Type.OwnerId != HttpContext.GetFamilyGroupId())
				return Unauthorized();

			_context.TransactionCategories.Remove(foundedCategory);
			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}
