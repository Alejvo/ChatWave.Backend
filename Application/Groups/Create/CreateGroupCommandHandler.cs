using Domain.Interfaces;
using Domain.Models;
using Domain.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Create
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand>
    {
        private readonly IGroupRepository _repository;

        public CreateGroupCommandHandler(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description
            };

            await _repository.CreateAsync(GroupProcedures.CreateGroup,group);
        }
    }
}
