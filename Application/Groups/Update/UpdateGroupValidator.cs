using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Update
{
    public class UpdateGroupValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupValidator()
        {
            RuleFor(group => group.Id).NotEmpty();
            RuleFor(group=>group.Name).NotEmpty();
            RuleFor(group=>group.Description).NotEmpty();
        }
    }
}
