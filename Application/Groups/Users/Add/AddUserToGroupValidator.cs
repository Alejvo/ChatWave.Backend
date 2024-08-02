using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Users.Add
{
    public class AddUserToGroupValidator:AbstractValidator<AddUserToGroupCommand>
    {
        public AddUserToGroupValidator()
        {
            RuleFor(group=>group.groupId).NotEmpty();
            RuleFor(group=>group.userId).NotEmpty();
        }
    }
}
