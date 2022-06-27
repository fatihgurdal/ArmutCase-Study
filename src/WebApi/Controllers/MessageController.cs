using Application.Users.Commands.RegisterUser;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public async Task<ActionResult> Send([FromBody] SendMessageCommand command)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("[controller]/{userName:string}")]
        //public async Task<ActionResult> GetMessages([FromRoute] string userName)
        //{
        //    throw new NotImplementedException();
        //}


    }
}