using FluentValidation;
using Store.Business.Models;

namespace Store.Business.Validations
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
