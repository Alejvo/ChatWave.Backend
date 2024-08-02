using Domain.Interfaces;
using Domain.Utilities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Users.Remove
{
    public class RemoveUserFromGroupCommandHandler : IRequestHandler<RemoveUserFromGroupCommand, ErrorOr<Unit>>
    {
        private readonly IGroupRepository _repository;

        public RemoveUserFromGroupCommandHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Unit>> Handle(RemoveUserFromGroupCommand request, CancellationToken cancellationToken)
        {
            await _repository.RemoveUserToGroup(GroupProcedures.RemoveUser, request.groupId, request.userId);
            return Unit.Value;
        }
    }
}
