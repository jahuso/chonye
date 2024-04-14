using Azure;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Mvc;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace Chonye.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly OpenAIClient _client;
        private readonly SearchClient _searchClient;

        public OpenAIController(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new OpenAIClient(new Uri(_configuration["OpenAI:Uri"]),
                new AzureKeyCredential(_configuration["OpenAI:ApiKey"])
            );

            //var searchServiceName = _configuration["SearchServiceName"];
            //var searchIndexName = _configuration["SearchIndexName"];
            //var searchApiKey = _configuration["SearchApiKey"];
            //var searchServiceUri = new Uri($"https://{searchServiceName}.search.windows.net");

            var searchServiceUri = new Uri(_configuration["OpenAI:SearchUri"]);
            var searchIndexName = _configuration["OpenAI:SearchIndexName"];
            var searchApiKey = _configuration["OpenAI:SearchKey"];
            _searchClient = new SearchClient(searchServiceUri, searchIndexName, new AzureKeyCredential(searchApiKey));
        }


        //[HttpGet]
        //[Produces("application/json")]
        //public async Task<IActionResult> AskRefactored([FromQuery] string request)
        //{
        //    try
        //    {
        //        // Azure OpenAI setup
        //        var apiBase = _configuration["OpenAI:Uri"];
        //        var apiKey = _configuration["OpenAI:ApiKey"];
        //        var deploymentId = _configuration["OpenAI:Model"];

        //        // Azure AI Search setup
        //        var searchEndpoint = _configuration["OpenAI:SearchUri"];
        //        var searchKey = _configuration["OpenAI:SearchKey"];
        //        var searchIndexName = _configuration["OpenAI:SearchIndexName"];


        //        // Search in Azure Cognitive Search first
        //        var azureSearchResult = await SearchAzureCognitiveSearch(request);

        //        if (!string.IsNullOrEmpty(azureSearchResult))
        //        {
        //            return Ok(azureSearchResult);
        //        }

        //        // If no answer found in Azure Cognitive Search, query Azure OpenAI
        //        var chatCompletionsOptions = new ChatCompletionsOptions()
        //        {
        //            Messages =
        //            {
        //                new ChatRequestSystemMessage("Soy ChonchAI, asistente de inteligencia Artificial, si la pregunta del usuario no puede ser respondida usando los documentos recuperados, respondela usando tu propio conocimiento."),
        //                new ChatRequestUserMessage(request)
        //            },
        //            DeploymentName = _configuration["OpenAI:Model"],
        //            MaxTokens = 800,
        //            Temperature = 0,
        //        };

        //        var response = await _client.GetChatCompletionsAsync(chatCompletionsOptions);
        //        var message = response.Value.Choices[0].Message;

        //        return Ok(message.Content);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        //private async Task<string> SearchAzureCognitiveSearch(string question)
        //{
        //    // Azure OpenAI setup

        //    var searchOptions = new SearchOptions
        //    {
        //        SearchMode = SearchMode.All
        //    };

        //    var response = await _searchClient.SearchAsync<SearchDocument>(question, searchOptions);

        //    if (response.Value.GetResults().Any())
        //    {
        //        return response.Value.GetResults().First().Document["answer"] as string;
        //    }

        //    return null;
        //}

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Ask([FromQuery] string request)
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
                        new ChatRequestSystemMessage("Soy ChonchAI, asistente de inteligencia Artificial, si la pregunta del usuario no puede ser respondida usando los documentos recuperados, respondela usando tu propio conocimiento."),
                        new ChatRequestUserMessage("Que incluye el Smart Center Basico"),
                        new ChatRequestAssistantMessage("4 agentes, 4 puertos y un supervisor"),
                        new ChatRequestUserMessage("Quien es Bill Gates?"),
                        new ChatRequestAssistantMessage("Bill Gates es un empresario y filántropo estadounidense, cofundador de Microsoft y conocido por su liderazgo en la revolución de la informática personal. Junto con su esposa, Melinda, fundó la Fundación Bill y Melinda Gates, una organización filantrópica líder en el mundo."),
                        
                        //new ChatRequestUserMessage("Que costo tiene"),
                        //new ChatRequestAssistantMessage("Tiene un costo de USD 242"),
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