using Chonye.Core.DTOs;
using Chonye.Domain.Helpers;
using Chonye.Domain.Models;
using Chonye.Domain.Request;
using Chonye.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chonye.API.Controllers
{


    [Route("[controller]")]
    [ApiController]
    public class TenantController : Controller
    {
        private readonly DatabaseConfig _config;
        private readonly ILogger<TenantController> _logger;
        public TenantController(IOptions<DatabaseConfig> config,ILogger<TenantController> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        // GET: api/<TenantController>
        [ProducesResponseType(type: typeof(ActionResult<IListTenantResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IListTenantResponse>>> GetList([FromServices] IMediator mediator, [FromQuery] TenantParameters request)
        {
            var result = await mediator.Send(new ListTenantRequest
            {
                TenantId = request.TenantId
            });

            switch (result)
            {
                case IListTenantResponse.Succeeded sucess:
                    return Ok(sucess);
                case IListTenantResponse.NotFound:
                    return NotFound();
                case IListTenantResponse.InternalError internalError:
                    return BadRequest(internalError);
                default:
                    return BadRequest("Internal Server Error");
            }
        }

        // GET api/<TenantController>/5
        [ProducesResponseType(type: typeof(ActionResult<IGetTenantResponse>), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IGetTenantResponse>> Get([FromServices] IMediator mediator, [FromQuery] GetTenantRequest request)
        {
            var result = await mediator.Send(new GetTenantRequest(request.TenantId));

            return Ok(result);
        }


        // POST api/<TenantController>
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        //[HttpPost]
        //public async Task<ActionResult<HttpResponseMessage>> Post([FromServices] IMediator mediator, [FromBody] CreateTenantRequest request)
        //{
        //    var result = await mediator.Send(new CreateTenantRequest(request.Name, request.Email, request.Address, request.Phone));

        //    switch (result)
        //    {
        //        case ICreateTenantResponse.Succeeded successResult:
        //            return Ok(successResult.GlobalId);
        //        case ICreateTenantResponse.InvalidData invalidDataResponse:
        //            return BadRequest(invalidDataResponse.Error);
        //        case ICreateTenantResponse.InternalError internalError:
        //            return BadRequest(internalError.Message);
        //        default:
        //            return BadRequest("Internal Server Error");
        //    }
        //}

        //// PUT api/<TenantController>/5
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        //[HttpPut("{id}")]
        //public async Task<ActionResult<HttpResponseMessage>> Put([FromServices] IMediator mediator, [FromBody] PatchTenantRequest request)
        //{
        //    var result = await mediator.Send(new PatchTenantRequest(
        //        request.TenantId, request.Name, request.Email, request.Address, request.Phone));

        //    switch (result)
        //    {
        //        case IPatchTenantResponse.Succeeded successResult:
        //            return Ok(successResult.GlobalId);
        //        case IPatchTenantResponse.InvalidData invalidDataResponse:
        //            return BadRequest(invalidDataResponse.Error);
        //        case IPatchTenantResponse.InternalError internalError:
        //            return BadRequest(internalError.Message);
        //        default:
        //            return BadRequest("Internal Server Error");
        //    }
        //}

        //// DELETE api/<TenantController>/5
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<HttpResponseMessage>> Delete([FromServices] IMediator mediator, [FromBody] DeleteTenantRequest request)
        //{
        //    var result = await mediator.Send(new DeleteTenantRequest(request.Id));

        //    switch (result)
        //    {
        //        case IDeleteTenantResponse.Succeeded successResult:
        //            return Ok(successResult.GlobalId);
        //        case IDeleteTenantResponse.NotFound notFound:
        //            return NotFound();
        //        case IDeleteTenantResponse.InternalError internalError:
        //            return BadRequest(internalError.Message);
        //        default:
        //            return BadRequest("Internal Server Error");
        //    }            
        //}
    }
}
