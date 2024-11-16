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

namespace Application.Users.Create
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,ErrorOr<Unit>>
    {
        private readonly IUserRepository _repository;

        public CreateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Unit>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
            var user = new User
            { 
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName ,
                Email = request.Email ,
                FirstName = request.FirstName ,
                LastName = request.LastName,
                Password = request.Password ,
                Birthday = request.Birthday ,
                ProfileImage = profileImageBytes
                
            };
            await _repository.CreateAsync(UserProcedures.CreateUser, new {user.Id,user.FirstName,user.LastName,user.Email,user.Password,user.Birthday,user.UserName,user.ProfileImage});
            return Unit.Value;
        }
    }
}
