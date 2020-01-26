using System;

namespace Store.Business.Models
{
    public class Address : Entity
    {
        public Guid SupplierID { get; set; }
        public string CEP { get; set; }
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        /* EF Relation */
        public Supplier Supplier { get; set; }
    }
}
