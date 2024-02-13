using System.Data;

namespace NewWebApi.Interface
{
	public interface IOpenServices
	{
		 public Task<string> GetTaroAnswer(DataTable cards);
		 public Task<string> GetHoroscopeAnswer(string horoscope);
	}
}