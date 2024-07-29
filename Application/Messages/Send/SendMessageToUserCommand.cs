﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Send
{
    public record SendMessageToUserCommand(
            string Text,
            string SenderId,
            string ReceiverId
        ) :IRequest;
}
