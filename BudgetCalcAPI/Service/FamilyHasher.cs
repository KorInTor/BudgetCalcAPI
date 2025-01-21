using BudgetCalcAPI.Model.UserDTO;
using System.Security.Cryptography;
using System.Text;

namespace BudgetCalcAPI.Service
{
	public class FamilyHasher
	{
		public string HashFamilyInfo(UserDTO[] users)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach(var user in users)
			{
				stringBuilder.Append(user.FullName);
				stringBuilder.Append(user.FamilyGroupId);
				stringBuilder.Append(user.Id);
			}
			byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
			byte[] hashBytes = SHA256.HashData(bytes);
			return Convert.ToHexString(hashBytes) + "." + users[0].FamilyGroupId;
		}

		public bool VerifyFamilyHash(string hash, UserDTO[] users)
		{
			return hash.SequenceEqual(HashFamilyInfo(users));
		}
	}
}
