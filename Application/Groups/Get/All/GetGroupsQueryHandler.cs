using Application.Users.Common;
using Domain.Interfaces;
using Domain.Models.Groups;
using Domain.Utilities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Groups.Get.All
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery,ErrorOr<IReadOnlyList<GroupResponse>>>
    {
        private readonly IGroupRepository _repository;

        public GetGroupsQueryHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<IReadOnlyList<GroupResponse>>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups =await _repository.GetAll(GroupProcedures.GetGroups);
            var groupResponse = groups.Select(group=>GroupResponse.ToGroupResponse(group)).ToList();
            return groupResponse;
        }
    }
}
