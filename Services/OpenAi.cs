using System.Data;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using NewWebApi.Interface;

namespace NewWebApi.Services
{
	public class OpenAi : IOpenServices
	{
		private readonly ILogger<OpenAi> _logger;
		private readonly HttpClient _httpClient;

		public OpenAi(HttpClient client, ILogger<OpenAi> logger)
		{
			_httpClient = client;
			_logger = logger;
		}
		public OpenAi()
		{

		}

		public async Task<string> GetTaroAnswer(DataTable cards, string question)
		{
			var getNameOfPictures = cards.AsEnumerable().Select(s => s.Field<string>("card_name")).ToList();
			var createUnionString = string.Join(", ", getNameOfPictures);
			var ss = $@"{{
					""model"": ""gpt-3.5-turbo"",
					""messages"": [
						{{""role"": ""system"", ""content"": ""Ты профессиональный таролог, делаешь расклад следуя следующим инструкциям. Шаблон для ответа выглядит так : Card1:[анализ о том, как прошлые события повлияли на текущие?], Card2: [толкование того что происходит сейчас], Card3:[твой прогноз о том какой будет финал или развитие событий], ответы долэны быть естетственными и должны следовать форматированию и также делать заключение на основе трех выпавших карт.не использовать фразы типо - Учитывайте, что толкование карт таро может быть субъективным, и важно их рассматривать в контексте ситуации и интуиции.""}},
						{{""role"": ""system"", ""content"": ""Вопрос {question}""}},
						{{""role"": ""user"", ""content"": "" Выпадают карты {createUnionString}""}}
					],
					""temperature"": 0.7,
					""max_tokens"": 1000
				}}";
			_logger.LogInformation("\nCards: {0}", createUnionString);
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				Content = new StringContent(ss, Encoding.UTF8, "application/json")
			};
			
			var result = await GetResponse(request);
			if(result.Count()< 100)
			{
				return await GetResponse(request);
			}
			return result;
		}

		public async Task<string> GetResponse(HttpRequestMessage requestString)
		{
			try
			{
				using var response = await _httpClient.SendAsync(requestString);
				response.EnsureSuccessStatusCode();
				var resp = await response.Content.ReadAsStringAsync();
				var finalMessge = JsonObject.Parse(resp)?["choices"]?[0]?["message"]?["content"]?.ToString();
				_logger.LogInformation("\nResponse: {0}", finalMessge);
				return finalMessge ?? "";
			}
			catch (HttpRequestException ex)
			{
				_logger.LogError("\nException caught");
				_logger.LogError("Message: {0}", ex.Message);
				throw;
			}
		}

		public string BuildRequestString(params string[] cardDescriptions)
		{
			var stringBuilder = new System.Text.StringBuilder();
			stringBuilder.AppendLine("{");
			stringBuilder.AppendLine("  \"model\": \"gpt-3.5-turbo\",");
			stringBuilder.AppendLine("  \"messages\": [");

			foreach (var cardDescription in cardDescriptions)
			{
				stringBuilder.AppendLine($@"    {{""role"": ""system"", ""content"": ""{cardDescription}""}},");
			}

			// Убираем лишнюю запятую, если есть элементы
			if (cardDescriptions.Length > 0)
			{
				stringBuilder.Length -= 3; // учитываем 3 символа: новая строка + пробел + запятая
			}

			stringBuilder.AppendLine();
			stringBuilder.AppendLine("  ],");
			stringBuilder.AppendLine("  \"temperature\": 0.7");
			stringBuilder.Append("}");

			return stringBuilder.ToString();
		}
		



	}

}