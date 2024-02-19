using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NewWebApi.Interface;
using NewWebApi.Models;
using NewWebApi.Models.AuthModel;

namespace NewWebApi.Services.Attributes
{
	public class AuthAttrtribute : Attribute, IResourceFilter
	{
		
		public async void OnResourceExecuted(ResourceExecutedContext context)
		{ 
			var results = new Result();
			// Получить данные из тела HTTP-запроса
			var headers = context.HttpContext.Request.Headers;
			var repository = context.HttpContext.RequestServices.GetService(typeof(IRepository)) as IRepository;
			if(headers.TryGetValue("Id", out var headersValue))
			{
				await repository.DeductResponseCount(new User{ Name = headersValue});

			}
		}

		public async void OnResourceExecuting(ResourceExecutingContext context)
		{
			var results = new Result { IsSuccess = false};
			// Получить данные из тела HTTP-запроса
			var headers = context.HttpContext.Request.Headers;
			var repository = context.HttpContext.RequestServices.GetService(typeof(IRepository)) as IRepository;
			if(headers.TryGetValue("Id", out var headersValue))
			{
				results = await repository.CheckResponseCount(new User{ Name = headersValue});

			}
			if (!results.IsSuccess)
			{
				// Пользователь уже зарегистрирован, выполните необходимые действия
				context.Result = new BadRequestResult();
			}
			
		}
	}
}