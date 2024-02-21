using System.Data;
using Microsoft.Extensions.Caching.Memory;
using NewWebApi.Interface;
using NewWebApi.Models;
using Quartz;

namespace NewWebApi.Services
{
	public class GetHoroscopeSubscribersJob : IJob
	{
		private readonly IMemoryCache memoryCache;
		private readonly IOpenServices openServices;
        private readonly IReposi reposi;
        private readonly ILogger<GetHoroscopeSubscribersJob> logger;

        public GetHoroscopeSubscribersJob(IMemoryCache memoryCache, IOpenServices openServices,
			IReposi reposi, ILogger<GetHoroscopeSubscribersJob> logger) 
		{
			this.memoryCache = memoryCache;
			this.openServices = openServices;
            this.reposi = reposi;
            this.logger = logger;
        }

		public async Task Execute(IJobExecutionContext context)
		{
			var GetHoroscopeSubscribers =await Horoscope.GetHoroscopeSubscribes(reposi);
			var result = new Dictionary<int,Dictionary<string, string>>();
			foreach (DataRow item in GetHoroscopeSubscribers.DataTableResult.Rows)
			{
				var horoscope = new Horoscope(item);
				var horoscopeResponse = await horoscope.GetHoroscopeAnswer(openServices);
				logger.LogInformation("Получен гороскопп {1}", horoscopeResponse);
				if(result.ContainsKey(horoscope.HoroscopeTime.Hour))
				{
					result[horoscope.HoroscopeTime.Hour].Add(horoscope.UserName, horoscopeResponse);
				}
				else
				{
					result.Add(horoscope.HoroscopeTime.Hour, new Dictionary<string, string> { { horoscope.UserName, horoscopeResponse } });
				}
			}
			memoryCache.Set("Horoscopes", result, TimeSpan.FromDays(1));
		}
	}
}