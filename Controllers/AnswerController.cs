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
		private readonly IRepository _repository;

		public AnswerController(IOpenServices openai, IRepository repository)
		{
			_openAi = openai;
			_repository = repository;
		}
		public IEnumerable<Card> Cards { get; set; }
		[AuthAttrtribute]
		[HttpGet]
		public async Task<IActionResult> Get(Desc desc)
		{
			var cards = await _repository.GetCards(desc.Name);
			if(cards.DataTableResult == null)
			{
				return NotFound("No cards found");
			}
			var answerApi = await _openAi.GetTaroAnswer(cards.DataTableResult);
			if(answerApi == null)
			{
				return NotFound("No answer found");
			}

			return Ok(new ResponseType(cards.DataTableResult,answerApi));
			
		}
		
	}


}