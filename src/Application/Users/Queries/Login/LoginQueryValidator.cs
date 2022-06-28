using FluentValidation;

namespace Application.Users.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.UserName).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Password).MaximumLength(3).NotEmpty();
        }
    }
}
