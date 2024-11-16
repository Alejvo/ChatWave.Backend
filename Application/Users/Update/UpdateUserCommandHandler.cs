using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Users;
using Domain.Utilities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,ErrorOr<Unit>>
    {
        private readonly IUserRepository _repository;

        public UpdateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            byte[] profileImageBytes = null;
            if (request.ProfileImage != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.ProfileImage.CopyToAsync(memoryStream);
                    profileImageBytes = memoryStream.ToArray();
                }
            }
            var user = await _repository.GetById(UserProcedures.GetUserById,new { request.Id });

            if(user != null) 
            {
                var updatedUser = new User
                {
                    Id = user.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = request.Password,
                    Birthday = request.Birthday,
                    UserName = request.UserName,
                    ProfileImage = profileImageBytes 
                };
                await _repository.UpdateAsync(UserProcedures.UpdateUser,updatedUser);
            }
            return Unit.Value;
        }
    }
}
