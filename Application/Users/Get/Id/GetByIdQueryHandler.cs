using Application.Users.Common;
using Domain.Interfaces;
using Domain.Models.Users;
using Domain.Utilities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.GetBy.Id;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ErrorOr<UserResponse>>
{

    private readonly IUserRepository _repository;

    public GetByIdQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<UserResponse>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        if(await _repository.GetById(UserProcedures.GetUserById, new { request.Id }) is not User user){
            return Error.NotFound();
        }
        return UserResponse.ToUserResponse(user);
    }
}
