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
    struct FogofWar
    {
        public bool On { get; private set; }
        public int RangeWidth { get; private set; }
        public int RangeHeight { get; private set; }
        public FogofWar(bool on,int rangeWidth, int rangeHeight)
        {
            On = on; 
            RangeWidth = rangeWidth;
            RangeHeight = rangeHeight;
        }
    }
    static class Game
    {
        public static readonly int[] LevelNumber = new int[2] { 1, 2 };
        public static List<Weapon> Weapons = new List<Weapon>();
        public static FogofWar FogOfWar;
        public static void Start()
        {
            LoadWeapons();
            FogOfWar = new FogofWar(true,7,4);
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
        public static void PlayLevel(Level level, Player player)
        {
            while (!(level.Complete || player.IsDead()))
            {
                Printer.HUD.GameState(level, player);
                EnemiesActions(level, player);
                level.UpdateGrid();
                player.Action(level);
                level.UpdateGrid();
            }
        }
        public static void EnemiesActions(Level level, Player player)
        {
            foreach (Enemy enemy in level.Enemies)
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
        public static void LoadWeapons()
        {
            Weapons.Add(new Weapon("Fists", 1, 60f));

        }
        public static void Result(Player player)
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
