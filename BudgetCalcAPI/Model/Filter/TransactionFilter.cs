namespace BudgetCalcAPI.Model.Filter
{
	public class TransactionFilter
	{
		public DateTimeOffset? MinDateTime { get; set; }
		public DateTimeOffset? MaxDateTime { get; set; }
		public List<int>? UserIds { get; set; }
		public int? FamilyGroupId { get; set; }
		public List<int>? CategoriesIds { get; set; }
		public List<int>? TypesIds { get; set; }
		public int Skip { get; set; } = 0;
		public int Take { get; set; } = 0;
	}
}
