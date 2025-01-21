using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetCalcAPI.Model.Transactions
{
    [Table("transaction_category")]
    public class TransactionCategory
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; }

        [Column("type_id")]
        [ForeignKey("Type")]
        public int? TypeId { get; set; }

        public TransactionType? Type { get; set; }
    }
}
