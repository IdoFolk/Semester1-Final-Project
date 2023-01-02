using ConsoleDungeonCrawler.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Character
{
    static class Inventory
    {
        public static int MenuIndicator { get; private set; }
        public static int SubMenuIndicator { get; private set; }
        public static bool InventoryOpened { get; private set; } 
        public static void MenuNav(Player player)
        {
            Open();
            while (InventoryOpened)
            {
                HUD.InventoryNav();
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
                            case 0:
                                SubMenuWeaponsNav(player);
                                break;
                            case 1:
                                SubMenuPotionsNav(key);
                                break;
                            case 2:
                                SubMenuArmorNav(key);
                                break;
                        }
                        break;
                    case ConsoleKey.I:
                        Close();
                        break;
                }
                if (MenuIndicator < 0) MenuIndicator = 0;
                if (MenuIndicator > 2) MenuIndicator = 2;
            }
        }
        public static void SubMenuWeaponsNav(Player player)
        {
            while (InventoryOpened)
            {
                HUD.WeaponNav(player);
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        SubMenuIndicator++;
                        break;
                    case ConsoleKey.UpArrow:
                        SubMenuIndicator--;
                        break;
                    case ConsoleKey.Enter:
                        player.EquipWeapon(player.PlayerWeapons[SubMenuIndicator]);
                        return;
                    case ConsoleKey.I:
                        Close();
                        break;
                }
                if (SubMenuIndicator < 0) SubMenuIndicator = 0;
                if (SubMenuIndicator > player.PlayerWeapons.Count-1) SubMenuIndicator = player.PlayerWeapons.Count-1;
            }
        }
        public static void SubMenuPotionsNav(ConsoleKey key)
        {

        }
        public static void SubMenuArmorNav(ConsoleKey key)
        {

        }
        private static void Open()
        {
            InventoryOpened = true;
        }
        private static void Close()
        {
            InventoryOpened = false;
        }
    }
}
