using Application.Users.Commands.RegisterUser;
using Application.Users.Queries.GetUser;

using FluentAssertions;

using NUnit.Framework;

using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Users.Commands;

using static Testing;

[TestFixture]
public class RegisterUserTests
{
    [Test]
    public async Task RegisterUser_Sucess()
    {
        var newGuid = Guid.NewGuid().ToString();
        var command = new RegisterUserCommand()
        {
            FirstName = $"FirstNameTest{newGuid}",
            LastName = $"FirstNameTest{newGuid}",
            UserName = newGuid,
            Password = newGuid,
        };

        var userId = await FluentActions.Invoking(() =>
              SendAsync(command)).Invoke();

        var user = await FluentActions.Invoking(() => SendAsync(new GetUserQuery() { UserId = userId })).Invoke();

        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.UserName.Should().Be(command.UserName);
        user.FirstName.Should().Be(command.FirstName);
        user.LastName.Should().Be(command.LastName);
    }
}

