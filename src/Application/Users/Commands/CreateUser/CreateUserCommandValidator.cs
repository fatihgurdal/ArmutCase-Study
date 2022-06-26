using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName).MaximumLength(100).NotEmpty();
            RuleFor(x => x.FirstName).MaximumLength(50).NotEmpty();
            RuleFor(x => x.LastName).MaximumLength(50).NotEmpty();
        }
    }
}
