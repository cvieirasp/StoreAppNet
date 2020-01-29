using System;

namespace Store.Business.Models
{
    public class Product : Entity
    {
        public Guid SupplierId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Value { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Active { get; set; }

        /* EF Relation */
        public Supplier Supplier { get; set; }
        public Category Category { get; set; }
    }
}
