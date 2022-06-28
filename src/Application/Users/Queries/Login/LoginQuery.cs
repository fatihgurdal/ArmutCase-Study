using Application.Common.Interfaces;
using Application.Users.Options;

using Domain.Events;

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
        var user = await _context.Users.Find(x => x.UserName == request.UserName)
                                .FirstOrDefaultAsync() ?? throw new Exception("TODO: custom user not found");
        if (!user.Password.Equals(request.Password))
        {
            user.AddDomainEvent(new UserLoginEvent(user, "1.2.3.4")); //TODO: read ip fix
            throw new Exception("TODO: custom login failed");
        }
        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _options.Value.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString())//TODO: add const
                    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _options.Value.Issuer,
            _options.Value.Audience,
            claims,
            expires: DateTime.UtcNow.AddDays(1),//case çalışması olduğu için süreyi uzun verdim.
            signingCredentials: signIn);

        user.AddDomainEvent(new UserLoginEvent(user, "1.2.3.4")); //TODO: read ip fix

        return new TokenVm
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };

    }
}