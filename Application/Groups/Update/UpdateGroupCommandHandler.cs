using Domain.Interfaces;
using Domain.Models;
using Domain.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Update
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand>
    {
        private readonly IGroupRepository _repository;

        public UpdateGroupCommandHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById(GroupProcedures.GetGroupById,new { request.Id });
            if(group != null)
            {
                await _repository.UpdateAsync(GroupProcedures.UpdateGroup, new {request.Id,request.Name,request.Description});
            }
        }
    }
}
