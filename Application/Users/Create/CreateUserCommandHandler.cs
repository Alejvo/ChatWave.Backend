using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Create
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IRepository<User> _repository;

        public CreateUserCommandHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User 
            { 
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName ,
                Email = request.Email ,
                FirstName = request.FirstName ,
                LastName = request.LastName,
                Password = request.Password ,
                Birthday = request.Birthday
                
            };
            await _repository.CreateAsync(user);
        }
    }
}
