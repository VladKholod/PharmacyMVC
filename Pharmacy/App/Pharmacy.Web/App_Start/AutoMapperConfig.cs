using AutoMapper;
using Pharmacy.Core;
using Pharmacy.Web.Core.Models;
using Pharmacy.Web.Core.Models.MedicamentPriceHistories;
using Pharmacy.Web.Core.Models.Medicaments;
using Pharmacy.Web.Core.Models.OrderDetailses;
using Pharmacy.Web.Core.Models.Orders;
using Pharmacy.Web.Core.Models.Pharmacies;
using Pharmacy.Web.Core.Models.Storages;

namespace Pharmacy.Web
{
    public class AutoMapperConfig
    {
        public void Init()
        {
            #region Pharmacy

            Mapper.CreateMap<Pharmacy.Core.Pharmacy, PharmacyViewModel>();
            Mapper.CreateMap<PharmacyViewModel, Pharmacy.Core.Pharmacy>();

            #endregion

            #region Medicament

            Mapper.CreateMap<Medicament, MedicamentViewModel>();
            Mapper.CreateMap<MedicamentViewModel, Medicament>();

            #endregion

            #region Order

            Mapper.CreateMap<Order, OrderViewModel>();
            Mapper.CreateMap<OrderViewModel, Order>();

            Mapper.CreateMap<Order, CreateOrderViewModel>();
            Mapper.CreateMap<CreateOrderViewModel, Order>();

            Mapper.CreateMap<Order, EditOrderViewModel>();
            Mapper.CreateMap<EditOrderViewModel, Order>();

            #endregion

            #region OrderDetails

            Mapper.CreateMap<OrderDetails, OrderDetailsViewModel>();
            Mapper.CreateMap<OrderDetailsViewModel, OrderDetails>();

            Mapper.CreateMap<OrderDetails, CreateOrderDetailsViewModel>();
            Mapper.CreateMap<CreateOrderDetailsViewModel, OrderDetails>();

            Mapper.CreateMap<OrderDetails, EditOrderDetailsViewModel>();
            Mapper.CreateMap<EditOrderViewModel, OrderDetails>();

            #endregion

            #region Storage

            Mapper.CreateMap<Storage, StorageViewModel>();
            Mapper.CreateMap<StorageViewModel, Storage>();

            Mapper.CreateMap<Storage, CreateStorageViewModel>();
            Mapper.CreateMap<CreateStorageViewModel, Storage>();

            Mapper.CreateMap<Storage, EditStorageViewModel>();
            Mapper.CreateMap<EditStorageViewModel, Storage>();

            #endregion

            #region MedicamentPriceHistory

            Mapper.CreateMap<MedicamentPriceHistory, MedicamentPriceHistoryViewModel>();

            #endregion
        }
    }
}