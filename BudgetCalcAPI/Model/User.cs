using BudgetCalcAPI.Model.UserDTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetCalcAPI.Model
{
	[Table("user")]
	public class User
	{
		public int Id { get; set; }

		public string FullName { get; set; }

		[Column("family_group_id")]
		public int FamilyGroupId { get; set; }

		[Column("username")]
		public string Username { get; set; }

		[Column("password_hash")]
		public string PasswordHash { get; set; }

		public User() { }

		public User(UserDTO.UserDTO userDTO)
		{
			Id = userDTO.Id;
			FullName = userDTO.FullName;
			FamilyGroupId = userDTO.FamilyGroupId;
		}

		public void UpdateData(UserDTO.UserDTO userDTO)
		{
			Id = userDTO.Id;
			FullName = userDTO.FullName;
			FamilyGroupId = userDTO.FamilyGroupId;
		}
	}
}
