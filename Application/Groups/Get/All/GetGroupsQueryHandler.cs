using Application.Groups.Common;
using Application.Users.Common;
using Domain.Interfaces;
using Domain.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Groups.Get.All
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery,IEnumerable<GroupResponse>>
    {
        private readonly IGroupRepository _repository;

        public GetGroupsQueryHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GroupResponse>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups =await _repository.GetAll(GroupProcedures.GetGroups);
            return groups.Select(group =>

                GroupResponse.ToGroupResponse(group)
            );
        }
    }
}
