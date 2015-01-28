using System;
using System.Collections.Generic;

namespace Pharmacy.App.AppConsole
{
    public sealed class UIConsole
    {
        private static UIConsole _instance;

        public static UIConsole Instance
        {
            get { return _instance ?? new UIConsole(); }
        }

        private UIConsole()
        {
        }

        public int SelectMenuItem(IList<string> menuItems)
        {
            Console.CursorVisible = false;
            var currentMenuItem = 0;
            while (true)
            {
                DisplayMenu(menuItems, currentMenuItem);

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (currentMenuItem > 0)
                        currentMenuItem--;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (currentMenuItem < menuItems.Count - 1)
                        currentMenuItem++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    return currentMenuItem;
                }
            }
        }

        private void DisplayMenu(IList<string> menuItems, int currentMenuItem)
        {
            Console.Clear();
            for (var i = 0; i < menuItems.Count; i++)
                Console.WriteLine("{0} {1}", i == currentMenuItem ? "    >\t" : "\t", menuItems[i]);

            DisplayHelp();
        }

        public void DisplayHelp()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            Console.WriteLine("Press <Enter> to select menu item.");
        }
    }
}
