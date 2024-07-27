﻿using Application.Groups.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Get.Id
{
    public record GetGroupByIdQuery(string Id):IRequest<GroupResponse>;
}