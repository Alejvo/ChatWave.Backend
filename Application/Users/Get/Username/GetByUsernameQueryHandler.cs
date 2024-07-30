using Application.Users.Common;
using Domain.Interfaces;
using Domain.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.GetBy.Username
{
    public class GetByUsernameQueryHandler : IRequestHandler<GetByUsernameQuery, UserResponse>
    {
        private readonly IUserRepository _repository;

        public GetByUsernameQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponse> Handle(GetByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByUserName(request.Username);
            if(user == null)
            {
                return default;
            }
            return UserResponse.ToUserResponse(user);
        }
    }
}
