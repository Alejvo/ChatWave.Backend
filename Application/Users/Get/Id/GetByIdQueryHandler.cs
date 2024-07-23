using Application.Users.Common;
using Domain.Interfaces;
using Domain.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.GetBy.Id
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, UserResponse>
    {

        private readonly IUserRepository _repository;

        public GetByIdQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(UserProcedures.GetUserById, new {request.Id});
            if(user != null)
            {
                return UserResponse.ToUserResponse(user);
            }
            return default;
        }
    }
}
