using System;
using System.Linq;
using Pharmacy.BusinessLogic.Managers;
using Pharmacy.BusinessLogic.Validators;
using Pharmacy.Contracts.Managers;
using Pharmacy.Contracts.Repositories;
using Pharmacy.Core;
using Pharmacy.Data;

namespace Pharmacy.App.Views
{
    public class OrderDetailsView : BaseView
    {
        private readonly IEntityManager<OrderDetails> _orderDetailsManager;
        private readonly IEntityManager<Storage> _storageManager; 

        private readonly IRepository<OrderDetails> _repository; 

        public OrderDetailsView(DataContext context)
            : base(context)
        {
            _repository = new Repository<OrderDetails>(Context);
            
            var medicamentRepository = new Repository<Medicament>(Context);
            var orderRepository = new Repository<Order>(Context);
            _orderDetailsManager = new EntityManager<OrderDetails>(_repository,
                new OrderDetailsValidator(_repository, orderRepository, medicamentRepository));

            var storageRepository = new Repository<Storage>(Context);
            var pharmacyRepository = new Repository<Core.Pharmacy>(Context);
            _storageManager = new EntityManager<Storage>(storageRepository,
                new StorageValidator(storageRepository, pharmacyRepository, medicamentRepository));
        }

        #region CRUD
        public override void Add()
        {
            try
            {
                Console.WriteLine("Create orderDetail :");

                var order = GetEntity<Order>();
                if (order == null)
                    return;
                
                var medicament = GetEntity<Medicament>();
                if (medicament == null)
                    return;

                var quantity = EnterProperty<int>("quantity");

                var orderDetail= new OrderDetails()
                {
                    Order = order,
                    Medicament = medicament,
                    Quantity = quantity,
                    Price = medicament.Price 
                };

                if (_storageManager.GetByPrimaryKey(order.PharmacyId, medicament.Id) == null)
                {
                    _storageManager.Add(new Storage()
                    {
                        Medicament = medicament,
                        Pharmacy = order.Pharmacy,
                        Quantity = 0
                    });
                }

                if (order.Type == OperationType.Sale)
                {
                    if (_storageManager.GetByPrimaryKey(order.PharmacyId, medicament.Id).Quantity
                        < orderDetail.Quantity)
                    {
                        return;
                    }

                    _storageManager.GetByPrimaryKey(order.PharmacyId, medicament.Id).Quantity -=
                        orderDetail.Quantity;
                }
                else
                {
                    _storageManager.GetByPrimaryKey(order.PharmacyId, medicament.Id).Quantity +=
                        orderDetail.Quantity;
                }

                _orderDetailsManager.Add(orderDetail);
            }
            catch(Exception)
            {
                Console.WriteLine("Invalid input!");
            }
        }

        public override void Edit()
        {
            var orderDetail = GetEntity<OrderDetails>();

            if (orderDetail == null)
                return;

            try
            {
                Console.WriteLine("Edit orderDetail :");

                var order = GetEntity<Order>();
                if (order == null)
                    return;

                var medicament = GetEntity<Medicament>();
                if (medicament == null)
                    return;

                var quantity = EnterProperty<int>("quantity");

                orderDetail.Order = order;
                orderDetail.Medicament = medicament;
                orderDetail.Quantity = quantity;
                orderDetail.Price = medicament.Price;

                if (_storageManager.GetByPrimaryKey(order.PharmacyId, medicament.Id) == null)
                {
                    _storageManager.Add(new Storage()
                    {
                        Medicament = medicament,
                        Pharmacy = order.Pharmacy,
                        Quantity = 0
                    });
                }

                _orderDetailsManager.Update(orderDetail);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input!");
            }
        }

        public override void Remove()
        {
            var orderDetail = GetEntity<OrderDetails>();

            if (orderDetail == null)
                return;

            _orderDetailsManager.Remove(orderDetail.OrderId, orderDetail.MedicamentId);
        }

        public override void GetByPrimaryKey()
        {
            try
            {
                var id = EnterProperty<int>("orderId");

                var orderDetailses = _repository.Find(s => s.OrderId == id).ToList();
                if (orderDetailses.Count == 0)
                    return;

                foreach (var item in orderDetailses)
                    Console.WriteLine(item);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value!");
            }
        }

        public override void GetAll()
        {
            foreach (var orderDetails in _orderDetailsManager.GetAll())
            {
                Console.WriteLine(orderDetails);
            }
        }
        #endregion CRUD
    }
}
