using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Pharmacy.App.AppConsole;
using Pharmacy.App.Menu;
using Pharmacy.App.Views;
using Pharmacy.Contracts.Menu;
using Pharmacy.Contracts.Views;
using Pharmacy.Core;
using Pharmacy.Data;

namespace Pharmacy.App
{
    public class Application
    {
        private readonly DataContext _context = new DataContext();
        private readonly UIConsole _uiConsole = UIConsole.Instance;

        private readonly IMenu _mainMenu;
        private readonly IMenu _subMenu;

        private IView _viewBehaviour;

        private string _selectedAction = string.Empty;
        private string _selectedEntity = string.Empty;

        public Application()
        {
            _mainMenu = new SubMenu(InitEntityList());
            _subMenu = new SubMenu(InitActionList());

            _viewBehaviour = new PharmacyView(_context);
        }

        public void Run()
        {
            do
            {
                var selectedMenuItem = _uiConsole.SelectMenuItem(_mainMenu.MenuItems);
                _selectedEntity = _mainMenu.MenuItems[selectedMenuItem];

                ChangeViewBehaviour();

                selectedMenuItem = _uiConsole.SelectMenuItem(_subMenu.MenuItems);
                _selectedAction = _subMenu.MenuItems[selectedMenuItem];

                PerformAction();
            } while (Console.ReadKey(false).Key != ConsoleKey.F12);
        }

        #region InitLists
        private List<string> InitEntityList()
        {
            var entities = Assembly.GetAssembly(typeof(BaseEntity)).GetTypes()
                .Where(type => type != typeof(BaseEntity)
                               && (type.BaseType == typeof(BaseEntity)
                                   || type.GetInterface(typeof(IDbEntity).Name) != null));

            return entities.Select(entity => entity.Name).ToList();
        }

        private List<string> InitActionList()
        {
            return new List<string> {"Edit", "Remove", "GetByPrimaryKey", "GetAll", "Add"};
        }
        #endregion InitLists

        private void ChangeViewBehaviour()
        {
            if(!_subMenu.MenuItems.Contains("Add"))
                _subMenu.MenuItems.Add("Add");

            if (_selectedEntity == "Medicament")
            {
                _viewBehaviour = new MedicamentView(_context);
            }
            else if (_selectedEntity == "MedicamentPriceHistory")
            {
                _viewBehaviour = new MedicamentPriceHistoryView(_context);

                if (_subMenu.MenuItems.Contains("Add"))
                    _subMenu.MenuItems.Remove("Add");
            }
            else if (_selectedEntity == "Order")
            {
                _viewBehaviour = new OrderView(_context);
            }
            else if (_selectedEntity == "OrderDetails")
            {
                _viewBehaviour = new OrderDetailsView(_context);
            }
            else if (_selectedEntity == "Pharmacy")
            {
                _viewBehaviour = new PharmacyView(_context);
            }
            else if (_selectedEntity == "Storage")
            {
                _viewBehaviour = new StorageView(_context);
            }

        }

        private void PerformAction()
        {
            Console.Clear();

            if (_selectedAction == "Add")
            {
                _viewBehaviour.Add();
            }
            else if (_selectedAction == "Edit")
            {
                _viewBehaviour.Edit();
            }
            else if (_selectedAction == "Remove")
            {
                _viewBehaviour.Remove();
            }
            else if (_selectedAction == "GetByPrimaryKey")
            {
                _viewBehaviour.GetByPrimaryKey();
            }
            else if (_selectedAction == "GetAll")
            {
                _viewBehaviour.GetAll();
            }
        }
    }
}
