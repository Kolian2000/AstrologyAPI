namespace NewWebApi.Models
{
	public class Horoscope
	{
		public int HoroscopeId { get; set; }
		public bool CanGetHoroscope { get; set; }
		public DateTime HoroscopeTime { get; set; }
		public int UserId { get; set; }
		public string HoroscopeName { get; set; }
		public string TimeZone { get; set; }
		
	}
}