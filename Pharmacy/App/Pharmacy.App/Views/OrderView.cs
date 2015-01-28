using System;
using Pharmacy.BusinessLogic.Managers;
using Pharmacy.BusinessLogic.Validators;
using Pharmacy.Contracts.Managers;
using Pharmacy.Core;
using Pharmacy.Data;

namespace Pharmacy.App.Views
{
    public class OrderView : BaseView
    {
        private readonly IEntityManager<Order> _manager;

        public OrderView(DataContext context)
            : base(context)
        {
            var pharmacyRepository = new Repository<Core.Pharmacy>(Context);

            var repository = new Repository<Order>(Context);
            _manager = new EntityManager<Order>(repository,
                new OrderValidator(repository, pharmacyRepository));
        }

        #region CRUD
        public override void Add()
        {
            try
            {
                Console.WriteLine("Create order :");
                var date = EnterProperty<DateTime>("date");
                Console.Write("type(1 or 2) : ");
                var t = Console.ReadKey().KeyChar;
                if(t!='1' && t!='2')
                    return;

                OperationType type;
                Enum.TryParse(t.ToString(), out type);

                var pharmacy = GetEntity<Core.Pharmacy>();
                if (pharmacy == null)
                    return;

                var order = new Order()
                {
                    Date = date,
                    Type = type,
                    Pharmacy = pharmacy
                };

                _manager.Add(order);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input");
            }
        }

        public override void Edit()
        {
            var order = GetEntity<Order>();

            if (order == null)
                return;

            try
            {
                Console.WriteLine("Edit order :");
                var date = EnterProperty<DateTime>("date");
                Console.Write("type(1 or 2) : ");
                var t = Console.ReadKey().KeyChar;
                if (t != '1' && t != '2')
                    return;

                OperationType type;
                Enum.TryParse(t.ToString(), out type);

                var pharmacy = GetEntity<Core.Pharmacy>();
                if (pharmacy == null)
                    return;

                order.Date = date;
                order.Type = type;
                order.Pharmacy = pharmacy;

                _manager.Update(order);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input!");
            }
        }

        public override void Remove()
        {
            var order = GetEntity<Order>();
            if (order != null)
                _manager.Remove(order.Id);
        }

        public override void GetByPrimaryKey()
        {
            try
            {
                var id = EnterProperty<int>("id");
                Console.WriteLine(_manager.GetByPrimaryKey(id));
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value!");
            }
        }

        public override void GetAll()
        {
            foreach (var order in _manager.GetAll())
            {
                Console.WriteLine(order);
            }
        }
        #endregion CRUD
    }
}
