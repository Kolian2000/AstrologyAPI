using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewWebApi.Models;

namespace NewWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HoroscopeSenderController : ControllerBase
	{
		private readonly IMemoryCache _memoryCache;

		public HoroscopeSenderController(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}
		[HttpPost("HoroscopeSender")]
		public async Task<IActionResult> HoroscopeSender(DateTime date)
		{
			if (_memoryCache.TryGetValue("Horoscopes", out Dictionary<DateTime, Dictionary<string, string>> horoscopes))
			{
				if (horoscopes.ContainsKey(date))
				{
					return Ok(horoscopes[date]);
				}
				else
				{
					return NotFound($"Horoscopes not found for the date: {date}");
				}
			}
			else
			{
				return NotFound("Horoscopes not found");
			}
		}
		// public Task<IActionResult> HoroscopeSubscribe(Horoscope horoscope)
		// {
			
		// }
		
		
		
		

	}
}