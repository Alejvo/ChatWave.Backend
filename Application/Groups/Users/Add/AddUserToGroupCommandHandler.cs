using Domain.Interfaces;
using Domain.Utilities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Users.Add
{
    public class AddUserToGroupCommandHandler : IRequestHandler<AddUserToGroupCommand, ErrorOr<Unit>>
    {
        private readonly IGroupRepository _repository;

        public AddUserToGroupCommandHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Unit>> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddUserToGroup(GroupProcedures.AddUser, request.groupId, request.userId);
            return Unit.Value;
        }
    }
}
