using Application.Users.Commands.RegisterUser;
using Application.Users.Queries.Login;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize]
    public class AccountController : ApiControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<Guid>> Register([FromBody] RegisterUserCommand command)
        {
            var id = await Mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id }, default);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Account/Login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenVm>> Login([FromBody] LoginQuery query)
        {
            var token = await Mediator.Send(query);

            return Ok(token);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public Task<ActionResult> Get([FromRoute] Guid id)
        {
            throw new NotImplementedException($"For create endpoint. Request id:{id}");
        }
    }
}