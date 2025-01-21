using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetCalcAPI.Model;

namespace BudgetCalcAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly ApplicationContext _context;

		public UserController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers([FromQuery] int[]ids)
		{
			var users = await _context.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
			return Ok(users);
		}

		[HttpGet("familygroup/{id}")]
		public async Task<IActionResult> GetFamilyGroupUsers(int id)
		{
			var users = await _context.Users.Where(t => t.Id == id).ToListAsync();
			return Ok(users);
		}

		[HttpPut]
		public async Task<IActionResult> ChangeUserInfo([FromBody] User user)
		{
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
			return Ok(user);
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] User user)
		{
			if (user == null)
			{
				return BadRequest("new user not provided");
			}
			user.Id = user.Id != 0 ? 0 : user.Id;
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return Ok(user);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var user = await _context.Users.Where(u => u.Id == id).SingleAsync();
			if (user == null)
				return NotFound("User not found");
			_context.Remove(user);
			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}
