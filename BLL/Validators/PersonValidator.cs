using BLL.DTO;
using FluentValidation;

namespace BLL.Validators
{
    public class PersonValidator : AbstractValidator<PersonDTO>
    {
        public PersonValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().NotNull().WithMessage("First Name is required.");
            RuleFor(p => p.LastName).NotEmpty().NotNull().WithMessage("Last Name is required.");
            RuleFor(p => p.Phone).NotEmpty()
                .NotNull().WithMessage("Phone Number is required.")
                .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(20).WithMessage("PhoneNumber must not exceed 20 characters.");
            //.Matches(new Regex(@"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/")).WithMessage("PhoneNumber not valid");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email");
            RuleFor(p => p.Age).NotEmpty().WithMessage("Age is required.")
                .InclusiveBetween(18, 90);
            RuleFor(p => p.IdNumber).NotEmpty().Length(9);
        }
    }
}
