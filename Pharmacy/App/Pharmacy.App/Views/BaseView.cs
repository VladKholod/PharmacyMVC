using System;
using System.ComponentModel;
using System.Linq;
using Pharmacy.App.AppConsole;
using Pharmacy.Contracts.Views;
using Pharmacy.Core;
using Pharmacy.Data;

namespace Pharmacy.App.Views
{
    public abstract class BaseView : IView
    {
        private readonly UIConsole _uiConsole;

        protected readonly DataContext Context;

        protected BaseView(DataContext context)
        {
            _uiConsole = UIConsole.Instance;
            Context = context;
        }

        protected T GetEntity<T>() where T : class, IDbEntity
        {
            Console.Clear();
            var repository = new Repository<T>(Context);
            var entities = repository.GetAll().ToList();

            var entityStringList = entities.Select(item => item.ToString()).ToList();

            var selectedIndex = _uiConsole.SelectMenuItem(entityStringList);
            return entities.Count == 0 ? null : entities[selectedIndex];
        }

        protected T EnterProperty<T>(string propertyName)
        {
            Console.Clear();
            Console.Write("{0} : ", propertyName);
            var input = Console.ReadLine();

            _uiConsole.DisplayHelp();
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                return (T)converter.ConvertFromString(input);
            }
            return default(T);
        }

        #region IView
        public abstract void Add();

        public abstract void Edit();

        public abstract void Remove();

        public abstract void GetByPrimaryKey();

        public abstract void GetAll();
        #endregion
    }
}
