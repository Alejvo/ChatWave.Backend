using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Send.ToGroup
{
    public class SendGrupalMessageValidator : AbstractValidator<SendGrupalMessageCommand>
    {
        public SendGrupalMessageValidator()
        {
            RuleFor(message=>message.GroupId).NotEmpty();
            RuleFor(message=>message.SenderId).NotEmpty();
            RuleFor(message=>message.Text).NotNull();
        }
    }
}
