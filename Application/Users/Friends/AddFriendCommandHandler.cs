using Domain.Interfaces;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Friends
{
    public class AddFriendCommandHandler : IRequestHandler<AddFriendCommand, ErrorOr<Unit>>
    {
        private readonly IUserRepository _userRepository;

        public AddFriendCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<Unit>> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.AddFriend(request.userId, request.friendId);
            return Unit.Value;
        }
    }
}
