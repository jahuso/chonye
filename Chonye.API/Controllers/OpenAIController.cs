using Azure;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chonye.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        OpenAIClient _client;

        public OpenAIController(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new OpenAIClient(new Uri(_configuration["OpenAI:Uri"]),
                new AzureKeyCredential(_configuration["OpenAI:ApiKey"])
                );
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Ask([FromQuery] string request)
        {

            //CompletionsOptions completionsOptions = new()
            //{
            //    DeploymentName = "deploy35turbo",
            //};

            ChatCompletionsOptions chatCompletionsOptions = new()
            {
                DeploymentName = _configuration["OpenAI:Model"],
                Messages =
                    {
                        new ChatMessage(ChatRole.System, @"You are an AI assistant that return information on a raw json document format without explanation"),
                        new ChatMessage(ChatRole.User, @"Top 5 richest countries by gdp"),
                        //new ChatMessage(ChatRole.User, @"Top 5 richest countries return the data on a dictionary that have a string and a long"),
                        new ChatMessage(ChatRole.Assistant, request),
                        
                    },
                Temperature = (float)0.2,
                MaxTokens = 350,
                NucleusSamplingFactor = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0,
            };

            Response<ChatCompletions> responseWithoutStream = await _client.GetChatCompletionsAsync(chatCompletionsOptions);

            ChatCompletions completions = responseWithoutStream.Value;
            var respuesta = completions.Choices[0].Message.Content;
            var conelyei =respuesta.ToJson();
            return Ok(completions.Choices[0].Message.Content);

        }

        //[HttpGet("generate")]
        //[NonAction]
        //public async Task<IActionResult> GenerateChart([FromQuery] ChartGenerationRequest request)
        //{
        //    try
        //    {
        //        //string prompt = $"Create a chart that {request.Prompt}";

        //        //var completion = await _client.GetChatCompletionsAsync (
        //        //    model: "davinci",
        //        //    prompt: prompt,
        //        //    maxTokens: 100,
        //        //    temperature: 0.7
        //        //);


        //        Response<ChatCompletions> responseWithoutStream = await _client.GetChatCompletionsAsync(_configuration["OpenAI:Model"],new ChatCompletionsOptions()
        //        {
        //            Messages =
        //            {

        //                //new ChatMessage(ChatRole.System, @"you are a service that reply just the message requested and nothing else:"),
        //                //new ChatMessage(ChatRole.User, @"Top 5 richest countries return the data on a dictionary that have a string and a long"),
        //                new ChatMessage(ChatRole.System, @"You are an AI assistant that helps people find information."),
        //                new ChatMessage(ChatRole.User, @"Top 5 richest countries by gdp, Only respond with code as JSON document without explanation"),
        //            },
        //            Temperature = (float)0.2,
        //            MaxTokens = 350,
        //            NucleusSamplingFactor = (float)0.95,
        //            FrequencyPenalty = 0,
        //            PresencePenalty = 0,
        //        });



        //        ChatCompletions completions = responseWithoutStream.Value;
        //        return Ok(completions.Choices[0].Message.Content);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Error = ex.Message });
        //    }
        //}
    }
}
