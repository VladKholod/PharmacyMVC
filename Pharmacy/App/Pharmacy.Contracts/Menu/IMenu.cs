using System.Collections.Generic;

namespace Pharmacy.Contracts.Menu
{
    public interface IMenu
    {
        IList<string> MenuItems { get; }
    }
}
