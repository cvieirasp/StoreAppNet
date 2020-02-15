using Store.Business.Interfaces;
using Store.Business.Models;
using Store.Business.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _repository;
        private readonly IAddressRepository _addressRepository;

        public SupplierService(ISupplierRepository repository, IAddressRepository addressRepository, INotificator notificator)
            : base(notificator)
        {
            _repository = repository;
            _addressRepository = addressRepository;
        }

        public async Task Add(Supplier supplier)
        {
            if (!Validate(new SupplierValidation(), supplier) || !Validate(new AddressValidation(), supplier.Address))
                return;

            if (_repository.Search(s => s.Document == supplier.Document).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento informado");
                return;
            }

            await _repository.Add(supplier);
        }

        public async Task Update(Supplier supplier)
        {
            if (!Validate(new SupplierValidation(), supplier) && !Validate(new AddressValidation(), supplier.Address))
                return;

            if (_repository.Search(s => s.Document == supplier.Document && s.Id != supplier.Id).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento informado");
                return;
            }

            await _repository.Update(supplier);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!Validate(new AddressValidation(), address))
                return;

            await _addressRepository.Update(address);
        }

        public async Task Delete(Guid id)
        {
            if (_repository.Get(id).Result.Products.Any())
            {
                Notify("Não é possível excluir um fornecedor com produtos cadastrados");
                return;
            }

            await _repository.Delete(id);
        }

        public void Dispose()
        {
            _repository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
