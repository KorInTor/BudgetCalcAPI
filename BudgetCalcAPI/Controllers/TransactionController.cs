using BudgetCalcAPI.Model;
using BudgetCalcAPI.Model.Filter;
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
			var query = _context.Transactions.AsQueryable();

			if (transactionFilter.CategoriesIds != null && transactionFilter.CategoriesIds.Count != 0)
			{
				query = query.Where(t => t.CategoryId.HasValue && transactionFilter.CategoriesIds.Contains(t.CategoryId.Value));
			}

			if (transactionFilter.TypesIds != null && transactionFilter.TypesIds.Count != 0)
			{
				query = query.Where(t => t.Category.TypeId.HasValue && transactionFilter.TypesIds.Contains(t.Category.TypeId.Value));
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

			if (transactionFilter.FamilyGroupId != null)
			{
				var familyGroupUserIds = _context.Users
					.Where(u => u.FamilyGroupId == transactionFilter.FamilyGroupId)
					.Select(u => u.Id);

				query = query.Where(t => familyGroupUserIds
				.Contains(t.UserId) || (transactionFilter.UserIds != null && transactionFilter.UserIds.Contains(t.UserId)));
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
			{
				return BadRequest("Transaction is null");
			}

			_context.Transactions.Add(transaction);
			await _context.SaveChangesAsync();
			return Ok(transaction);
		}
	}
}
