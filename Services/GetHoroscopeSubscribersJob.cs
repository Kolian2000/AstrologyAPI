using System.Data;
using Microsoft.Extensions.Caching.Memory;
using NewWebApi.Interface;
using Quartz;

namespace NewWebApi.Services
{
	public class GetHoroscopeSubscribersJob : IJob
	{
		private readonly IMemoryCache memoryCache;
		private readonly IRepository repository;
		private readonly IOpenServices openServices;

		public GetHoroscopeSubscribersJob(IMemoryCache memoryCache, IRepository repository, IOpenServices openServices) 
		{
			this.memoryCache = memoryCache;
			this.repository = repository;
			this.openServices = openServices;
		}

		public async Task Execute(IJobExecutionContext context)
		{
			var GetHoroscopeSubscribers = await repository.GetHoroscopeSubscribes();
			var result = new Dictionary<DateTime,Dictionary<string, string>>();
			foreach (DataRow item in GetHoroscopeSubscribers.DataTableResult.Rows)
			{
				var id = Convert.ToString(item["username"]);
				string horoscopeName = item["horoscope_name"].ToString();
				var horoscopeTime = Convert.ToDateTime(item["horoscope_time"]);
				
				var horoscopeResponse = await openServices.GetHoroscopeAnswer(horoscopeName);
				if(result.ContainsKey(horoscopeTime))
				{
					result[horoscopeTime].Add(id, horoscopeResponse);
				}
				else
				{
					result.Add(horoscopeTime, new Dictionary<string, string> { { id, horoscopeResponse } });
				}
			}
			memoryCache.Set("Horoscopes", result, TimeSpan.FromDays(1));
		}
	}
}