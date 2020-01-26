using System.Collections.Generic;

namespace Store.Business.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }

        /* EF Relation */
        public IEnumerable<Product> Products { get; set; }
    }
}
