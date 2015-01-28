using System.Collections.Generic;
using Pharmacy.Contracts.Menu;

namespace Pharmacy.App.Menu
{
    public class SubMenu : IMenu
    {
        private readonly IList<string> _menuItems;

        public IList<string> MenuItems
        {
            get { return _menuItems; }
        }

        public SubMenu(IList<string> menuItems)
        {
            _menuItems = menuItems;
        }
    }
}
