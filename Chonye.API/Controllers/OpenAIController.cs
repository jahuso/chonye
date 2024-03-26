using Azure;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Mvc;

namespace Chonye.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly OpenAIClient _client;
        //private readonly SearchClient _searchClient;

        public OpenAIController(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new OpenAIClient(new Uri(_configuration["OpenAI:Uri"]),
                new AzureKeyCredential(_configuration["OpenAI:ApiKey"])
            );

            var searchServiceName = _configuration["SearchServiceName"];
            //var searchIndexName = _configuration["SearchIndexName"];
            //var searchApiKey = _configuration["SearchApiKey"];
            //var searchServiceUri = new Uri($"https://{searchServiceName}.search.windows.net");

            //_searchClient = new SearchClient(searchServiceUri, searchIndexName, new AzureKeyCredential(searchApiKey));
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetOpenAIResponseAsync([FromQuery] string request)
        {
            try
            {
                // Azure OpenAI setup
                var apiBase = _configuration["OpenAI:Uri"];
                var apiKey = _configuration["OpenAI:ApiKey"];
                var deploymentId = _configuration["OpenAI:Model"];

                // Azure AI Search setup
                var searchEndpoint = _configuration["OpenAI:SearchUri"];
                var searchKey = _configuration["OpenAI:SearchKey"];
                var searchIndexName = _configuration["OpenAI:SearchIndexName"];
                
                var chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    Messages =
                    {
                        new ChatRequestSystemMessage("Soy ChonchAI, tu asistente comercial como te puedo ayudar hoy?"),
                        new ChatRequestUserMessage("Que incluye el Smart Center Basico"),
                        new ChatRequestAssistantMessage("4 agentes, 4 puertos y un supervisor"),
                        new ChatRequestUserMessage("Que costo tiene"),
                        new ChatRequestAssistantMessage("Tiene un costo de USD 242"),
                        new ChatRequestUserMessage(request),
    
                    },
                    AzureExtensionsOptions = new AzureChatExtensionsOptions()
                    {
                        Extensions =
                        {
                            new AzureCognitiveSearchChatExtensionConfiguration()
                            {
                                SearchEndpoint = new Uri(searchEndpoint),
                                IndexName = searchIndexName,
                                Key = searchKey,
                                //QueryType = "AzureCognitiveSearch",
                                //Parameters = FromString([object Object])
                            },
                        },
                    },
                    DeploymentName = deploymentId,
                    MaxTokens = 800,
                    Temperature = 0,
                };

                var response = await _client.GetChatCompletionsAsync(chatCompletionsOptions);

                var message = response.Value.Choices[0].Message;

                var contextMessages = message.AzureExtensionsContext;

                return Ok(message.Content);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }

}