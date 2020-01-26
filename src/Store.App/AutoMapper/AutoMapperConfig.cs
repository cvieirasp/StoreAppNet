using AutoMapper;
using Store.App.ViewModels;
using Store.Business.Models;

namespace Store.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
        }
    }
}
