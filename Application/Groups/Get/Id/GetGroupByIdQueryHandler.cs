using Application.Groups.Common;
using Application.Users.Common;
using Domain.Interfaces;
using Domain.Models;
using Domain.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Get.Id
{
    public class GetGroupByIdQueryHandler:IRequestHandler<GetGroupByIdQuery,GroupResponse>
    {
        private readonly IGroupRepository _repository;

        public GetGroupByIdQueryHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<GroupResponse> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById(GroupProcedures.GetGroupById,new { request.Id });
            if(group != null)
            {
                return GroupResponse.ToGroupResponse(group);
            }
            return default;
        }
    }
}
