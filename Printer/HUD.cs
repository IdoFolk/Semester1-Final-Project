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
            Inventory(player, level);
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
            Console.Write($"{player.Name} attacks {enemy.Name} for {player.EquippedWeapon.Damage} damage.");
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
            Console.Write($"{enemy.Name} attacks {player.Name} for {enemy.Damage} damage.");
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
        public static void Inventory(Player player, Level level)
        {
            OwnedKeys(level);
        }
        public static void OwnedKeys(Level level)
        {
            
            Console.SetCursorPosition(UI.InventoryBox.PosX+1, UI.InventoryBox.PosY+18);
            Console.Write("Keys:");
            Console.SetCursorPosition(UI.InventoryBox.PosX+1, UI.InventoryBox.PosY+19);
            for (int i = 0; i < UI.InventoryBox.Width-1; i++)
            {
                Console.Write(" ");
            }
            int j = 0;  
            foreach (Key key in level.PlayerKeys)
            {
                Console.SetCursorPosition(UI.InventoryBox.PosX + 4 + j, UI.InventoryBox.PosY + 19);
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

    }
}
