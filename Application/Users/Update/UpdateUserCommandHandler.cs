using Domain.Interfaces;
using Domain.Models;
using Domain.Utilities;
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
        private readonly IUserRepository _repository;

        public UpdateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(UserProcedures.GetUserById,new { request.Id });
            var updatedUser = new User
            {
                Id = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                Birthday = request.Birthday,
                UserName = request.UserName
            };
            if(user != null) 
            {
                await _repository.UpdateAsync(UserProcedures.UpdateUser,updatedUser);
            }
        
        }
    }
}
