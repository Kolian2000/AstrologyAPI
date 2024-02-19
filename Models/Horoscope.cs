using System.Data;
using NewWebApi.Interface;
using NewWebApi.Models.Enum;
using Npgsql;

namespace NewWebApi.Models
{
	public class Horoscope
	{
		public int HoroscopeId { get; set; }
		public bool CanGetHoroscope { get; set; }
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
			UserName = item["user_name"].ToString(); // Предположим, что user_name - это int
			ZodiacSign = item["zodiac_sign"].ToString(); // Предположим, что zodiac_sign - это строка
			TimeZone = item["time_zone"].ToString(); // Предположим, что time_zone - это строка
		}

		public static Task<Result> GetHoroscopeSubscribes(IReposi reposi)
		{
			using (var comand = new NpgsqlCommand("SELECT u.username,s.horoscope_name,s.horoscope_time FROM horoscopes s join users u on s.user_id = u.user_id WHERE can_get_horoscope = true;"))
			{
				return reposi.Request(comand, TypeOfComand.Get);
			}
		}
		public Task<Result> SubscribeToHoroscope(IReposi reposi)
		{
			using (var comand = new NpgsqlCommand("INSERT INTO horoscopes (username, zodiac_sign, horoscope_time, can_get_horoscope, time_zone ) VALUES (@username, @zodiac_sign, @horoscope_time, true,@time_zone );"))
			{
				comand.Parameters.AddWithValue("@username", UserName);
				comand.Parameters.AddWithValue("@zodiac_sign", ZodiacSign);
				comand.Parameters.AddWithValue("@horoscope_time", HoroscopeTime);
				comand.Parameters.AddWithValue("@time_zone", TimeZone);
				return reposi.Request(comand, TypeOfComand.Insert);
			}
		}
		public async Task<string> GetHoroscopeAnswer(IOpenServices openServices)
		{
			var requestString = openServices.BuildRequestString("ты профессиональный астролог, знаешь принципы планет и домов, умеешь самокритически мыслить", $"Сделай гороскоп на сегодня для {ZodiacSign}");
			return await openServices.GetResponse(requestString);
		}
		public void TimeConverter()
		{
			// HoroscopeTime = DateTime.SpecifyKind(HoroscopeTime, DateTimeKind.Utc);
			TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
			
			
			HoroscopeTime = TimeZoneInfo.ConvertTime(HoroscopeTime, timeZoneInfo, TimeZoneInfo.Local);
		}
	}
}