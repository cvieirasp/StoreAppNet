using FluentValidation;
using Store.Business.Models;

namespace Store.Business.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(a => a.CEP)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .Length(8)
                .WithMessage("O campo {PropertyName} precisa ter {MaxLength} caracteres");

            RuleFor(a => a.PublicPlace)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .Length(2, 200)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.Number)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .Length(1, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.District)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .Length(2, 200)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.City)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.State)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .Length(2, 50)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
