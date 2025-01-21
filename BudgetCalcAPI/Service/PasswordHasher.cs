using System.Security.Cryptography;

namespace BudgetCalcAPI.Service
{
	public class PasswordHasher
	{
		public string HashPassword(string password)
		{
			var salt = new byte[16];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100, HashAlgorithmName.SHA256);
			var hash = pbkdf2.GetBytes(32);
			return Convert.ToBase64String(salt.Concat(hash).ToArray()); //соль храним в самом хэше как говорит pbkdf2
		}

		public bool VerifyPassword(string storedHash, string password)
		{
			var salt = Convert.FromBase64String(storedHash).Take(16).ToArray();
			var storedPasswordHash = Convert.FromBase64String(storedHash).Skip(16).ToArray();

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100, HashAlgorithmName.SHA256);
			var hash = pbkdf2.GetBytes(32);
			return storedPasswordHash.SequenceEqual(hash);
		}
	}
}
