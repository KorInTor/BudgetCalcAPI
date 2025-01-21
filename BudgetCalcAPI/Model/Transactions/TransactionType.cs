using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetCalcAPI.Model.Transactions
{
    [Table("transaction_type")]
    public class TransactionType
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
    }
}
