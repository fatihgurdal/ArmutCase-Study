using Application.Users.Commands.BlockUser;
using Application.Users.Commands.RegisterUser;
using Application.Users.Queries.GetUser;
using Application.Users.Queries.Login;
using Application.Users.Queries.UserActivities;

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
        /// <summary>
        /// Yeni hesap oluşturma. Minimum bilgi ile password'ü açık bir şekilde yapar.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<Guid>> Register([FromBody] RegisterUserCommand command)
        {
            var id = await Mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id }, default);
        }

        /// <summary>
        /// JWT ile Bearer token üretir. Ürettiği token Bearer'ın standart kullanımı ile Header'da gönderilmesi gerekir.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenVm>> Login([FromBody] LoginQuery query)
        {
            var token = await Mediator.Send(query);

            return Ok(token);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new GetUserQuery() { UserId = id }));
        }

        /// <summary>
        /// Bir kullanıcı engellemek veya engelini kaldırmak için kullanılır. Eğer kişi engelli olduğu halde engellenmeye çalışılırsa hata vermez. Aynı şekilde engeli kalkmış birini yine engeli kaldırılacaksa hata vermez. 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Block")]
        public async Task<ActionResult> Block([FromBody] BlockUserCommand query)
        {
            await Mediator.Send(query);

            return NoContent();
        }
        /// <summary>
        /// Kullanıcının bazı aktivitelerini listeler. Login, kayıt, başarısız girişler ve kullanıcı engellemeleri
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Activities")]
        public async Task<ActionResult> Activities()
        {
            var activities = await Mediator.Send(new UserActivitiesQuery());

            return Ok(activities);
        }
    }
}