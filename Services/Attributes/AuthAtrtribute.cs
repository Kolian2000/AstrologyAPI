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
			
			// Получить данные из тела HTTP-запроса
			var headers = context.HttpContext.Request.Headers;
			var repository = context.HttpContext.RequestServices.GetService(typeof(IReposi)) as IReposi;
			if(headers.TryGetValue("Id", out var headersValue))
			{
				await new User{ Name = headersValue}.DeductResponseCount(repository);
			}
		}

		public async void OnResourceExecuting(ResourceExecutingContext context)
		{
			var results = new Result();
			
			var headers = context.HttpContext.Request.Headers;
			var repository = context.HttpContext.RequestServices.GetService(typeof(IReposi)) as IReposi;
			if(headers.TryGetValue("Id", out var headersValue))
			{
				results = await new User{ Name = headersValue}.CheckResponseCount(repository);

			}
			if (!results.IsSuccess)
			{
				// Отказано в доступе
				context.Result = new BadRequestResult();
			}
			
		}
	}
}