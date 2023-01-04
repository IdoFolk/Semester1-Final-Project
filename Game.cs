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
        public static List<Potion> Potions = new List<Potion>();
        public static List<Armor> Armors = new List<Armor>();
        public static Range FogOfWar;
        public static bool NoPause { get; private set; } = false;
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
        public static void EnemiesActions(Level level, Player player)
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
            LoadPotions();
            LoadArmors();
        }
        private static void LoadWeapons()
        {
            Weapons.Add(new Weapon("Fists", 1, 0.6f, 0,0));
            Weapons.Add(new Weapon("Dagger", 2, 0.7f, 2,0.3f));
            Weapons.Add(new Weapon("Sword", 2, 0.8f, 4,0.55f));
            Weapons.Add(new Weapon("Spear", 3, 0.9f, 3,0.75f));
            Weapons.Add(new Weapon("Mace", 4, 0.5f, 5,0.9f));
            Weapons.Add(new Weapon("GreatAxe", 5, 0.4f, 5,1f));
        }
        private static void LoadPotions()
        {
            Potions.Add(new Potion(ConsoleColor.Green, 2, 0.5f));
            Potions.Add(new Potion(ConsoleColor.Blue, 4, 0.8f));
            Potions.Add(new Potion(ConsoleColor.Magenta, 6, 1f));
        }
        private static void LoadArmors()
        {
            Armors.Add(new Armor("Underwear",0,0,0));
            Armors.Add(new Armor("Cloth",0.5f,2,0.4f));
            Armors.Add(new Armor("Leather",0.4f,3,0.7f));
            Armors.Add(new Armor("Plate",0,5,0.9f));
            Armors.Add(new Armor("Cloak",0.9f,1,1f));
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
