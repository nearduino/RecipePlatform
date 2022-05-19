using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Model.Validators
{
    public class UserValidator : AbstractValidator<RegistrationRequest>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().NotNull().WithMessage("First name should be not empty.");
            RuleFor(user => user.LastName).NotEmpty().NotNull().WithMessage("Last name should be not empty."); 
            RuleFor(user => user.UserName).NotEmpty().NotNull().WithMessage("Username should be not empty."); 
            RuleFor(user => user.Email).EmailAddress().WithMessage("Email is invalid.");
            RuleFor(user => user.Password).NotEmpty().NotNull().WithMessage("Password should be not empty.");
            RuleFor(user => user.ConfirmPassword).NotEmpty().NotNull().WithMessage("Confirmation password should be not empty.");
            RuleFor(user => user.ConfirmPassword).Equal(user => user.Password).WithMessage("Password and confirmation password should match."); ;
        }
    }
}
