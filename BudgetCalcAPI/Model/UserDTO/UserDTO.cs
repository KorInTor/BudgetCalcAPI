namespace BudgetCalcAPI.Model.UserDTO
{
	public class UserDTO
	{
		public UserDTO(User u)
		{
			Id = u.Id;
			FullName = u.FullName;
			FamilyGroupId = u.FamilyGroupId;
		}

		public int Id { get; set; }

		public string FullName { get; set; }

		public int FamilyGroupId { get; set; }
	}
}
