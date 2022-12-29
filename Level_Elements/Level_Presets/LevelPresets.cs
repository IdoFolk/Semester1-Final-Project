using ConsoleDungeonCrawler.Character;
using ConsoleDungeonCrawler.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Level_Elements
{
    static class LevelPresets
    {
        static Level _level;
        public static Level SetLevel(int levelNum, Player player)
        {
            _level = new Level(levelNum,player);
            PresetEditor(levelNum);
            return _level;
        }
        private static void PresetEditor(int levelNum)
        {
            switch (levelNum)
            {
                case 1:
                    SetEnemies("Goblin", ConsoleColor.DarkGreen, 0, 2, 2, 1, 80f);
                    SetKey(1, ConsoleColor.Blue);
                    SetKey(2, ConsoleColor.Green);
                    SetDoor(1, ConsoleColor.Blue);
                    break;
                case 2:
                    SetEnemies("Goblin", ConsoleColor.DarkGreen, 0, 2, 2, 1, 80f);
                    SetEnemies("Orc", ConsoleColor.DarkRed, 2, 4, 3, 2, 90f);
                    break;
            }
            
        }
        private static void SetKey(int key, ConsoleColor color)
        {
            _level.Keys[key - 1].Color = color;
        }
        private static void SetDoor(int door, ConsoleColor color)
        {
            _level.Doors[door - 1].Color = color;
        }
        private static void SetEnemies(string name, ConsoleColor color, int amountMin, int amountMax, int hp, int damage, float chaseChance)
        {
            //Amount is capped by the total enemy amount
            if ((amountMax - amountMin) > _level.Enemies.Count)
            {
                Console.WriteLine("Enemy amount Capped");
                Environment.Exit(0);
            }
            for (int enemy = amountMin; enemy < amountMax; enemy++)
            {
                _level.Enemies[enemy].Name = name;
                _level.Enemies[enemy].Id = enemy+1;
                _level.Enemies[enemy].Color = color;
                _level.Enemies[enemy].MaxHP = hp;
                _level.Enemies[enemy].CurrentHP = _level.Enemies[enemy].MaxHP;
                _level.Enemies[enemy].Damage = damage;
                _level.Enemies[enemy].ChaseChance = chaseChance;
            }
        }
    }
}
