using System.ComponentModel.DataAnnotations;
using NewWebApi.Interface;
using NewWebApi.Models.Enum;
using Npgsql;

namespace NewWebApi.Models.AuthModel
{
	public class User
	{
		public int? Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string PasswordHash { get; set; }
		
		public string? Email { get; set; }
		
		public string ResponseCount { get; set; }
		
		public TimeSpan CreatedAt { get; set; }
		
		public User(UserDto userDto)
		{
			Name = userDto.Name;
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
		}
		public User() { }
		public async Task<Result> AddUser(IReposi reposi)
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
					comand.Parameters.AddWithValue("@username", Name);
					comand.Parameters.AddWithValue("@password_hash", PasswordHash);
					result = await reposi.Request(comand, TypeOfComand.Insert);
				}
				if (!result.IsSuccess)
				{
					return new Result
					{
						IsSuccess = false,
						ErrorMessage = "User with the provided username already exists."
						// Другие поля, которые вы хотите добавить к объекту Result
					};
				}
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex.Message);
				throw;
			}
			return result;
		}
		public async Task<Result> CheckUserExists(IReposi reposi)
		{
			var result = new Result();
			
			try
			{
				using (var command = new NpgsqlCommand("SELECT 1 FROM \"users\" WHERE username = @username;"))
				{
					command.Parameters.AddWithValue("@username",Name);
					result= await reposi.Request(command, TypeOfComand.Check);
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
				System.Console.WriteLine(ex.Message);
				throw;
			}	
		}

		

		public async Task<Result> CheckResponseCount(IReposi reposi)
		{
			using (var comand = new NpgsqlCommand("SELECT response_count FROM users WHERE username = @username;"))
			{
				comand.Parameters.AddWithValue("@username", Name);
				return await reposi.Request(comand, Models.Enum.TypeOfComand.Count);
			}
		}
		public Task<Result> DeductResponseCount(IReposi reposi)
		{
			using (var comand = new NpgsqlCommand("UPDATE users SET response_count = response_count - 1 WHERE username = @username;"))
			{
				comand.Parameters.AddWithValue("@username", Name);
				return reposi.Request(comand, Models.Enum.TypeOfComand.Insert);
			}
		}
	}
}