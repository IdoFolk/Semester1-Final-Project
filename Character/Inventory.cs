using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Character
{
    class Inventory
    {
        Player _player;
        public int MenuIndicator { get; private set; } = 1;
        public int SubMenuIndicator { get; private set; } = 1;
        public Inventory(Player player)
        {
            _player = player;
        }
        public void MenuNav()
        {
            
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    MenuIndicator--;
                    break;
                case ConsoleKey.DownArrow:
                    MenuIndicator++;
                    break;
                case ConsoleKey.Enter:
                    switch (MenuIndicator)
                    {
                        case 1:
                            SubMenuWeaponsNav(key);
                            break;
                        case 2:
                            SubMenuPotionsNav(key);
                            break;
                        case 3:
                            SubMenuArmorNav(key);
                            break;
                    }
                    break;
            }
            if (MenuIndicator < 1) MenuIndicator = 1;
            if (MenuIndicator > 3) MenuIndicator = 3;
        }
        public void SubMenuWeaponsNav(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.RightArrow:
                    SubMenuIndicator++;
                    break;
                case ConsoleKey.LeftArrow:
                    SubMenuIndicator--;
                    break;
                case ConsoleKey.Enter:
                    switch (SubMenuIndicator)
                    {
                        case 1:

                            break;
                        case 2:
                            break;
                    }
                    break;
            }
            if (SubMenuIndicator < 1) SubMenuIndicator = 1;
            if (SubMenuIndicator > _player.PlayerWeapons.Count) SubMenuIndicator = _player.PlayerWeapons.Count;
        }
        public void SubMenuPotionsNav(ConsoleKey key)
        {

        }
        public void SubMenuArmorNav(ConsoleKey key)
        {

        }
    }
}
