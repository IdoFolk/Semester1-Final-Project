using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ConsoleDungeonCrawler.Character;
using ConsoleDungeonCrawler.Level_Elements;

namespace ConsoleDungeonCrawler
{
    struct Range
    {
        public bool On;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Range(bool on,int rangeWidth, int rangeHeight)
        {
            On = on; 
            Width = rangeWidth;
            Height = rangeHeight;
        }
        public Range(int rangeWidth, int rangeHeight)
        {
            Width = rangeWidth;
            Height = rangeHeight;
            On = true;
        }
    }
    static class Game
    {
        public static readonly int[] LevelNumber = new int[2] { 1, 2 };
        public static List<Weapon> Weapons = new List<Weapon>();
        public static Range FogOfWar;
        public static void Start()
        {
            LoadItems();
            FogOfWar = new Range(false,7,4);
            Player player = new Player("Ido", 10);
            for (int i = 0; i < LevelNumber.Length; i++) //Level Gameplay
            {
                //Level level = LoadLevel(LevelNumber[i], player);
                Level level = LevelPresets.SetLevel(LevelNumber[i], player);
                Printer.HUD.LogReset();
                Printer.UI.GameUI();
                PlayLevel(level, player);
                if (player.IsDead()) break;
            }
            Result(player);
        }
        private static void PlayLevel(Level level, Player player)
        {
            while (!(level.IsComplete || player.IsDead()))
            {
                Printer.HUD.GameState(level, player);
                EnemiesActions(level, player);
                level.UpdateGrid();
                player.Action(level);
                level.UpdateGrid();
            }
        }
        private static void EnemiesActions(Level level, Player player)
        {
            foreach (Enemy enemy in level.Enemies)
            {
                if (enemy.IsClose(player))
                {
                    Direction direction = enemy.MovePattern(level, player);
                    foreach (Enemy enemy1 in level.Enemies)
                    {
                        if (enemy.Id == enemy1.Id) continue;
                        else if (enemy.Pos.X == enemy1.Pos.X && enemy.Pos.Y == enemy1.Pos.Y)
                            enemy.DontMove(direction);
                    }
                }
            }
        }
        private static void LoadItems()
        {
            LoadWeapons();
        }
        private static void LoadWeapons()
        {
            Weapons.Add(new Weapon("Fists", 1, 60f));
            Weapons.Add(new Weapon("Sword", 2, 80f));
            Weapons.Add(new Weapon("Great Axe", 4, 50f));
        }
        private static void Result(Player player)
        {
            if (player.IsDead())
            {
                Console.Clear();
                Printer.UI.YouDied();
            }
            else
            {
                Console.Clear();
                Printer.UI.YouWin();
            }
        }
    }
}
