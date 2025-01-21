using BudgetCalcAPI.Model;
using BudgetCalcAPI.Model.UserDTO;
using BudgetCalcAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BudgetCalcAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UserController : Controller
	{
		private readonly ApplicationContext _context;
		private readonly TokenManager _tokenManager;

		public UserController(ApplicationContext context, TokenManager tokenManager)
		{
			_context = context;
			_tokenManager = tokenManager;
		}

		[HttpGet("familygroup")]
		public async Task<IActionResult> GetFamilyGroupUsers()
		{
			int contextFamilyGroupId = HttpContext.GetFamilyGroupId();

			var users = await _context.Users
				.Where(u => u.FamilyGroupId == contextFamilyGroupId)
				.ToListAsync();

			var userDTOs = users.Select(u => new UserDTO(u)).ToList();

			if (userDTOs == null || userDTOs.Count == 0)
				return BadRequest("Invalid family, update token");

			return Ok(userDTOs);
		}

		[HttpGet("familygroup/invitecode")]
		public async Task<IActionResult> GetFamilyInviteCode()
		{
			int contextFamilyGroupId = HttpContext.GetFamilyGroupId();

			var users = await _context.Users
				.Where(u => u.FamilyGroupId == contextFamilyGroupId)
				.ToListAsync();

			var userDTOs = users.Select(u => new UserDTO(u)).ToArray();

			if (users == null || users.Count == 0)
				return BadRequest("Invalid family, update token");

			var hasher = new FamilyHasher();
			return Ok(hasher.HashFamilyInfo(userDTOs));
		}

		[HttpPut("familygroup/change")]
		public async Task<IActionResult> ChangeFamilyGroup([FromBody] string familyInviteCode)
		{
			if (familyInviteCode.Split(".").Length != 2)
				return BadRequest("Request code is Invalid");
			if (!int.TryParse(familyInviteCode.Split(".")[1], out int newFamilyGroupId))
				return BadRequest("Request code is Invalid");

			var users = await _context.Users
				.Where(u => u.FamilyGroupId == newFamilyGroupId)
				.ToListAsync();

			var userDTOs = users.Select(u => new UserDTO(u)).ToArray();

			if (userDTOs == null || userDTOs.Length == 0)
				return BadRequest("Invalid family, update token");

			var hasher = new FamilyHasher();
			if (!hasher.VerifyFamilyHash(familyInviteCode, userDTOs))
				return BadRequest("Invalid invite code");

			var user = await _context.Users.Where(u => u.Id == HttpContext.GetUserId()).SingleOrDefaultAsync();
			if (user == null)
				return NotFound("User no longer exists");
			user.FamilyGroupId = userDTOs.First().FamilyGroupId;
			_context.Update(user);

			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> ChangeUserInfo([FromBody] UserDTO newUserData)
		{
			var userId = HttpContext.GetUserId();

			if (userId != newUserData.Id)
				return Unauthorized();

			var user = await _context.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
			if (user == null)
				return NotFound();

			user.UpdateData(newUserData);
			_context.Users.Update(user);
			await _context.SaveChangesAsync();

			return Accepted();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			int? contextUserId = HttpContext.GetUserId();
			if (contextUserId == null || contextUserId != id)
				return Unauthorized();

			var user = await _context.Users.Where(u => u.Id == id).SingleAsync();
			if (user == null)
				return NotFound("User not found");
			_context.Remove(user);
			await _context.SaveChangesAsync();

			return Ok();
		}

	}
}
