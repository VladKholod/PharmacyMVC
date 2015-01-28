namespace Pharmacy.Contracts.Views
{
    public interface IView
    {
        void Add();
        void Edit();
        void Remove();
        void GetByPrimaryKey();
        void GetAll();
    }
}
