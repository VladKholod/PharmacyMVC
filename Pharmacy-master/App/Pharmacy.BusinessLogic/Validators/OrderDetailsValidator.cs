using Pharmacy.Contracts.Repositories;
using Pharmacy.Contracts.Validations;
using Pharmacy.Core;

namespace Pharmacy.BusinessLogic.Validators
{
    public class OrderDetailsValidator : IValidator<OrderDetails>
    {
        private readonly IRepository<OrderDetails> _orderDetailsRepository; 
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Medicament> _medicamentRepository;

        public OrderDetailsValidator(IRepository<OrderDetails> orderDetailsRepository, 
            IRepository<Order> orderRepository,
            IRepository<Medicament> medicamentRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
            _orderRepository = orderRepository;
            _medicamentRepository = medicamentRepository;
        }

        public bool IsValidEntity(OrderDetails entity)
        {
            return (_orderRepository.GetByPrimaryKey(entity.OrderId) != null
                    || entity.Order != null)
                   && (_medicamentRepository.GetByPrimaryKey(entity.MedicamentId) != null
                       || entity.Medicament != null)
                   && entity.Price > 0
                   && entity.Quantity > 0;
        }

        public bool IsEntityExist(params object[] keys)
        {
            if (keys.Length != 2)
                return false;

            return _orderDetailsRepository.GetByPrimaryKey(keys) != null;
        }
    }
}
