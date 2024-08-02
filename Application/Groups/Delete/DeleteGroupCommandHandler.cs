using Domain.Interfaces;
using Domain.Utilities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Delete
{
    public class DeleteGroupCommandHandler:IRequestHandler<DeleteGroupCommand, ErrorOr<Unit>>
    {
        private readonly IGroupRepository _repository;

        public DeleteGroupCommandHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById(GroupProcedures.GetGroupById,new { request.Id });
            if(group != null)
            {
                await _repository.DeleteAsync(GroupProcedures.DeleteGroup,new { request.Id });
            }

            return Unit.Value;
        }
    }
}
