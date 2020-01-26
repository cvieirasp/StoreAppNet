using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Store.App.ViewModels
{
    public class AddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo CEP precisa ter 8 caracteres", MinimumLength = 8)]
        public string CEP { get; set; }

        [Required(ErrorMessage = "O campo Logradouro é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo Logradouro precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Logradouro")]
        public string PublicPlace { get; set; }

        [Required(ErrorMessage = "O campo Número é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo Número precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Número")]
        public string Number { get; set; }

        [MaxLength(200, ErrorMessage = "O campo Complemento pode terno máximo {1} caracteres")]
        [DisplayName("Complemento")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "O campo Bairro é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Bairro precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Bairro")]
        public string District { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Cidade precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Cidade")]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo Estado precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Estado")]
        public string State { get; set; }

        [HiddenInput]
        public Guid SupplierId { get; set; }
    }
}
