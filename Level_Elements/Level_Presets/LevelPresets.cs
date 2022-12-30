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
        Goblin,
        Orc
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
                    SetEnemies(EnemyType.Goblin, 2);
                    SetDoor(ConsoleColor.Blue);
                    SetDoor(ConsoleColor.Red);
                    SetDoor(ConsoleColor.Yellow);
                    SetKey(ConsoleColor.Red);
                    SetKey(ConsoleColor.Yellow);
                    SetKey(ConsoleColor.Blue);
                    ResetIndicator();
                    break;
                case 2:
                    SetEnemies(EnemyType.Goblin, 2);
                    SetEnemies(EnemyType.Orc, 2);
                    ResetIndicator();
                    break;
            }

        }
        private static void SetKey(ConsoleColor color)
        {
            _level.Keys[_keyIndicator].Color = color;
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
                    case EnemyType.Goblin:
                        _level.Enemies[enemy].Name = "Goblin";
                        _level.Enemies[enemy].Id = enemy;
                        _level.Enemies[enemy].Color = ConsoleColor.DarkGreen;
                        _level.Enemies[enemy].MaxHP = 2;
                        _level.Enemies[enemy].CurrentHP = _level.Enemies[enemy].MaxHP;
                        _level.Enemies[enemy].Damage = 1;
                        _level.Enemies[enemy].ChaseChance = 90f;
                        break;
                    case EnemyType.Orc:
                        _level.Enemies[enemy].Name = "Orc";
                        _level.Enemies[enemy].Id = enemy;
                        _level.Enemies[enemy].Color = ConsoleColor.DarkRed;
                        _level.Enemies[enemy].MaxHP = 4;
                        _level.Enemies[enemy].CurrentHP = _level.Enemies[enemy].MaxHP;
                        _level.Enemies[enemy].Damage = 1;
                        _level.Enemies[enemy].ChaseChance = 90f;
                        break;
                    default:
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
