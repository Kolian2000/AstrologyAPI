using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewWebApi.Interface;
using NewWebApi.Models;

namespace NewWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HoroscopeSenderController : ControllerBase
	{
		private readonly IMemoryCache _memoryCache;
		private readonly IReposi _repository;

		public HoroscopeSenderController(IMemoryCache memoryCache, IReposi repository)
		{
			_memoryCache = memoryCache;
			_repository = repository;
		}
		[HttpPost("HoroscopeSender")]
		public async Task<IActionResult> HoroscopeSender(DateTime date)
		{
			if (_memoryCache.TryGetValue("Horoscopes", out Dictionary<int, Dictionary<string, string>> horoscopes))
			{
				if (horoscopes.ContainsKey(date.Hour))
				{
					return Ok(horoscopes[date.Hour]);
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
		[HttpPost("HoroscopeSubscribe")]
		public async Task<IActionResult> HoroscopeSubscribe(Horoscope horoscope)
		{
			horoscope.TimeConverter();
			var horoscopeSub = await horoscope.SubscribeToHoroscope(_repository);
			return Ok(horoscopeSub);
		}
		
		
		
		

	}
}