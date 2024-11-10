using Domain.Interfaces;
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
    public class GetGroupsByNameQueryHandler : IRequestHandler<GetGroupsByNameQuery, ErrorOr<IReadOnlyList<GroupResponse>>>
    {
        private readonly IGroupRepository _repository;

        public GetGroupsByNameQueryHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<IReadOnlyList<GroupResponse>>> Handle(GetGroupsByNameQuery request, CancellationToken cancellationToken)
        {
            var groups= await _repository.GetByNames(request.name);
            return groups.ToList();
        }
    }
}
