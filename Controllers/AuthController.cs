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
		private readonly IReposi _iReposi;

		public AuthController(IReposi IReposi)
		{
			_iReposi = IReposi;
		}
		[HttpPost("Register")]
		
		public async Task<ActionResult> Register(UserDto user)
		{	
			var users = new User(user);
			var results =  await users.CheckUserExists(_iReposi);
			if(results.IsSuccess)
			{
				results.IsSuccess = false;
				return Ok(results);
			}
			var result = await users.AddUser(_iReposi);
			return Ok(result);
		}
		
		
	}
}