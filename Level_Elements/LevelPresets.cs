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
