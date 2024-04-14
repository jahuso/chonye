using Azure;
using Azure.AI.OpenAI;
using Azure.Search.Documents;
using Chonye.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chonye.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InviteesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly OpenAIClient _client;
        private readonly SearchClient _searchClient;

        public InviteesController(IConfiguration configuration)
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


        [HttpGet]
        [ActionName("SuggestbySector")]
        public async Task<ActionResult<String>> GetIndustry([FromQuery] string industry)
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
                        new ChatRequestSystemMessage("Soy CT concierge, asistente de inteligencia Artificial, si la pregunta del usuario no puede ser respondida usando los documentos recuperados, respondela usando tu propio conocimiento"),

                        //new ChatRequestSystemMessage("Soy ChonchAI, asistente de inteligencia Artificial, si la pregunta del usuario no puede ser respondida usando los documentos recuperados, respondela usando tu propio conocimiento."),
                        //new ChatRequestUserMessage( $"As a business in the {industrySector} sector, how can AI improve my operations and outcomes?\""),
                        //new ChatRequestAssistantMessage("4 agentes, 4 puertos y un supervisor"),
                        ////new ChatRequestUserMessage("Que costo tiene"),
                        //new ChatRequestAssistantMessage("Tiene un costo de USD 242"),
                        new ChatRequestUserMessage($"puedes darme ideas sobre como puede la inteligencia artificial ayudar a mi negocio del sector de {industry}?\""),                        
                        //new ChatRequestUserMessage($"Como una empresa del sector {industrySector}, como puede la inteligencia artificial mejorar mis procesos y resultados?\""),
                        //new ChatRequestUserMessage($"As a business in the {industrySector} sector, how can AI improve my operations and outcomes?\""),

                    },

                    DeploymentName = deploymentId,
                    MaxTokens = 800,
                    Temperature = 0,
                };

                var response = await _client.GetChatCompletionsAsync(chatCompletionsOptions);

                var message = response.Value.Choices[0].Message;


                //var message = $"Hola soy el asistente ChonchaAI" + invite + response.Value.Choices[0].Message;

                var contextMessages = message.AzureExtensionsContext;


                return message.Content;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet]
        [ActionName("GetAdvise")]
        public async Task<IActionResult> GetAdvise([FromQuery] Invitee pInvitee)
        {

            try
            {
                var invitee = new Invitee() { 
                    Name = pInvitee.Name,
                    Company = pInvitee.Company,
                    Industry  = pInvitee.Industry,
                };
                string greeting = "Bienvenido soy el concierge de CT Telecomunicaciones, ";

                var aiResponse = await GetIndustry(pInvitee.Industry);

                string farewell = " Espero haberte sido de utilidad";
                
                var finalResponse = greeting + aiResponse.Value + farewell; 

                return Ok(finalResponse);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet]
        [ActionName("SuggestbyName")]
        [Produces("application/json")]
        public async Task<IActionResult> SuggestbyName([FromQuery] string request)
        {
            var invitee1 = new Invitee
            {
                Name = "Alejandro",
                Company = "CT Telecomunicaciones",
                Industry = "Telecomunicaciones"
            };

            var invitee2 = new Invitee
            {
                Name = "Jane Smith",
                Company = "XYZ Corp.",
                Industry = "Finanzas"
            };

            string inviteeName = "";
            string industrySector = "";

            if (request.Equals(invitee1.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                industrySector = invitee1.Industry;
                inviteeName = invitee1.Name;
            }
            else
            {
                industrySector = invitee2.Industry;
                inviteeName = invitee2.Name;
            }

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
                        new ChatRequestSystemMessage("Soy CT concierge, asistente de inteligencia Artificial, si la pregunta del usuario no puede ser respondida usando los documentos recuperados, respondela usando tu propio conocimiento"),

                        //new ChatRequestSystemMessage("Soy ChonchAI, asistente de inteligencia Artificial, si la pregunta del usuario no puede ser respondida usando los documentos recuperados, respondela usando tu propio conocimiento."),
                        //new ChatRequestUserMessage( $"As a business in the {industrySector} sector, how can AI improve my operations and outcomes?\""),
                        //new ChatRequestAssistantMessage("4 agentes, 4 puertos y un supervisor"),
                        ////new ChatRequestUserMessage("Que costo tiene"),
                        //new ChatRequestAssistantMessage("Tiene un costo de USD 242"),
                        new ChatRequestUserMessage("Como una empresa del sector finanzas, como puede la inteligencia artificial mejorar mis procesos y resultados?"),
                        //new ChatRequestUserMessage($"As a business in the {industrySector} sector, how can AI improve my operations and outcomes?\""),

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
