using ConsoleDungeonCrawler.Level_Elements;
using ConsoleDungeonCrawler.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Character
{
    static class Inventory
    {
        public static int MenuIndicator { get; private set; }
        public static int SubMenuIndicator { get; private set; }
        public static bool IsOpen { get; private set; } 
        public static void MenuNav(Player player, Level level)
        {
            Open();
            while (IsOpen)
            {
                if (player.IsDead()) return;
                HUD.InventoryNav();
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        Sounds.PlaySFX(Sounds.MenuNav1SFX);
                        MenuIndicator--;
                        NoPause(level, player);
                        break;
                    case ConsoleKey.DownArrow:
                        Sounds.PlaySFX(Sounds.MenuNav1SFX);
                        MenuIndicator++;
                        NoPause(level, player);
                        break;
                    case ConsoleKey.I:
                        Sounds.PlaySFX(Sounds.CloseInventorySFX);
                        Close();
                        break;
                    default:
                        switch (MenuIndicator)
                        {
                            case 0:
                                SubMenuWeaponsNav(player, level, key);
                                break;
                            case 1:
                                SubMenuPotionsNav(player, level, key);
                                break;
                            case 2:
                                SubMenuArmorNav(player, level, key);
                                break;
                        }
                        break;
                }
                if (MenuIndicator < 0) MenuIndicator = 0;
                if (MenuIndicator > 2) MenuIndicator = 2;
            }
        }
        public static void SubMenuWeaponsNav(Player player, Level level, ConsoleKey key)
        {
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                    Sounds.PlaySFX(Sounds.MenuNav2SFX);
                    SubMenuIndicator--;
                        NoPause(level, player);
                        break;
                    case ConsoleKey.RightArrow:
                    Sounds.PlaySFX(Sounds.MenuNav2SFX);
                    SubMenuIndicator++;
                        NoPause(level, player);
                        break;
                    case ConsoleKey.Enter:
                    Sounds.PlaySFX(Sounds.EquipSFX);
                    player.EquipWeapon(player.PlayerWeapons[SubMenuIndicator]);
                        Close();
                        break;
                    case ConsoleKey.Delete:
                    Sounds.PlaySFX(Sounds.MenuNav3SFX);
                    player.DropWeapon(player.PlayerWeapons[SubMenuIndicator]);
                        HUD.ClearInventory();
                        Close();
                        break;
                    case ConsoleKey.I:
                    Sounds.PlaySFX(Sounds.CloseInventorySFX);
                    Close();
                        break;
                }
                if (SubMenuIndicator < 1) SubMenuIndicator = 1;
                if (SubMenuIndicator > player.PlayerWeapons.Count-1) SubMenuIndicator = player.PlayerWeapons.Count-1;
                HUD.WeaponNav(player);
        }
        public static void SubMenuPotionsNav(Player player, Level level, ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    Sounds.PlaySFX(Sounds.MenuNav2SFX);
                    SubMenuIndicator--;
                    NoPause(level, player);
                    break;
                case ConsoleKey.RightArrow:
                    Sounds.PlaySFX(Sounds.MenuNav2SFX);
                    SubMenuIndicator++;
                    NoPause(level, player);
                    break;
                case ConsoleKey.Enter:
                    player.UsePotion(player.PlayerPotions[SubMenuIndicator]);
                    Close();
                    break;
                case ConsoleKey.Delete:
                    Sounds.PlaySFX(Sounds.MenuNav3SFX);
                    player.DropPotion(player.PlayerPotions[SubMenuIndicator]);
                    HUD.ClearInventory();
                    Close();
                    break;
                case ConsoleKey.I:
                    Sounds.PlaySFX(Sounds.CloseInventorySFX);
                    Close();
                    break;
            }
            if (SubMenuIndicator < 0) SubMenuIndicator = 0;
            if (SubMenuIndicator > player.PlayerPotions.Count - 1) SubMenuIndicator = player.PlayerPotions.Count - 1;
            HUD.PotionNav(player);
        }
        public static void SubMenuArmorNav(Player player, Level level, ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    Sounds.PlaySFX(Sounds.MenuNav2SFX);
                    SubMenuIndicator--;
                    NoPause(level, player);
                    break;
                case ConsoleKey.RightArrow:
                    Sounds.PlaySFX(Sounds.MenuNav2SFX);
                    SubMenuIndicator++;
                    NoPause(level, player);
                    break;
                case ConsoleKey.Enter:
                    Sounds.PlaySFX(Sounds.EquipSFX);
                    player.EquipArmor(player.PlayerArmors[SubMenuIndicator]);
                    Close();
                    break;
                case ConsoleKey.Delete:
                    Sounds.PlaySFX(Sounds.MenuNav3SFX);
                    player.DropArmor(player.PlayerArmors[SubMenuIndicator]);
                    HUD.ClearInventory();
                    Close();
                    break;
                case ConsoleKey.I:
                    Sounds.PlaySFX(Sounds.CloseInventorySFX);
                    Close();
                    break;
            }
            if (SubMenuIndicator < 1) SubMenuIndicator = 1;
            if (SubMenuIndicator > player.PlayerArmors.Count-1) SubMenuIndicator = player.PlayerArmors.Count-1;
            HUD.ArmorNav(player);
        }
        private static void NoPause(Level level, Player player)
        {
            if (Game.NoPause)
            {
                Game.EnemiesActions(level, player);
                level.UpdateGrid();
                Map.PrintMap(level, player);
            }
        }
        private static void Open()
        {
            IsOpen = true;
        }
        private static void Close()
        {
            IsOpen = false;
        }
    }
}
