using System.Data;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewWebApi.Interface;
using NewWebApi.Models;
using NewWebApi.Models.AuthModel;
using NewWebApi.Models.Enum;
using Npgsql;

namespace NewWebApi.Services
{
	public class Repository : DataBaseConnection, IRepository
	{
		private readonly ILogger<Repository> _logger;

		public Repository(IConfiguration configuration, ILogger<Repository> logger) : base(configuration, logger)
		{
			_logger = logger;
		}
		public async Task<Result> GetCards(string descName)
		{
			var result = new Result();
			try
			{
				using (var comand = new NpgsqlCommand("SELECT * FROM card WHERE fk_desc_name = @descName ORDER BY RANDOM() LIMIT 3;"))
				{
					_logger.LogInformation("SELECT * FROM card WHERE fk_desc_name = {1} ORDER BY RANDOM() LIMIT 3;", descName);
					comand.Parameters.AddWithValue("@descName", descName);
					result = await Request(comand,TypeOfComand.Get);
					if(!result.IsSuccess)
					{
						return new Result
						{
							IsSuccess = false,
							ErrorMessage = "Failed to get cards."
							// Другие поля, которые вы хотите добавить к объекту Result
						};
					}
					_logger.LogError("Error in request {0}", result.ErrorMessage);

					
				}	
			}
			catch (Exception ex)
			{
				_logger.LogError("Error in request {0}",ex.Message );
				throw;
			}
			return result;
		}
		
		public async Task<Result> AddUser(User user)
		{
			var result = new Result();
			var sqlQuery = @"
				INSERT INTO ""users"" (username, password_hash)
				SELECT @username, @password_hash
				WHERE NOT EXISTS (SELECT 1 FROM ""users"" WHERE username = @username);";

			try
			{	
				using (var comand = new NpgsqlCommand(sqlQuery))
				{
					_logger.LogInformation("INSERT INTO \"users\" (username, password_hash) VALUES ({1}, {2})",user.Name, user.PasswordHash); 
					comand.Parameters.AddWithValue("@username", user.Name);
					comand.Parameters.AddWithValue("@password_hash", user.PasswordHash);
					result = await Request(comand, TypeOfComand.Insert);
				}
				if (!result.IsSuccess)
				{
					return new Result
					{
						IsSuccess = false,
						ErrorMessage = "Failed to add user to the database."
						// Другие поля, которые вы хотите добавить к объекту Result
					};
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("Error in request {0}",ex.Message);
				throw;
			}
			return result;
		}
		
		public async Task<Result> CheckUserExists(UserDto user)
		{
			var result = new Result();
			
			try
			{
				using (var command = new NpgsqlCommand("SELECT 1 FROM \"users\" WHERE username = @username;"))
				{
					_logger.LogInformation("Request {0}", command.CommandText);
					command.Parameters.AddWithValue("@username", user.Name);
					result= await Request(command, TypeOfComand.Check);
				}
				if (result.IsSuccess)
				{
					result.ErrorMessage = "User with the provided username already exists.";
					return result;
				}
				return new Result
				{
					IsSuccess = false,
					ErrorMessage = "User with the provided username does not exist."
				};		
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				throw;
			}	
		}
		public async Task<Result> CheckResponseCount(User user)
		{
			using (var comand = new NpgsqlCommand("SELECT response_count FROM users WHERE username = @username;"))
			{
				comand.Parameters.AddWithValue("@username", user.Name);
				return await Request(comand, Models.Enum.TypeOfComand.Count);
				//resilt.IsSuccess = Convert.ToInt32(resilt.DataTableResult.Rows[0]) > 0;
				//return resilt;
			}
		}
		public Task<Result> DeductResponseCount(User user)
		{
			using (var comand = new NpgsqlCommand("UPDATE users SET response_count = response_count - 1 WHERE username = @username;"))
			{
				comand.Parameters.AddWithValue("@username", user.Name);
				return Request(comand, Models.Enum.TypeOfComand.Insert);
			}
		}
		public Task<Result> GetHoroscopeSubscribes()
		{
			using (var comand = new NpgsqlCommand("SELECT u.username,s.horoscope_name,s.horoscope_time FROM horoscopes s join users u on s.user_id = u.user_id WHERE can_get_horoscope = true;"))
			{
				return Request(comand, Models.Enum.TypeOfComand.Get);
			}
		}
		public Task<Result> SubscribeToHoroscope(Horoscope horoscope)
		{
			using (var comand = new NpgsqlCommand("INSERT INTO horoscopes (user_id, horoscope_name, horoscope_time, can_get_horoscope) VALUES (@user_id, @horoscope_name, @horoscope_time, true);"))
			{
				comand.Parameters.AddWithValue("@user_id", horoscope.UserId);
				comand.Parameters.AddWithValue("@horoscope_name", horoscope.HoroscopeName);
				comand.Parameters.AddWithValue("@horoscope_time", horoscope.HoroscopeTime);
				return Request(comand, Models.Enum.TypeOfComand.Insert);
			}
		}
		
		
		
	}
}