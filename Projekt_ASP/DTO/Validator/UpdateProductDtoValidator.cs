using FluentValidation;

namespace Projekt_ASP.DTO.Validator
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDTO>
    {
        
        public void UpdateProductDtoValidatorFun()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Name).Length(50);
            
            RuleFor(x => x.IsAvailable).NotEmpty();
        }
    }
}
