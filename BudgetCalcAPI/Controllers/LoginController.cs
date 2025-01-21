using BudgetCalcAPI.Filters;
using BudgetCalcAPI.Model;
using BudgetCalcAPI.Model.UserDTO;
using BudgetCalcAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetCalcAPI.Controllers
{
	[Route("api/user")]
	[ApiController]
	public class LoginController : Controller
	{
		private readonly ApplicationContext _context;
		private readonly TokenManager _tokenManager;

		public LoginController(ApplicationContext context, TokenManager tokenManager)
		{
			_context = context;
			_tokenManager = tokenManager;
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDTO loginInfo)
		{
			var validUser = await _context.Users.Where(u => u.Username == loginInfo.Username).SingleOrDefaultAsync();
			if (validUser == null)
				return Unauthorized("Имя пользователя или Пароль не правильный");

			var hasher = new PasswordHasher();
			if (!hasher.VerifyPassword(validUser.PasswordHash, loginInfo.Password))
				return Unauthorized("Имя пользователя или Пароль не правильный");

			var token = _tokenManager.GenerateJwtToken(validUser.Id, validUser.FamilyGroupId);

			return Ok(new { UserId = validUser.Id, Token = token });
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] NewUserDTO newUser)
		{
			if (_context.Users.Where(u => u.Username == newUser.Username).SingleOrDefault() != null)
				return Conflict("this login already taken");

			var hasher = new PasswordHasher();
			var user = new User { FullName = newUser.FullName, Username = newUser.Username, PasswordHash = hasher.HashPassword(newUser.Password) };

			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			var token = _tokenManager.GenerateJwtToken(user.Id, user.FamilyGroupId);

			return Ok(new { UserId = user.Id, Token = token });
		}

		[Authorize]
		[TokenValidation]
		[HttpPut("newpassword")]
		public async Task<IActionResult> ChangePassword([FromBody] string newPassword)
		{
			var user = await _context.Users.Where(u => u.Id == HttpContext.GetUserId()).SingleOrDefaultAsync();
			if (user == null)
				return NotFound("This user no longer exists");

			var hasher = new PasswordHasher();
			user.PasswordHash = hasher.HashPassword(newPassword);

			_context.Update(user);
			await _context.SaveChangesAsync();

			return Ok();
		}

		[Authorize]
		[TokenValidation]
		[HttpPut("newusername")]
		public async Task<IActionResult> ChangeUsername([FromBody] string newPassword)
		{
			bool exists = await _context.Users
				.AnyAsync(u => u.Username == newPassword);
			if (exists)
				return BadRequest("Username already taken");

			var user = await _context.Users.Where(u => u.Id == HttpContext.GetUserId()).SingleOrDefaultAsync();
			if (user == null)
				return NotFound("This user no longer exists");

			user.Username = newPassword;

			_context.Update(user);
			await _context.SaveChangesAsync();

			return Ok();
		}
	}
}
