namespace Pharmacy.Core
{
    public class BaseEntity : IDbEntity
    {
        public int Id { get; private set; }
    }
}
