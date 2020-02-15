using FluentValidation;
using Store.Business.Models;
using Store.Business.Validations.Documents;

namespace Store.Business.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(s => s.SupplierType == SupplierType.IndividualRegistration, () =>
            {
                RuleFor(s => s.Document.Length).Equal(CpfValidation.CPF_LENGTH)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");
                RuleFor(s => CpfValidation.Validate(s.Document)).Equal(true)
                .WithMessage("O CPF está em formato inválido");
            });

            When(s => s.SupplierType == SupplierType.LegalEntityRegistration, () =>
            {
                RuleFor(s => s.Document.Length).Equal(CnpjValidation.CNPJ_LENGTH)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");
                RuleFor(s => CnpjValidation.Validate(s.Document)).Equal(true)
                .WithMessage("O CNPJ está em formato inválido");
            });
        }
    }
}