using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Groups.Delete
{
    public class DeleteGroupValidator : AbstractValidator<DeleteGroupCommand>
    {
        public DeleteGroupValidator()
        {
            RuleFor(group => group.Id)
                .NotNull();
        }

    }
}
