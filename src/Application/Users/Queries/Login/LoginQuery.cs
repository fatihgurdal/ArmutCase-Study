using Application.Common.Interfaces;
using Application.Users.Options;

using MediatR;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MongoDB.Driver;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Users.Queries.Login;

public record LoginQuery : IRequest<TokenVm>
{
    public string UserName { get; init; }
    public string Password { get; init; }
}

public class LoginQueryHandler : IRequestHandler<LoginQuery, TokenVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IOptions<JwtOptions> _options;

    public LoginQueryHandler(IApplicationDbContext context, IOptions<JwtOptions> options)
    {
        _context = context;
        _options = options;
    }

    public async Task<TokenVm> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Find(x => x.UserName == request.UserName && x.Password == request.Password)
                                .FirstOrDefaultAsync() ?? throw new Exception("TODO custom exception");


        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,user.Id.ToString()),
            new Claim(ClaimTypes.Role,"Standart"),//Rollük bir ister olmadığı için
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Value.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return new TokenVm
        {
            Token = tokenHandler.WriteToken(securityToken)
        };

    }
}