﻿using Domain.Models;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Get
{
    public record GetGrupalMessagesQuery(string GroupId) : IRequest<ErrorOr<IReadOnlyList<MessageResponse>>>;
  
}
