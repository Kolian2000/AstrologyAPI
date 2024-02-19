namespace NewWebApi.Services
{
	public class ConvertToLocalTime
	{
		public DateTime TimeConverter(DateTime dateTime, string timeZone)
		{
			TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
			return TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo, TimeZoneInfo.Local);
		}
	}
}