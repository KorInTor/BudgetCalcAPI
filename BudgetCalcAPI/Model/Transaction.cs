using BudgetCalcAPI.Model.Transactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetCalcAPI.Model
{
	[Table("transaction")]
	public class Transaction
	{
		public long Id { get; set; }

		[Column("category_id")]
		[ForeignKey("Category")]
		public int? CategoryId { get; set; }

		public TransactionCategory? Category { get; set; }

		[Column("user_id")]
		public int UserId { get; set; }

		public decimal Amount { get; set; }

		[Column("created_at")]
		public DateTime CreatedAt { get; set; }
	}
}
