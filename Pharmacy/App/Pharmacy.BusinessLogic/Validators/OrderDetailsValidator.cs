using Pharmacy.Contracts.Repositories;
using Pharmacy.Contracts.Validations;
using Pharmacy.Core;

namespace Pharmacy.BusinessLogic.Validators
{
    public class OrderDetailsValidator : IValidator<OrderDetails>
    {
        private readonly IRepository<OrderDetails> _orderDetailsRepository; 

        public OrderDetailsValidator(IRepository<OrderDetails> orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public bool IsValidEntity(OrderDetails entity)
        {
            return !IsEntityExist(entity.OrderId, entity.MedicamentId)
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
