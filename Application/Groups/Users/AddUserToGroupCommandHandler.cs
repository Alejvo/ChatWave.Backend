using Domain.Interfaces;
using Domain.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Users
{
    public class AddUserToGroupCommandHandler:IRequestHandler<AddUserToGroupCommand>
    {
        private readonly IGroupRepository _repository;

        public AddUserToGroupCommandHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddUserToGroup(GroupProcedures.AddUser,request.groupId,request.userId);
        }
    }
}
