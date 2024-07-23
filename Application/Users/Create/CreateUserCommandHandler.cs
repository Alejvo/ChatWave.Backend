﻿using Domain.Interfaces;
using Domain.Models;
using Domain.Utilities;
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
        private readonly IUserRepository _repository;

        public CreateUserCommandHandler(IUserRepository repository)
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
            await _repository.CreateAsync(UserProcedures.CreateUser,user);
        }
    }
}
