using Pharmacy.Core;

namespace Pharmacy.Contracts.Validations
{
    public interface IValidator<in T> where T : class, IDbEntity
    {
        bool IsValidEntity(T entity);
        bool IsEntityExist(params object[] keys);
    }
}
