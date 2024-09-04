﻿using Domain.Models;
using Domain.Models.Messages;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Get.User
{
    public record GetUserMessagesQuery(string ReceiverId, string SenderId) : IRequest<ErrorOr<IReadOnlyList<MessagesBySender>>>;

}