using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.UserName).MaximumLength(100).NotEmpty();
            RuleFor(x => x.FirstName).MaximumLength(50).NotEmpty();
            RuleFor(x => x.LastName).MaximumLength(50).NotEmpty();
        }
    }
}
