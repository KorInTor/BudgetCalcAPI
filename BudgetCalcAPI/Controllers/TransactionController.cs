using BudgetCalcAPI.Model;
using BudgetCalcAPI.Model.Filter;
using BudgetCalcAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalcAPI.Controllers
{
	[Route("api/transactions")]
	[ApiController]
	public class TransactionsController : ControllerBase
	{
		private readonly ApplicationContext _context;

		public TransactionsController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetTransactions([FromQuery] TransactionFilter transactionFilter)
		{
			var familyGroupUserIds = _context.Users
					.Where(u => u.FamilyGroupId == HttpContext.GetFamilyGroupId())
					.Select(u => u.Id);

			var query = _context.Transactions.Where(t => familyGroupUserIds
				.Contains(t.UserId) || (transactionFilter.UserIds != null && transactionFilter.UserIds.Contains(t.UserId)));

			if (transactionFilter.CategoriesIds != null && transactionFilter.CategoriesIds.Count != 0)
			{
				query = query.Where(t => t.CategoryId.HasValue && transactionFilter.CategoriesIds.Contains(t.CategoryId.Value));
			}

			if (transactionFilter.TypesIds != null && transactionFilter.TypesIds.Count != 0)
			{
				query = query.Where(t => transactionFilter.TypesIds.Contains(t.Category.TypeId));
			}

			if (transactionFilter.MaxDateTime != null)
			{
				query = query.Where(t => t.CreatedAt <= transactionFilter.MaxDateTime);
			}

			if (transactionFilter.MinDateTime != null)
			{
				query = query.Where(t => t.CreatedAt >= transactionFilter.MinDateTime);
			}

			if (transactionFilter.UserIds != null && transactionFilter.UserIds.Count != 0)
			{
				query = query.Where(t => transactionFilter.UserIds.Contains(t.UserId));
			}

			if (transactionFilter.UserIds != null)
			{
				query = query.Where(t => transactionFilter.UserIds.Contains(t.UserId));
			}

			if (transactionFilter.Skip > 0)
			{
				query = query.Skip(transactionFilter.Skip);
			}

			if (transactionFilter.Take > 0)
			{
				query = query.Take(transactionFilter.Take);
			}

			var transactions = await query.ToListAsync();

			return Ok(transactions);
		}

		[HttpPost]
		public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
		{
			if (transaction == null)
				return BadRequest("Transaction is null");
			if (transaction.CategoryId == null)
				return BadRequest("CategoryId is missing");

			var category = await _context.TransactionCategories.Where(c => c.Id == transaction.CategoryId).SingleOrDefaultAsync();
			if (category == null)
				return NotFound("Invalid category");

			var type = await _context.TransactionTypes.Where(type => type.Id == category.TypeId).SingleOrDefaultAsync();
			if (type == null)
				return NotFound("Invalid type");
			if (type.OwnerId != HttpContext.GetFamilyGroupId())
				return Unauthorized();

			transaction.UserId = HttpContext.GetUserId();
			_context.Transactions.Add(transaction);
			await _context.SaveChangesAsync();
			return Ok(transaction);
		}
	}
}
