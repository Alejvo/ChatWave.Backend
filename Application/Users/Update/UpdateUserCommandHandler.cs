using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IRepository<User> _repository;

        public UpdateUserCommandHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Id);
            if(user != null) 
            {
                await _repository.UpdateAsync(user);
            }
        
        }
    }
}
