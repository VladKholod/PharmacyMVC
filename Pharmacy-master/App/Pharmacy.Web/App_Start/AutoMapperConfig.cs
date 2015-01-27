using AutoMapper;
using Pharmacy.Core;
using Pharmacy.Web.Core.Models;

namespace Pharmacy.Web.App_Start
{
    public class AutoMapperConfig
    {
        public void Init()
        {
            Mapper.CreateMap<Pharmacy.Core.Pharmacy, PharmacyViewModel>();
            Mapper.CreateMap<PharmacyViewModel, Pharmacy.Core.Pharmacy>();

            Mapper.CreateMap<Medicament, MedicamentViewModel>();
            Mapper.CreateMap<MedicamentViewModel, Medicament>();

            Mapper.CreateMap<Order, OrderViewModel>();
            Mapper.CreateMap<OrderViewModel, Order>();

//            Mapper.CreateMap<Medicament, MedicamentPriceHistory>();
//            Mapper.CreateMap<Storage, StorageViewModel>();
//            Mapper.CreateMap<Order, OrderViewModel>();
//            Mapper.CreateMap<OrderDetails, OrderDetailsViewModel>();
//            Mapper.CreateMap<MedicamentPriceHistory, MedicamentPriceHistoryViewModel>();
//
//            Mapper.AssertConfigurationIsValid();
        }
    }
}