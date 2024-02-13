using System.Data;
using Microsoft.AspNetCore.Mvc;
using NewWebApi.Models;
using NewWebApi.Models.AuthModel;

namespace NewWebApi.Interface
{
	public interface IRepository
	{
		 public Task<Result> GetCards(string id);
		 public Task<Result> AddUser(User user);
		 public Task<Result> CheckUserExists(UserDto user);
		 
		 public  Task<Result> CheckResponseCount(User user);
		 
		 public Task<Result> DeductResponseCount(User user);
		 public Task<Result> GetHoroscopeSubscribes();
	}
}