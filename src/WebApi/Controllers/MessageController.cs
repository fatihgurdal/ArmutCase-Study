using Application.Users.Commands.RegisterUser;
using Application.Users.Commands.SendMessage;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize]
    public class MessageController : ApiControllerBase
    {
        private readonly ILogger<MessageController> _logger;

        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }
        //TODO: register user
        //TODO: send message
        //TODO: get messages
        //TODO: block user
        //TODO: get activties

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> SendMessage([FromBody] SendMessageCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}