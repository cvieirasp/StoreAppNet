using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Store.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo Nome precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo Descrição precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName( "Descrição")]
        public string Description { get; set; }

        [DisplayName("Imagem")]
        public string Image { get; set; }

        public IFormFile ImageUpload { get; set; }

        [Required(ErrorMessage = "O campo Valor é obrigatório")]
        [DisplayName("Valor")]
        public string Value { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Created { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Updated { get; set; }

        [DisplayName("Ativo?")]
        public bool Active { get; set; }

        public SupplierViewModel Supplier { get; set; }

        public CategoryViewModel Category { get; set; }
    }
}
