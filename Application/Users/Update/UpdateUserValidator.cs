using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Update
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.Id)
                .NotNull();
            RuleFor(user=>user.FirstName)
                .NotNull();
            RuleFor(user=>user.LastName)
                .NotNull();
            RuleFor(user=>user.UserName)

                .NotNull();
            RuleFor(user => user.Email)
            .EmailAddress()

                .NotNull();
            RuleFor(user => user.Password)
                .NotNull();
            RuleFor(user=>user.Birthday)
                .GreaterThan(new DateTime(1900, 1, 1))
                .LessThan(DateTime.Now)
                .NotNull();
        }
    }
}
