using FluentValidation;

namespace Application.Messages.Commands.SendMessage
{
    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator()
        {
            RuleFor(x => x.UserName).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Message).MaximumLength(2000).NotEmpty();
        }
    }
}
