using Cybersecurity.Entities;
using Cybersecurity.Exceptions;
using Cybersecurity.Interfaces.Repositories;
using Cybersecurity.Models.DTO;
using FluentValidation;

namespace Cybersecurity.Models.Validator
{
    public class AdminUpdateUserDtoValidator : AbstractValidator<AdminUpdateUserDto>
    {
        public AdminUpdateUserDtoValidator(IGenericRepository<Role> roleRepository, IGenericRepository<User> userRepository)
        {
            RuleFor(u => u.Login)
                .NotEmpty();

            RuleFor(u => new { u.Id, u.Login })
                .Custom((value, context) =>
                {
                    var existingUser = userRepository.ExistAsync(u => u.Login == value.Login).Result;

                    if (existingUser)
                    {
                        var existingLogin = userRepository.GetByPredicateAsync(u => u.Login == value.Login).Result;

                        if (existingLogin.Id != value.Id)
                        {
                            throw new BadRequestException("Login zajęty");
                        }
                    }
                });

            RuleFor(u => u.IsPasswordExpire).NotEmpty();

            RuleFor(u => u.DayExpire).NotEmpty();

            RuleFor(u => u.RoleId)
                    .Custom((value, context) =>
                    {
                        var existingRole = roleRepository.ExistAsync(r => r.Id == value).Result;

                        if (!existingRole)
                            throw new NotFoundException("Rola nie znaleziona");
                    });
        }
    }
}
