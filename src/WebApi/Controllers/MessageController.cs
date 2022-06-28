using Application.Users.Commands.RegisterUser;
using Application.Users.Commands.SendMessage;
using Application.Users.Queries.GetMessages;

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
        /// <summary>
        /// Gerçek zamanlı değildir. Kullanıcı adı ve mesaj içeriği ile gönderim yapabilir.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> SendMessage([FromBody] SendMessageCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
        /// <summary>
        /// İki kişi arasındaki mesajları döner. Route'da gönderilen kullanıcı adının alıcı ve gönderici olduğu ortak sohbet mesajlarını döner
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{userName}")]
        public async Task<ActionResult<IEnumerable<MessageVm>>> GetMessages([FromRoute] string userName)
        {
            var messages = await Mediator.Send(new GetMessagesQuery() { UserName = userName });
            return Ok(messages);
        }
    }
}