using Chonye.Core.DTOs;
using Chonye.Domain.Helpers;
using Chonye.Domain.Request;
using Chonye.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Chonye.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseConfig _config;
        private readonly ILogger<TenantController> _logger;

        public UserController(IOptions<DatabaseConfig> config, ILogger<TenantController> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        // GET: api/<UserController>
        [ProducesResponseType(type: typeof(ActionResult<IListUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IListUserResponse>>> Get([FromServices] IMediator mediator, [FromQuery] UserParameters request)
        {
            var result = await mediator.Send(new ListUserRequest
            { 
                UserId = request.UserId,
            });

            switch (result)
            {
                case IListUserResponse.Succeeded sucess:
                    return Ok(sucess);
                case IListUserResponse.NotFound:
                    return NotFound();
                case IListUserResponse.InternalError internalError:
                    return BadRequest(internalError);
                default:
                    return BadRequest("Internal Server Error");
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]

        [HttpDelete("{id}")]
        public async Task<ActionResult<HttpResponseMessage>> Delete([FromServices] IMediator mediator, [FromBody] DeleteUserRequest request)
        {
            var result = await mediator.Send(new DeleteUserRequest(request.Id));

            switch (result)
            {
                case IDeleteUserResponse.Succeeded successResult:
                    return Ok(successResult.GlobalId);
                case IDeleteUserResponse.NotFound notFound:
                    return NotFound();
                case IDeleteUserResponse.InternalError internalError:
                    return BadRequest(internalError.Message);
                default:
                    return BadRequest("Internal Server Error");
            }
        }
    }
}
