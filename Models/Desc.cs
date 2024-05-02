using NewWebApi.Interface;
using NewWebApi.Models.Enum;
using Npgsql;

namespace NewWebApi.Models
{
	public class Desc
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public string? KindDesc { get; set; }
		public string Question { get; set; }
		
		
		public async Task<Result> GetCards(IReposi reposi)
		{
			var result = new Result();
			try
			{
				using (var comand = new NpgsqlCommand("SELECT * FROM card WHERE fk_desc_name = @descName ORDER BY RANDOM() LIMIT 3;"))
				{
					comand.Parameters.AddWithValue("@descName", Name);
					result = await reposi.Request(comand,TypeOfComand.Get);
					if(!result.IsSuccess)
					{
						result.ErrorMessage = "Failed to get cards.";
						return result;
					}	
				}	
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex.Message);	
			}
			return result;
		}
		
		
	}
}