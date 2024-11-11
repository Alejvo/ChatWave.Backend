using Application.Users.Common;
using Domain.Interfaces;
using Domain.Utilities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.GetBy.Username
{
    public class GetByUsernameQueryHandler : IRequestHandler<GetByUsernameQuery, ErrorOr<IEnumerable<UserResponse>>>
    {
        private readonly IUserRepository _repository;

        public GetByUsernameQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<IEnumerable<UserResponse>>> Handle(GetByUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUsersByUsername(request.Username);
            if(user == null)
            {
                return default;
            }
            var userResponse = user.Select(user=> UserResponse.ToUserResponse(user));
            return userResponse.ToList();
        }
    }
}
