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
			var results =  await Models.AuthModel.User.CheckUserExists(_iReposi, user.Name);
			if(results.IsSuccess)
			{
				results.IsSuccess = false;
				return Ok(results);
			}
			var result = await users.AddUser(_iReposi);
			return Ok(result);
		}
		[HttpGet("CheckUser")]
		public async Task<ActionResult> CheckUser([FromBody]string name)
		{
			var result = await Models.AuthModel.User.CheckUserExists(_iReposi,name);
			return Ok(result);
		}
		
		
	}
}