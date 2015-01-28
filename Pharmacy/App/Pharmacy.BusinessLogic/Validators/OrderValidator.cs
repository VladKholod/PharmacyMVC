using Pharmacy.Contracts.Repositories;
using Pharmacy.Contracts.Validations;
using Pharmacy.Core;

namespace Pharmacy.BusinessLogic.Validators
{
    public class OrderValidator : IValidator<Order>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Core.Pharmacy> _pharmacyRepository;

        public OrderValidator(IRepository<Order> orderRepository, IRepository<Core.Pharmacy> pharmacyRepository)
        {
            _orderRepository = orderRepository;
            _pharmacyRepository = pharmacyRepository;
        }

        public bool IsValidEntity(Order entity)
        {
            return _pharmacyRepository.GetByPrimaryKey(entity.PharmacyId) != null
                   || entity.Pharmacy != null;
        }

        public bool IsEntityExist(params object[] keys)
        {
            if (keys.Length != 1)
                return false;

            return _orderRepository.GetByPrimaryKey(keys) != null;
        }
    }
}
