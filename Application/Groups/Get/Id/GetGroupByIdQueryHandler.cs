using Application.Groups.Common;
using Application.Users.Common;
using Domain.Interfaces;
using Domain.Models;
using Domain.Utilities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Get.Id;

public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, ErrorOr<GroupResponse>>
{
    private readonly IGroupRepository _repository;

    public GetGroupByIdQueryHandler(IGroupRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<GroupResponse>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _repository.GetById(GroupProcedures.GetGroupById, new { request.Id }) is not Group group)
        {
            return Error.NotFound();
        }
        return GroupResponse.ToGroupResponse(group);
    }

}
