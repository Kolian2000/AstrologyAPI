using System.Data;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.OpenApi;
using NewWebApi.Interface;
using NewWebApi.Models;
using NewWebApi.Services.Attributes;

namespace NewWebApi.Controllers
{	[ApiController]
	[Route("api/[controller]")]
	public class AnswerController : ControllerBase
	{
		private readonly IOpenServices _openAi;
		private readonly IReposi _reposi;

		public AnswerController(IOpenServices openai, IReposi reposi)
		{
			_openAi = openai;
			_reposi = reposi;
		}
		
		[HttpPost]
		public async Task<IActionResult> Get(Desc desc)
		{
			var cards = await desc.GetCards(_reposi);
			if(!cards.IsSuccess)
			{
				return NotFound("No cards found");
			}
			var answerApi = await _openAi.GetTaroAnswer(cards.DataTableResult, desc.Question);
			if(answerApi == null)
			{
				return NotFound("No answer found");
			}
			

			return Ok(new ResponseType(cards.DataTableResult,answerApi));
			
		}
		
	}


}