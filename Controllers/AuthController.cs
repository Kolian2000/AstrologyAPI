using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using NewWebApi.Interface;
using NewWebApi.Models.AuthModel;
using NewWebApi.Services.Attributes;

namespace NewWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		public IRepository _repository { get; set; }
		

		public AuthController(IRepository repository)
		{
			_repository = repository;
		}
		[HttpPost("Register")]
		
		public async Task<ActionResult> Register(UserDto user)
		{	
			var results =  await _repository.CheckUserExists(user);
			if(results.IsSuccess)
			{
				return BadRequest(results);
			}
			
			var users = new User();
			var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
			
			users.PasswordHash = passwordHash;
			users.Name = user.Name;
			
			var result = await _repository.AddUser(users);
			return Ok(result);
			
			
		}
		
		
	}
}