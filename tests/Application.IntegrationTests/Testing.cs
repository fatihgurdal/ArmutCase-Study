//using Infrastructure.Identity;
using Infrastructure.Persistence;

using MediatR;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;

using System;
using System.IO;
using System.Threading.Tasks;

//using Respawn;

namespace Application.IntegrationTests;

public class Testing
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static Guid _currentUserId;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        SpinupDocker();
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
    }
    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static Guid GetCurrentUserId()
    {
        return _currentUserId;
    }

    private void SpinupDocker()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "docker-compose.yml");
        new Ductus.FluentDocker.Builders.Builder()
                   .UseContainer()
                   .UseCompose()
                   .FromFile(path)
                   .RemoveOrphans()
                   .Build()
                   .Remove(true);
        new Ductus.FluentDocker.Builders.Builder()
                    .UseContainer()
                    .WithName("Application.IntegrationTests")
                    .UseCompose()
                    .FromFile(path)
                    .Build()
                    .Start();
    }

}

