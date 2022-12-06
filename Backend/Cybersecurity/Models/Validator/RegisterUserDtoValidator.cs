using Cybersecurity.Entities;
using Cybersecurity.Exceptions;
using Cybersecurity.Interfaces.Repositories;
using Cybersecurity.Models.DTO;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Cybersecurity.Models.Validator
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(IGenericRepository<Role> roleRepository)
        {
            RuleFor(u => u.Login)
                .NotEmpty();

            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(new Regex(@"(?=[A-Za-z0-9@#$%^&+!=]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+!=]).*$"))
                .WithMessage("Hasło musi posiadać przynajmniej jedną dużą literę, jeden znak specjalny, cyfre oraz mieć conajmniej 8 znaków");

            RuleFor(u => u.ConfirmPassword)
                .NotEmpty()
                .Equal(u => u.Password)
                .WithMessage("Hasła muszą być takie same");

            RuleFor(r => r.RoleId)
                .Custom((value, context) =>
                {
                    var existingRole = roleRepository.ExistAsync(r => r.Id == value).Result;

                    if (!existingRole)
                    {
                        throw new NotFoundException("Rola nie znaleziona");
                    }
                });
        }
    }
}
