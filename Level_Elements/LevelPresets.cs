using ConsoleDungeonCrawler.Character;
using ConsoleDungeonCrawler.Printer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace ConsoleDungeonCrawler.Level_Elements
{
    enum EnemyType
    {
        Freshmen,
        Junior,
        Senior
    }
    static class LevelPresets
    {
        static Level _level;
        static int _enemyIndicator = 0;
        static int _keyIndicator = 0;
        static int _doorIndicator = 0;
        public static Level SetLevel(int levelNum, Player player)
        {
            _level = new Level(levelNum, player);
            PresetEditor(levelNum);
            return _level;
        }
        private static void PresetEditor(int levelNum)
        {
            switch (levelNum)
            {
                case 1:
                    SetEnemies(EnemyType.Freshmen, 3);
                    SetDoor(ConsoleColor.Blue);
                    SetKey(ConsoleColor.Blue);
                    ResetIndicator();
                    break;
                case 2:
                    SetEnemies(EnemyType.Junior, 2);
                    SetEnemies(EnemyType.Freshmen, 3);
                    SetKey(ConsoleColor.Yellow);
                    SetKey(ConsoleColor.Magenta);
                    SetKey(ConsoleColor.Blue);
                    SetDoor(ConsoleColor.Blue);
                    SetDoor(ConsoleColor.Yellow);
                    SetDoor(ConsoleColor.Magenta);
                    ResetIndicator();
                    break;
                case 3:
                    SetEnemies(EnemyType.Freshmen, 2);
                    SetEnemies(EnemyType.Junior, 1);
                    SetEnemies(EnemyType.Freshmen, 6);
                    SetEnemies(EnemyType.Senior, 1);
                    SetDoor(ConsoleColor.Yellow);
                    SetDoor(ConsoleColor.Cyan);
                    SetDoor(ConsoleColor.Magenta);
                    SetKey(ConsoleColor.Cyan);
                    SetKey(ConsoleColor.Magenta);
                    SetKey(ConsoleColor.Yellow);
                    ResetIndicator();
                    break;
                case 4:
                    SetEnemies(EnemyType.Junior, 2);
                    SetEnemies(EnemyType.Freshmen, 5);
                    SetEnemies(EnemyType.Junior, 3);
                    SetDoor(ConsoleColor.Yellow);
                    SetDoor(ConsoleColor.Green);
                    SetDoor(ConsoleColor.Magenta);
                    SetKey(ConsoleColor.Magenta);
                    SetKey(ConsoleColor.Yellow);
                    SetKey(ConsoleColor.Green);
                    ResetIndicator();
                    break;
                case 5:
                    SetDoor(ConsoleColor.Magenta);
                    SetDoor(ConsoleColor.Cyan);
                    SetDoor(ConsoleColor.Blue);
                    SetDoor(ConsoleColor.Yellow);
                    SetKeyInShop(ConsoleColor.Cyan,30);
                    SetKeyInShop(ConsoleColor.Blue,75);
                    SetKeyInShop(ConsoleColor.Magenta,100);
                    SetKey(ConsoleColor.Yellow);
                    ResetIndicator();
                    break;
                case 6:
                    SetDoor(ConsoleColor.Cyan);
                    SetDoor(ConsoleColor.Magenta);
                    SetDoor(ConsoleColor.Yellow);
                    SetDoor(ConsoleColor.Red);
                    SetKey(ConsoleColor.Red);
                    SetKey(ConsoleColor.Yellow);
                    SetKey(ConsoleColor.Cyan);
                    SetKey(ConsoleColor.Magenta);
                    SetEnemies(EnemyType.Junior, 5);
                    SetEnemies(EnemyType.Senior, 1);
                    SetEnemies(EnemyType.Junior, 2);
                    ResetIndicator();
                    break;
                case 7:
                    SetDoor(ConsoleColor.Yellow);
                    SetDoor(ConsoleColor.Magenta);
                    SetDoor(ConsoleColor.Cyan);
                    SetDoor(ConsoleColor.Red);
                    SetKey(ConsoleColor.Red);
                    SetKey(ConsoleColor.Cyan);
                    SetKey(ConsoleColor.Magenta);
                    SetKey(ConsoleColor.Yellow);
                    SetEnemies(EnemyType.Junior, 2);
                    SetEnemies(EnemyType.Senior, 4);
                    SetEnemies(EnemyType.Junior, 3);
                    ResetIndicator();
                    break;
                case 8:
                    SetDoor(ConsoleColor.Yellow);
                    SetDoor(ConsoleColor.Red);
                    SetKey(ConsoleColor.Red);
                    SetKey(ConsoleColor.Yellow);
                    SetEnemies(EnemyType.Junior, 4);
                    SetEnemies(EnemyType.Freshmen, 3);
                    SetEnemies(EnemyType.Junior, 1);
                    SetEnemies(EnemyType.Freshmen, 1);
                    SetEnemies(EnemyType.Junior, 1);
                    SetEnemies(EnemyType.Freshmen, 1);
                    ResetIndicator();
                    break;
                case 9:
                    SetDoor(ConsoleColor.Magenta);
                    SetDoor(ConsoleColor.Cyan);
                    SetDoor(ConsoleColor.Blue);
                    SetDoor(ConsoleColor.Red);
                    SetKeyInShop(ConsoleColor.Cyan, 50);
                    SetKeyInShop(ConsoleColor.Blue, 100);
                    SetKeyInShop(ConsoleColor.Magenta, 150);
                    SetKey(ConsoleColor.Red);
                    ResetIndicator();
                    break;
                case 10:
                    SetDoor(ConsoleColor.Red);
                    SetKey(ConsoleColor.Red);
                    ResetIndicator();
                    break;
            }

        }
        private static void SetKey(ConsoleColor color)
        {
            _level.Keys[_keyIndicator].SetColor(color);
            _keyIndicator++;
        }
        private static void SetKeyInShop(ConsoleColor color, int price)
        {
            _level.Keys[_keyIndicator].KeyInShop();
            _level.Keys[_keyIndicator].SetPrice(price);
            _level.Keys[_keyIndicator].SetColor(color);
            _keyIndicator++;
        }
        private static void SetDoor(ConsoleColor color)
        {
            _level.Doors[_doorIndicator].Color = color;
            _doorIndicator++;
        }
        private static void SetEnemies(EnemyType enemyType, int amount)
        {
            //Amount is capped by the total enemy amount
            if ((_enemyIndicator + amount) > _level.Enemies.Count)
            {
                Console.Clear();
                Console.WriteLine("Enemy amount Capped");
                Environment.Exit(0);
            }
            int enemyCap = _enemyIndicator;
            for (int enemy = _enemyIndicator; enemy < amount + enemyCap; enemy++)
            {
                switch (enemyType)
                {
                    case EnemyType.Freshmen:
                        _level.Enemies[enemy].Name = "Freshmen";
                        _level.Enemies[enemy].Id = enemy;
                        _level.Enemies[enemy].Color = ConsoleColor.DarkGreen;
                        _level.Enemies[enemy].MaxHP = 2;
                        _level.Enemies[enemy].CurrentHP = _level.Enemies[enemy].MaxHP;
                        _level.Enemies[enemy].Damage = 1;
                        _level.Enemies[enemy].ChaseChance = 0.9f;
                        break;
                    case EnemyType.Junior:
                        _level.Enemies[enemy].Name = "Junior";
                        _level.Enemies[enemy].Id = enemy;
                        _level.Enemies[enemy].Color = ConsoleColor.DarkYellow;
                        _level.Enemies[enemy].MaxHP = 4;
                        _level.Enemies[enemy].CurrentHP = _level.Enemies[enemy].MaxHP;
                        _level.Enemies[enemy].Damage = 2;
                        _level.Enemies[enemy].ChaseChance = 0.9f;
                        break;
                    case EnemyType.Senior:
                        _level.Enemies[enemy].Name = "Senior";
                        _level.Enemies[enemy].Id = enemy;
                        _level.Enemies[enemy].Color = ConsoleColor.DarkRed;
                        _level.Enemies[enemy].MaxHP = 6;
                        _level.Enemies[enemy].CurrentHP = _level.Enemies[enemy].MaxHP;
                        _level.Enemies[enemy].Damage = 2;
                        _level.Enemies[enemy].ChaseChance = 0.9f;
                        break;

                }
                _enemyIndicator++;
            }

        }
        private static void ResetIndicator()
        {
            _enemyIndicator = 0;
            _doorIndicator = 0;
            _keyIndicator = 0;
        }
    }
}
