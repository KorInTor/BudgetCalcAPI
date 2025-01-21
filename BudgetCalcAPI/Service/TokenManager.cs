using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BudgetCalcAPI.Service
{
	public class TokenManager
	{
		private readonly IConfiguration _configuration;

		public TokenManager(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GenerateJwtToken(int userId, int familyId)
		{
			var jwtSecretKey = _configuration["Jwt:Key"];
			var jwtIssuer = _configuration["Jwt:Issuer"];
			var jwtAudience = _configuration["Jwt:Audience"];

			var claims = new[]
			{
				new Claim("userId", userId.ToString()),
				new Claim("familyId", familyId.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: jwtIssuer,
				audience: jwtAudience,
				claims: claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
