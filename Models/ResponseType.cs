using System.Data;

namespace NewWebApi.Models
{
	public class ResponseType
	{
		public ResponseType(DataTable dataTable, string apiAnswer)
		{
            DataTable = dataTable;
            ApiAnswer = apiAnswer;
        }
		public DataTable DataTable { get; set; }
		public string ApiAnswer { get; set; }
	}
}