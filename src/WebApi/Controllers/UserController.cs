using Application.Users.Commands.CreateUser;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ApiControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateUserCommand command)
        {
            var id = await Mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id }, default);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public Task<ActionResult> Get([FromRoute] Guid id)
        {
            throw new NotImplementedException($"For create endpoint. Request id:{id}");
        }
    }
}