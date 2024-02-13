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

    public async Task<string> GetTaroAnswer(DataTable cards)
    {
        var getNameOfPictures = cards.AsEnumerable().Select(s => s.Field<string>("card_name")).ToList();
        var createUnionString = string.Join(", ", getNameOfPictures);
        var requestString = BuildRequestString($"Ты профессиональный таролог, делаешь расклад где первая карта - Что из прошлого повлияло на события сегодняшние?,вторая карта что происходит сейчас и третья карта какой будет финал, не добавляй никаких спец символов в ответ,каждая карта будет начинаться со СТРОКИ Card1, Card2, Card3, не использовать фразы типо - Учитывайте, что толкование карт таро может быть субъективным, и важно их рассматривать в контексте ситуации и интуиции.", $"Выпадают карты {createUnionString}");
        return await GetResponse(requestString);
    }

    public async Task<string> GetHoroscopeAnswer(string horoscope)
    {
        var requestString = BuildRequestString("ты профессиональный астролог, знаешь принципы планет и домов, умеешь самокритически мыслить", $"Сделай гороскоп на сегодня для {horoscope}");
        return await GetResponse(requestString);
    }

    private async Task<string> GetResponse(string requestString)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            Content = new StringContent(requestString, Encoding.UTF8, "application/json")
        };

        try
        {
            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            var finalMessge = JsonObject.Parse(resp)?["choices"]?[0]?["message"]?["content"]?.ToString();
            return finalMessge ?? "";
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("\nException caught");
            _logger.LogError("Message: {0}", ex.Message);
            throw;
        }
    }

    private string BuildRequestString(params string[] messages)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("{");
        stringBuilder.AppendLine("  \"model\": \"gpt-3.5-turbo\",");
        stringBuilder.AppendLine("  \"messages\": [");

        foreach (var message in messages)
        {
            stringBuilder.AppendLine($"    {{\"role\": \"system\", \"content\": \"{message}\"}},");
        }

        stringBuilder.AppendLine("    {\"role\": \"user\", \"content\": \"\"},");
        stringBuilder.AppendLine("    {\"role\": \"assistant\", \"content\": \"\"}");
        stringBuilder.AppendLine("  ]");
        stringBuilder.AppendLine("}");

        return stringBuilder.ToString();
    }
}

}