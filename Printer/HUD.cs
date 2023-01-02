using ConsoleDungeonCrawler.Character;
using ConsoleDungeonCrawler.Level_Elements;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (_logIndicator > UI.LogBox.Height - 3) LogReset();
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
        public static void OpenChestLog()
        {
            Log();
            Console.Write("You have opened a chest and got ");
        }
        public static void GotWeaponLog(Weapon weapon)
        {
            Log();
            Console.Write($"A {weapon.Name}!");
            Log();

        }
        public static void WeaponBreakLog(Weapon weapon)
        {
            Log();
            Console.Write($"{weapon.Name} broke...");
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


            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 3);
            Console.Write($"{player.Name} HP: ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("♥♥♥♥♥♥♥♥♥♥");
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 5 + player.Name.Length, UI.PlayerStatBox.PosY + 3);
            Console.ForegroundColor = ConsoleColor.Red;
            switch (player.CurrentHP)
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
        }
        public static void InventoryMenu()
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 1);
            Console.Write("Inventory:                  ");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 3);
            Console.Write("WEAPONS: Damage, HitChance");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 8);
            Console.Write("POTIONS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 10);
            Console.Write("ARMOR:");
        }
        public static void OwnedWeapons(Player player)
        {
            Clear(UI.InventoryBox.PosX+1,UI.InventoryBox.PosY+4, UI.InventoryBox.Width-1,4);
            int w = 0;
            foreach (Weapon weapon in player.PlayerWeapons)
            {
                if (weapon.IsEquipped)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(UI.InventoryBox.PosX+1, UI.InventoryBox.PosY+4+w);
                Console.Write($"{weapon.Name}:");
                Console.BackgroundColor = DefaultBackground;
                Console.ForegroundColor = DefaultForeground;
                Console.Write($" {weapon.Damage}, {weapon.HitChance}%");
                w++;
            }

        }
        public static void OwnedPotions(Player player)
        {

        }
        public static void OwnedArmor(Player player)
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 11);
            Console.Write("Head:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 12);
            Console.Write("Chest:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 13);
            Console.Write("Legs:");

        }
        public static void OwnedKeys(Level level)
        {
            
            Console.SetCursorPosition(UI.InventoryBox.PosX+1, UI.InventoryBox.PosY+19);
            Console.Write("KEYS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX+1, UI.InventoryBox.PosY+20);
            for (int i = 0; i < UI.InventoryBox.Width-1; i++)
            {
                Console.Write(" ");
            }
            int j = 0;  
            foreach (Key key in level.PlayerKeys)
            {
                Console.SetCursorPosition(UI.InventoryBox.PosX + 2 + j, UI.InventoryBox.PosY + 20);
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
                j += 4;
            }
            
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;
        }
        public static void InventoryNav()
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX + 20, UI.InventoryBox.PosY + 1);
            Console.Write("Opened");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 3);
            Console.Write("WEAPONS: Damage, HitChance");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 8);
            Console.Write("POTIONS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 10);
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
                    Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 10);
                    Console.Write("ARMOR:");
                    Console.BackgroundColor = DefaultBackground;
                    Console.ForegroundColor = DefaultForeground;
                    break;
            }
        }
        public static void WeaponNav(Player player)
        {
            int i = 0;
            foreach (Weapon weapon in player.PlayerWeapons)
            {
                if (Inventory.SubMenuIndicator == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 4 + i);
                Console.Write($"{weapon.Name}:");
                Console.BackgroundColor = DefaultBackground;
                Console.ForegroundColor = DefaultForeground;
                Console.Write($" {weapon.Damage}, {weapon.HitChance}%");
                i++;
            }
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
