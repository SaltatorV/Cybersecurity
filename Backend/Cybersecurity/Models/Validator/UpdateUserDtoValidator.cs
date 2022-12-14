using Cybersecurity.Entities;
using Cybersecurity.Exceptions;
using Cybersecurity.Interfaces.Repositories;
using Cybersecurity.Models.DTO;
using FluentValidation;

namespace Cybersecurity.Models.Validator
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator(IGenericRepository<User> userRepository)
        {

            RuleFor(u => u.Login )
                .NotEmpty()
                .Must((value, context) =>
                {
                    var existingUser = userRepository.ExistAsync(u => u.Login == value.Login).Result;

                    if (existingUser)
                    {
                        var existingLogin = userRepository.GetByPredicateAsync(u => u.Login == value.Login).Result;

                        if (existingLogin.Id != value.Id)
                        {
                            return false;
                        }
                    }

                    return true;
                }).WithMessage("Login zajęty");
        }
    }
}
