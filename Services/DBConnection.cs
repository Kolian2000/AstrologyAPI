using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewWebApi.Interface;
using NewWebApi.Models;
using NewWebApi.Models.Enum;
using Npgsql;

namespace NewWebApi.Services
{
	public class DBConnection : IReposi
	{
		private readonly IConfiguration configuration;
		private readonly ILogger logger;

		public DBConnection(IConfiguration configuration, ILogger<DBConnection> logger)
		{
			this.configuration = configuration;
			this.logger = logger;
		}
		public Task<Result> Request(NpgsqlCommand comand, TypeOfComand comandType)
		{
			var result = new Result();

			logger.LogInformation("Request {0}", comand.CommandText);

			var sqlDataSource = configuration.GetConnectionString("DefaultConnection");
			
			try
			{
				using (var myCommand = new NpgsqlConnection(sqlDataSource))
				{
					comand.Connection = myCommand;
					myCommand.Open();

					switch(comandType)
					{
						case TypeOfComand.Get:
							using (var read = comand.ExecuteReader())
							{	
								result.DataTableResult = new DataTable();
								result.DataTableResult.Load(read);
								
								result.IsSuccess = result.DataTableResult.Rows.Count > 0;
							}
							break;
						case TypeOfComand.Insert:
							var rowsAffected = comand.ExecuteNonQuery();
							result.IsSuccess = rowsAffected > 0;
							break;
						case TypeOfComand.Check:
							using (var read = comand.ExecuteReader())
							{
								result.IsSuccess = read.Read();
							}
							break;	
						case TypeOfComand.Count:
							var reads = (Int32)comand.ExecuteScalar();
							result.IsSuccess = reads > 0;
                               
							break;

													
					}
				}

				
	   			return Task.FromResult(result);
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, "Error in request {0}", comand.CommandText);
				throw;
			}
		}

	}
}