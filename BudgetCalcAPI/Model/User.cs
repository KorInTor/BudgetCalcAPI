using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetCalcAPI.Model
{
	[Table("user")]
	public class User
	{
		public int Id { get; set; }

		public string Name { get; set; }

		[Column("family_group")]
		public int? FamilyGroupId { get; set; }
	}
}
