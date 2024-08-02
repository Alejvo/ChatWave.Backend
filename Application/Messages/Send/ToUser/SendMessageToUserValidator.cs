using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Send.ToUser
{
    public class SendMessageToUserValidator : AbstractValidator<SendMessageToUserCommand>
    {
        public SendMessageToUserValidator()
        {
            RuleFor(message=>message.Text).NotEmpty();
            RuleFor(message=>message.SenderId).NotEmpty();
            RuleFor(message => message.ReceiverId).NotEmpty();
        }
    }
}
