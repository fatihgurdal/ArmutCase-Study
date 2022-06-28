using FluentValidation;

namespace Application.Users.Commands.BlockUser
{
    public class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
    {
        public BlockUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
