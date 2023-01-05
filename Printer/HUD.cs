using ConsoleDungeonCrawler.Character;
using ConsoleDungeonCrawler.Level_Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Printer
{
    static class HUD
    {
        private const ConsoleColor DefaultForeground = ConsoleColor.Gray;
        private const ConsoleColor DefaultBackground = ConsoleColor.Black;
        private static int _logIndicator;
        public static void GameState(Level level, Player player)
        {
            Map.PrintMap(level, player);
            LevelStats(level);
            PlayerStats(player);
            PrintInventory(player, level);
        }
        public static void Log()
        {
            if (_logIndicator > UI.LogBox.Height - 4) LogReset();
            Console.SetCursorPosition(UI.LogBox.PosX + 1, UI.LogBox.PosY + 3 + _logIndicator);
            _logIndicator++;
        }
        public static void LogReset()
        {
            for (int i = 0; i < UI.LogBox.Height - 3; i++)
            {
                for (int j = 0; j < UI.LogBox.Width - 1; j++)
                {
                    Console.SetCursorPosition(UI.LogBox.PosX + j + 1, UI.LogBox.PosY + i + 3);
                    Console.Write(' ');
                }
            }
            _logIndicator = 0;
        }
        public static void CombatLog(Player player, Enemy enemy, int attack)
        {
            if (attack == 0)
            {
                Log();
                Console.Write($"{player.Name} Misses...");
                return;

            }
            Console.ForegroundColor = ConsoleColor.Red;
            Log();
            Console.Write($"{player.Name} attacks {enemy.Name} for {attack} damage.");
            Console.ForegroundColor = DefaultForeground;

        }
        public static void CombatLog(Enemy enemy, Player player, int attack)
        {
            if (attack == 0)
            {
                Log();
                Console.Write($"{enemy.Name} Misses...");
                Log();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Log();
            Console.Write($"{enemy.Name} attacks {player.Name} for {attack} damage.");
            Console.ForegroundColor = DefaultForeground;
            Log();

        }
        public static void OpenDoorLog(Door door)
        {
            Log();
            Console.ForegroundColor = door.Color;
            Console.Write("You have opened a door");
            Console.ForegroundColor = DefaultForeground;
            Log();
        }
        public static void GotKeyLog(Key key)
        {
            Log();
            Console.ForegroundColor = key.Color;
            Console.Write("You got a key");
            Console.ForegroundColor = DefaultForeground;
            Log();
        }
        public static void SteppedOnTrapLog(Trap trap)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Log();
            Console.Write("You stepped on a trap and");
            Log();
            Console.Write($"received {trap.Damage} damage");
            Log();
            Console.ForegroundColor = DefaultForeground;
        }
        public static void GotCoinLog(int amount)
        {
            Log();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"You got {amount} Clover!");
            Console.ForegroundColor = DefaultForeground;
            Log();
        }
        public static void OpenChestLog()
        {
            Log();
            Console.Write("You have opened a chest and got:");
        }
        public static void GotWeaponLog(Weapon weapon)
        {
            Log();
            Console.Write($"A {weapon.Name}!");
            Log();

        }
        public static void GotPotionLog(Potion potion)
        {
            Log();
            Console.ForegroundColor = potion.Color;
            Console.Write($"A {potion.Name} Potion!");
            Console.ForegroundColor = DefaultForeground;
            Log();

        }
        public static void GotArmorLog(Armor armor)
        {
            Log();
            Console.Write($"A {armor.Name}!");
            Log();
        }
        public static void WeaponBreakLog(Weapon weapon)
        {
            Log();
            Console.Write($"{weapon.Name} broke...");
            Log();
        }
        public static void DropWeaponLog(Weapon weapon)
        {
            Log();
            Console.Write($"{weapon.Name} Dropped...");
            Log();
        }
        public static void ArmorBreakLog(Armor armor)
        {
            Log();
            if (armor.Name == "Plate Armor") Console.Write($"{armor.Name} Broke...");
            else Console.Write($"{armor.Name} Ripped...");
            Log();
        }
        public static void DropArmorLog(Armor armor)
        {
            Log();
            Console.Write($"{armor.Name} Dropped...");
            Log();
        }
        public static void DropPotionLog(Potion potion)
        {
            Log();
            Console.Write($"{potion.Name} Potion Dropped...");
            Log();
        }
        public static void ItemCappedLog(ItemType itemType)
        {
            Log();
            switch (itemType)
            {
                case ItemType.Weapon:
                    Console.Write("Weapon ");
                    break;
                case ItemType.Potion:
                    Console.Write("Potion ");
                    break;
                case ItemType.Armor:
                    Console.Write("Armor ");
                    break;
                case ItemType.Coin:
                    break;
            }
            Console.Write("Limit Reached...");
            Log();
        }
        public static void LevelStats(Level level)
        {
            Console.SetCursorPosition(UI.LevelBox.PosX + 1, UI.LevelBox.PosY + 3);
            Console.WriteLine($"Level: {level.Number}");
            Console.SetCursorPosition(UI.LevelBox.PosX + 1, UI.LevelBox.PosY + 4);
            Console.WriteLine($"Enemies Killed: {level.EnemiesKilled}");
        }
        public static void PlayerStats(Player player)
        {
            //HP
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 3);
            Console.Write("HP: ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("♥♥♥♥♥♥♥♥♥♥");
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 5, UI.PlayerStatBox.PosY + 3);
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < player.CurrentHP; i++)
            {
                Console.Write('♥');
            }
            Console.ForegroundColor = DefaultForeground;
            //Shield
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 4);
            Console.Write($"Shield: ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("♦♦♦♦♦");
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 8, UI.PlayerStatBox.PosY + 4);
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < player.EquippedArmor.ArmorPoints; i++)
            {
                Console.Write('♦');
            }
            Console.ForegroundColor = DefaultForeground;

            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(UI.PlayerStatBox.PosX + 13, UI.PlayerStatBox.PosY + 5 + i);
                Console.Write("|");
            }
            for (int i = 0; i < UI.PlayerStatBox.Width-1; i++)
            {
                Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1 + i, UI.PlayerStatBox.PosY + 5);
                Console.Write("─");
            }
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 13, UI.PlayerStatBox.PosY + 5);
            Console.Write("┬");
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 13, UI.PlayerStatBox.PosY + 10);
            Console.Write("┴");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 4, UI.PlayerStatBox.PosY + 5);
            Console.Write("Weapon");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 17, UI.PlayerStatBox.PosY + 5);
            Console.Write("Armor");
            
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 4, UI.PlayerStatBox.PosY + 6);
            Console.Write($"{player.EquippedWeapon.Name}");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 7);
            Console.Write($"Damage: {player.EquippedWeapon.Damage}");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 8);
            Console.Write($"Hit: {(player.EquippedWeapon.HitChance * 10) * 10}%");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 9);
            Console.Write($"Status: {player.EquippedWeapon.Durability * 20}%");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 15, UI.PlayerStatBox.PosY + 6);
            Console.Write($"{player.EquippedArmor.Name}");
            
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 14, UI.PlayerStatBox.PosY + 8);
            Console.Write($"Evasion: {player.EquippedArmor.Evasion*100}%");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 14, UI.PlayerStatBox.PosY + 9);
            Console.Write($"Status: {player.EquippedArmor.ArmorPoints * 20}%");


        }
        public static void EnemyStats(Enemy enemy)
        {


            Console.SetCursorPosition(UI.EnemyStatBox.PosX + 1, UI.EnemyStatBox.PosY + 3);
            Console.Write($"{enemy.Name} HP: ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("♥♥♥♥♥♥♥♥♥♥");
            Console.SetCursorPosition(UI.EnemyStatBox.PosX + 5 + enemy.Name.Length, UI.EnemyStatBox.PosY + 3);
            Console.ForegroundColor = ConsoleColor.Red;
            switch (enemy.CurrentHP)
            {
                case 1:
                    for (int i = 0; i < 1; i++)
                    {
                        Console.Write('♥');
                    }
                    break;
                case 2:
                    for (int i = 0; i < 2; i++)
                    {
                        Console.Write('♥');
                    }
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write('♥');
                    }
                    break;
                case 4:
                    for (int i = 0; i < 4; i++)
                    {
                        Console.Write('♥');
                    }
                    break;
                case 5:
                    for (int i = 0; i < 5; i++)
                    {
                        Console.Write('♥');
                    }
                    break;
                case 6:
                    for (int i = 0; i < 6; i++)
                    {
                        Console.Write('♥');
                    }
                    break;
                case 7:
                    for (int i = 0; i < 7; i++)
                    {
                        Console.Write('♥');
                    }
                    break;
                case 8:
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write('♥');
                    }
                    break;
                case 9:
                    for (int i = 0; i < 9; i++)
                    {
                        Console.Write('♥');
                    }
                    break;
                case 10:
                    for (int i = 0; i < 10; i++)
                    {
                        Console.Write('♥');
                    }
                    break;
            }
            Console.ForegroundColor = DefaultForeground;

        }
        public static void PrintInventory(Player player, Level level)
        {
            InventoryMenu();
            OwnedWeapons(player);
            OwnedPotions(player);
            OwnedArmor(player);
            OwnedKeys(level);
            OwnedCoins(player);
        }
        public static void InventoryMenu()
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 1);
            Console.Write("Inventory:                  ");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 3);
            Console.Write("WEAPONS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 8);
            Console.Write("POTIONS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 11);
            Console.Write("ARMOR:");
        }
        public static void OwnedWeapons(Player player)
        {
            int w = 1;
            for (int j = 0; j < 15; j+=14)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (w > player.PlayerWeapons.Count-1) return;
                    Weapon weapon = player.PlayerWeapons[w];
                    if (weapon.IsEquipped)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.SetCursorPosition(UI.InventoryBox.PosX + 1 + j, UI.InventoryBox.PosY + 4 + i);
                    Console.Write($"{w}. {weapon.Name}");
                    Console.BackgroundColor = DefaultBackground;
                    Console.ForegroundColor = DefaultForeground;
                    w++;
                }
            }
        }
        public static void OwnedPotions(Player player)
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 9);
            Console.WriteLine("[                          ]");
            int i = 4;
            foreach (Potion potion in player.PlayerPotions)
            {
                Console.SetCursorPosition(UI.InventoryBox.PosX + i, UI.InventoryBox.PosY + 9);
                Console.ForegroundColor = potion.Color;
                Console.Write("♠");
                Console.ForegroundColor = DefaultForeground;
                i += 3;
            }
        }
        public static void OwnedArmor(Player player)
        {
            for (int i = 1; i < player.PlayerArmors.Count; i++)
            {
                Armor armor = player.PlayerArmors[i];
                if (armor.IsEquipped)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 11 + i);
                Console.Write($"{i}. {armor.Name}");
                Console.BackgroundColor = DefaultBackground;
                Console.ForegroundColor = DefaultForeground;
            }
        }
        public static void OwnedKeys(Level level)
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX+1, UI.InventoryBox.PosY+16);
            Console.Write("KEYS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX+1, UI.InventoryBox.PosY+17);
            Console.WriteLine("[                          ]");
            int j = 4;  
            foreach (Key key in level.PlayerKeys)
            {
                Console.SetCursorPosition(UI.InventoryBox.PosX + j, UI.InventoryBox.PosY + 17);
                if (key.Used)
                {
                    Console.ForegroundColor = key.UsedColor;
                    Console.Write("¶");
                    Console.BackgroundColor = DefaultBackground;
                }
                else
                {
                    Console.ForegroundColor = key.Color;
                    Console.Write("¶");
                }
                j += 3;
            }
            
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;
        }
        public static void OwnedCoins(Player player)
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 19);
            Console.Write("CLOVERS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 20);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("♣");
            Console.ForegroundColor = DefaultForeground;
            Console.Write($"x{player.Coins}");
        }
        public static void InventoryNav()
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX + 20, UI.InventoryBox.PosY + 1);
            Console.Write("Opened");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 3);
            Console.Write("WEAPONS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 8);
            Console.Write("POTIONS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 11);
            Console.Write("ARMOR:");
            switch (Inventory.MenuIndicator)
            {
                case 0:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 3);
                    Console.Write("WEAPONS:"); 
                    Console.BackgroundColor = DefaultBackground;
                    Console.ForegroundColor = DefaultForeground;
                    break;
                case 1:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 8);
                    Console.Write("POTIONS:");
                    Console.BackgroundColor = DefaultBackground;
                    Console.ForegroundColor = DefaultForeground;
                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 11);
                    Console.Write("ARMOR:");
                    Console.BackgroundColor = DefaultBackground;
                    Console.ForegroundColor = DefaultForeground;
                    break;
            }
        }
        public static void WeaponNav(Player player)
        {
            int w = 1;
            for (int j = 0; j < 15; j += 14)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (w > player.PlayerWeapons.Count - 1) return;
                    Weapon weapon = player.PlayerWeapons[w];
                    if (Inventory.SubMenuIndicator == w)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.SetCursorPosition(UI.InventoryBox.PosX + 1 + j, UI.InventoryBox.PosY + 4 + i);
                    Console.Write($"{w}. {weapon.Name}");
                    Console.BackgroundColor = DefaultBackground;
                    Console.ForegroundColor = DefaultForeground;
                    w++;
                }
            }
        }
        public static void PotionNav(Player player)
        {
            int i = 0;
            int j = 4;
            foreach (Potion potion in player.PlayerPotions)
            {
                if (Inventory.SubMenuIndicator == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = potion.Color;
                }
                Console.SetCursorPosition(UI.InventoryBox.PosX + j, UI.InventoryBox.PosY + 9);
                Console.ForegroundColor = potion.Color;
                Console.Write("♠");
                Console.BackgroundColor = DefaultBackground;
                i ++;
                j += 3;
            }
                Console.ForegroundColor = DefaultForeground;
        }
        public static void ArmorNav(Player player)
        {
            for (int i = 1; i < player.PlayerArmors.Count; i++)
            {
                Armor armor = player.PlayerArmors[i];
                if (Inventory.SubMenuIndicator == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 11 + i);
                Console.Write($"{i}. {armor.Name}");
                Console.BackgroundColor = DefaultBackground;
                Console.ForegroundColor = DefaultForeground;
            }
        }
        public static void ClearInventory()
        {
            //Inventory Weapons Clear
            HUD.Clear(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 4, UI.InventoryBox.Width - 1, 3);
            //Inventory Armor Clear
            HUD.Clear(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 12, UI.InventoryBox.Width - 1, 3);
        }
        public static void ClearPlayerStats()
        {
            HUD.Clear(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 5, UI.PlayerStatBox.Width - 1, 5);
        }
        public static void Clear(int posX, int posY, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.SetCursorPosition(posX + j, posY + i);
                    Console.Write(" ");
                }
            }
        }

    }
}
