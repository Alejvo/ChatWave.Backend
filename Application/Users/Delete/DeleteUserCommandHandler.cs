using Domain.Interfaces;
using Domain.Utilities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand,ErrorOr<Unit>>
    {
        private readonly IUserRepository _repository;

        public DeleteUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(UserProcedures.GetUserById,new { request.Id });

            if(user != null)
            {
                await _repository.DeleteAsync(UserProcedures.DeleteUser,new { request.Id });
            }
            return Unit.Value;

        }
    }
}
