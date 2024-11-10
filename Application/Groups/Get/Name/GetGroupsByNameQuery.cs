using Domain.Models.Groups;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Get.Name
{
    public record GetGroupsByNameQuery
        (
            string name
        ) :IRequest<ErrorOr<IReadOnlyList<GroupResponse>>>;
}
