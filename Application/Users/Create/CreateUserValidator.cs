using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Create
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator(IUserRepository repository)
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .Must(email => repository.IsEmailUnique(email))
                .WithMessage("email already exists.")
                .NotEmpty();
            RuleFor(x => x.FirstName)
                .NotEmpty();
            RuleFor(x=>x.LastName)
                .NotEmpty();
            RuleFor(x => x.Birthday)
                .GreaterThan(new DateTime(1900, 1, 1))
                .LessThan(DateTime.Now)
                .NotEmpty();
            RuleFor(x => x.UserName)
                .Must(username => repository.IsUserNameUnique(username))
                .WithMessage("Username already exists.");
            RuleFor(x => x.Password)
                .NotEmpty();
            
        }
    }
}
