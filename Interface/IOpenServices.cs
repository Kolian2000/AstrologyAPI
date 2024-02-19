using System.Data;

namespace NewWebApi.Interface
{
	public interface IOpenServices
	{
		 public Task<string> GetTaroAnswer(DataTable cards);
		 public string BuildRequestString(params string[] messages);
		 public Task<string> GetResponse(string requestString);
	}
}