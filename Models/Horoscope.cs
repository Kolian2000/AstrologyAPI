using System.Data;
using System.Text;
using NewWebApi.Interface;
using NewWebApi.Models.Enum;
using Npgsql;

namespace NewWebApi.Models
{
	public class Horoscope
	{
		public int? HoroscopeId { get; set; }
		public bool? CanGetHoroscope { get; set; }
		public DateTime HoroscopeTime { get; set; }
		public string UserName { get; set; }
		public string ZodiacSign { get; set; }
		public string TimeZone { get; set; }

		public Horoscope() { }

		public Horoscope(DataRow item)
		{
			HoroscopeId = Convert.ToInt32(item["horoscope_id"]);
			CanGetHoroscope = Convert.ToBoolean(item["can_get_horoscope"]);
			HoroscopeTime = Convert.ToDateTime(item["horoscope_time"]);
			UserName = item["username"].ToString(); 
			ZodiacSign = item["zodiac_sign"].ToString(); 
			TimeZone = item["time_zone"].ToString();
		}

		public static Task<Result> GetHoroscopeSubscribes(IReposi reposi)
		{
			using (var comand = new NpgsqlCommand("SELECT * FROM horoscopes WHERE can_get_horoscope = true;"))
			{
				return reposi.Request(comand, TypeOfComand.Get);
			}
		}
		public async Task<Result> SubscribeToHoroscope(IReposi reposi)
		{
			using (var comand = new NpgsqlCommand("INSERT INTO horoscopes (username, zodiac_sign, horoscope_time, can_get_horoscope, time_zone ) VALUES (@username, @zodiac_sign, @horoscope_time, true,@time_zone );"))
			{
				comand.Parameters.AddWithValue("@username", UserName);
				comand.Parameters.AddWithValue("@zodiac_sign", ZodiacSign);
				comand.Parameters.AddWithValue("@horoscope_time", HoroscopeTime);
				comand.Parameters.AddWithValue("@time_zone", TimeZone);
				var result = await reposi.Request(comand, TypeOfComand.Insert);
				if (!result.IsSuccess)
				{
					result.ErrorMessage = "Failed to subscribe.";
				}
				return result;
			}
		}
		public async Task<string> GetHoroscopeAnswer(IOpenServices openServices)
		{

			var ss = $@"{{
					""model"": ""gpt-3.5-turbo"",
					""messages"": [
						{{""role"": ""system"", ""content"": ""Ты профессиональный астролог делвешь гороскопы. Не используещь оценочные суждения""}},
						{{""role"": ""system"", ""content"": ""Сделай гороскоп для {ZodiacSign}""}}
					],
					""temperature"": 0.7
				}}";
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				Content = new StringContent(ss, Encoding.UTF8, "application/json")
			};
			return await openServices.GetResponse(request);
		}
		public void TimeConverter()
		{
			TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
			HoroscopeTime = TimeZoneInfo.ConvertTime(HoroscopeTime, timeZoneInfo, TimeZoneInfo.Local);
		}
	}
}