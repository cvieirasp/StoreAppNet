using Store.Business.Interfaces;
using Store.Business.Models;
using Store.Business.Validations;
using System;
using System.Threading.Tasks;

namespace Store.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        public async Task Add(Supplier supplier)
        {
            if (!Validate(new SupplierValidation(), supplier) && !Validate(new AddressValidation(), supplier.Address))
                return;


        }

        public async Task Update(Supplier supplier)
        {
            if (!Validate(new SupplierValidation(), supplier))
                return;


        }

        public async Task UpdateAddress(Address address)
        {
            if (!Validate(new AddressValidation(), address))
                return;


        }

        public async Task Delete(Guid id)
        {

        }
    }
}
