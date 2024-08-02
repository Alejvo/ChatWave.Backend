using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Users.Remove
{
    public class RemoveUserFromGroupValidator:AbstractValidator<RemoveUserFromGroupCommand>
    {
        public RemoveUserFromGroupValidator()
        {
            RuleFor(group => group.userId).NotEmpty();
            RuleFor(group=>group.groupId).NotEmpty();
        }
    }
}
