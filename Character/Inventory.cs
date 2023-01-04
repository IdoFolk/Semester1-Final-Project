using ConsoleDungeonCrawler.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
                    case ConsoleKey.I:
                        Close();
                        break;
                    default:
                        switch (MenuIndicator)
                        {
                            case 0:
                                SubMenuWeaponsNav(player, key);
                                break;
                            case 1:
                                SubMenuPotionsNav(player, key);
                                break;
                            case 2:
                                SubMenuArmorNav(player, key);
                                break;
                        }
                        break;
                }
                if (MenuIndicator < 0) MenuIndicator = 0;
                if (MenuIndicator > 2) MenuIndicator = 2;
            }
        }
        public static void SubMenuWeaponsNav(Player player, ConsoleKey key)
        {
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        SubMenuIndicator--;
                        break;
                    case ConsoleKey.RightArrow:
                        SubMenuIndicator++;
                        break;
                    case ConsoleKey.Enter:
                        player.EquipWeapon(player.PlayerWeapons[SubMenuIndicator]);
                        Close();
                        break;
                    case ConsoleKey.Delete:
                        player.DropWeapon(player.PlayerWeapons[SubMenuIndicator]);
                        HUD.ClearInventory();
                        Close();
                        break;
                    case ConsoleKey.I:
                        Close();
                        break;
                }
                if (SubMenuIndicator < 1) SubMenuIndicator = 1;
                if (SubMenuIndicator > player.PlayerWeapons.Count-1) SubMenuIndicator = player.PlayerWeapons.Count-1;
                HUD.WeaponNav(player);
        }
        public static void SubMenuPotionsNav(Player player, ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    SubMenuIndicator--;
                    break;
                case ConsoleKey.RightArrow:
                    SubMenuIndicator++;
                    break;
                case ConsoleKey.Enter:
                    player.UsePotion(player.PlayerPotions[SubMenuIndicator]);
                    Close();
                    break;
                case ConsoleKey.Delete:
                    player.DropPotion(player.PlayerPotions[SubMenuIndicator]);
                    HUD.ClearInventory();
                    Close();
                    break;
                case ConsoleKey.I:
                    Close();
                    break;
            }
            if (SubMenuIndicator < 0) SubMenuIndicator = 0;
            if (SubMenuIndicator > player.PlayerPotions.Count - 1) SubMenuIndicator = player.PlayerPotions.Count - 1;
            HUD.PotionNav(player);
        }
        public static void SubMenuArmorNav(Player player, ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    SubMenuIndicator--;
                    break;
                case ConsoleKey.RightArrow:
                    SubMenuIndicator++;
                    break;
                case ConsoleKey.Enter:
                    player.EquipArmor(player.PlayerArmors[SubMenuIndicator]);
                    Close();
                    break;
                case ConsoleKey.Delete:
                    player.DropArmor(player.PlayerArmors[SubMenuIndicator]);
                    HUD.ClearInventory();
                    Close();
                    break;
                case ConsoleKey.I:
                    Close();
                    break;
            }
            if (SubMenuIndicator < 1) SubMenuIndicator = 1;
            if (SubMenuIndicator > player.PlayerArmors.Count-1) SubMenuIndicator = player.PlayerArmors.Count-1;
            HUD.ArmorNav(player);
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
