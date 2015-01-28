using System;
using Pharmacy.BusinessLogic.Managers;
using Pharmacy.BusinessLogic.Validators;
using Pharmacy.Contracts.Managers;
using Pharmacy.Data;

namespace Pharmacy.App.Views
{
    public class PharmacyView : BaseView
    {
        private readonly IEntityManager<Core.Pharmacy> _manager;

        public PharmacyView(DataContext context)
            : base(context)
        {
            var repository = new Repository<Core.Pharmacy>(Context);

            _manager = new EntityManager<Core.Pharmacy>(repository,
                new PharmacyValidator(repository));
        }

        #region CRUD
        public override void Add()
        {
            try
            {
                Console.WriteLine("Create pharmacy :");

                var address = EnterProperty<string>("address");
                var number = EnterProperty<int>("number");
                var phone = EnterProperty<string>("phone");
                var openDate = EnterProperty<DateTime>("openDate");

                var pharmacy = new Core.Pharmacy()
                {
                    Address = address,
                    Number = number,
                    Phone = phone,
                    OpenDate = openDate
                };

                _manager.Add(pharmacy);
            }
            catch(Exception)
            {
                Console.WriteLine("Invalid input!");
            }
        }

        public override void Edit()
        {
            var pharmacy = GetEntity<Core.Pharmacy>();

            if (pharmacy == null)
                return;

            try
            {
                Console.WriteLine("Edit pharmacy :");

                var address = EnterProperty<string>("address");
                var number = EnterProperty<int>("number");
                var phone = EnterProperty<string>("phone");
                var openDate = EnterProperty<DateTime>("openDate");

                pharmacy.Address = address;
                pharmacy.Number = number;
                pharmacy.Phone = phone;
                pharmacy.OpenDate = openDate;

                _manager.Update(pharmacy);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input!");
            }
        }

        public override void Remove()
        {
            var pharmacy = GetEntity<Core.Pharmacy>();
            
            if (pharmacy == null)
                return;

            _manager.Remove(pharmacy.Id);
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
            foreach (var pharmacy in _manager.GetAll())
            {
                Console.WriteLine(pharmacy);
            }
        }
        #endregion CRUD
    }
}
