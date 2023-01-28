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
using ConsoleDungeonCrawler.Printer;

namespace ConsoleDungeonCrawler
{
    
    static class Game
    {
        public static readonly int[] LevelNumber = new int[] {1,2,3,4,5,6,7,8,9,10};
        public static List<Weapon> Weapons = new List<Weapon>();
        public static List<Potion> Potions = new List<Potion>();
        public static List<Armor> Armors = new List<Armor>();
        //Settings:
        public static Range FogOfWar = new Range(false, 6, 3);
        public static Difficulty Difficulty = Difficulty.Easy;
        public static ConsoleColor AvatarsColor { get; private set; } = ConsoleColor.DarkYellow;
        public static string PlayersName { get; private set; } = "Student";
        public static bool PlayerIsMale { get; private set; } = true;
        public static bool GameLost { get; private set; } = false;
        public static bool NoPause { get; private set; }
        public static bool SFXon { get; private set; } = true;
        public static bool Musicon { get; private set; } = true;
        //Stats:
        public static Dictionary<StatType, int> GameStats = new Dictionary<StatType, int>();
        public static int TotalEnemiesKilled { get; private set; }
        public static int TotalBossesKilled { get; private set; }
        public static int TotalChestsOpened { get; private set; }
        public static int TotalCoinsCollected { get; private set; }
        public static int TotalTrapsRevealed { get; private set; }
        public static int LevelsPassed { get; private set; }
        public static void Start()
        {
            HUD.StartingCutscene();
            LoadItems();
            LoadStats();
            GameLost = false;
            Player player = new Player(PlayersName,AvatarsColor, 10);
            for (int i = 0; i < LevelNumber.Length; i++) //Level Gameplay
            {
                if (Menu.CloseGame) return;
                Level level = LevelPresets.SetLevel(LevelNumber[i], player);
                Printer.HUD.LogReset();
                Printer.UI.GameUI();
                PlayLevel(level, player);
                if (player.IsDead()) break;
            }
            Result(player);
            Sounds.PlayMusic(Sounds.MenuMusic);
        }
        private static void PlayLevel(Level level, Player player)
        {
            Sounds.PlaySFX(Sounds.EnterSFX);
            while (!(level.IsComplete || GameLost))
            {
                if (Menu.CloseGame) return;
                Console.CursorVisible = false;
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
                if (enemy.IsDead()) continue;
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
            Weapons.Clear();
            switch (Difficulty)
            {
                case Difficulty.Easy:
                    Weapons.Add(new Weapon("Fists", 1, 0.7f, 0, 0));
                    Weapons.Add(new Weapon("Dagger", 2, 0.8f, 3, 0.3f));
                    Weapons.Add(new Weapon("Sword", 2, 0.9f, 4, 0.55f));
                    Weapons.Add(new Weapon("Spear", 3, 0.95f, 4, 0.75f));
                    Weapons.Add(new Weapon("Mace", 4, 0.6f, 5, 0.9f));
                    Weapons.Add(new Weapon("GreatAxe", 5, 0.5f, 5, 1f));
                    break;
                case Difficulty.Medium:
                    Weapons.Add(new Weapon("Fists", 1, 0.6f, 0, 0));
                    Weapons.Add(new Weapon("Dagger", 2, 0.7f, 2, 0.3f));
                    Weapons.Add(new Weapon("Sword", 2, 0.8f, 4, 0.55f));
                    Weapons.Add(new Weapon("Spear", 3, 0.9f, 3, 0.75f));
                    Weapons.Add(new Weapon("Mace", 4, 0.5f, 5, 0.9f));
                    Weapons.Add(new Weapon("GreatAxe", 5, 0.4f, 5, 1f));
                    break;
                case Difficulty.Hard:
                    Weapons.Add(new Weapon("Fists", 1, 0.5f, 0, 0));
                    Weapons.Add(new Weapon("Dagger", 2, 0.6f, 2, 0.3f));
                    Weapons.Add(new Weapon("Sword", 2, 0.7f, 4, 0.55f));
                    Weapons.Add(new Weapon("Spear", 3, 0.8f, 3, 0.75f));
                    Weapons.Add(new Weapon("Mace", 4, 0.5f, 5, 0.9f));
                    Weapons.Add(new Weapon("GreatAxe", 5, 0.4f, 5, 1f));
                    break;
            }
            
        }
        private static void LoadPotions()
        {
            Potions.Clear();
            switch (Difficulty)
            {
                case Difficulty.Easy:
                    Potions.Add(new Potion(ConsoleColor.Cyan, 3, 0.5f));
                    Potions.Add(new Potion(ConsoleColor.Yellow, 5, 0.8f));
                    Potions.Add(new Potion(ConsoleColor.Magenta, 7, 1f));
                    break;
                case Difficulty.Medium:
                    Potions.Add(new Potion(ConsoleColor.Cyan, 2, 0.5f));
                    Potions.Add(new Potion(ConsoleColor.Yellow, 4, 0.8f));
                    Potions.Add(new Potion(ConsoleColor.Magenta, 6, 1f));
                    break;
                case Difficulty.Hard:
                    Potions.Add(new Potion(ConsoleColor.Cyan, 2, 0.5f));
                    Potions.Add(new Potion(ConsoleColor.Yellow, 4, 0.8f));
                    Potions.Add(new Potion(ConsoleColor.Magenta, 6, 1f));
                    break;
            }
            
        }
        private static void LoadArmors()
        {
            Armors.Clear();
            switch (Difficulty)
            {
                case Difficulty.Easy:
                    Armors.Add(new Armor("Underwear", 0, 0, 0));
                    Armors.Add(new Armor("T-Shirt", 0.6f, 3, 0.4f));
                    Armors.Add(new Armor("Jacket", 0.5f, 4, 0.7f));
                    Armors.Add(new Armor("Coat", 0.2f, 5, 0.9f));
                    Armors.Add(new Armor("Hoodie", 0.95f, 1, 1f));
                    break;
                case Difficulty.Medium:
                    Armors.Add(new Armor("Underwear", 0, 0, 0));
                    Armors.Add(new Armor("T-Shirt", 0.5f, 2, 0.4f));
                    Armors.Add(new Armor("Jacket", 0.5f, 3, 0.7f));
                    Armors.Add(new Armor("Coat", 0, 5, 0.9f));
                    Armors.Add(new Armor("Hoodie", 0.95f, 1, 1f));
                    break;
                case Difficulty.Hard:
                    Armors.Add(new Armor("Underwear", 0, 0, 0));
                    Armors.Add(new Armor("T-Shirt", 0.5f, 2, 0.4f));
                    Armors.Add(new Armor("Jacket", 0.5f, 3, 0.7f));
                    Armors.Add(new Armor("Coat", 0, 5, 0.9f));
                    Armors.Add(new Armor("Hoodie", 0.95f, 1, 1f));
                    break;
            }
            
        }
        private static void Result(Player player)
        {
            if (Menu.CloseGame) return;
            if (player.IsDead())
            {
                Console.Clear();
                Printer.UI.YouDied(20 + UI.StartingPosX, 10);
                Sounds.PlaySFX(Sounds.DeadSFX);
                Console.SetCursorPosition(45 + UI.StartingPosX, 7);
                Console.Write("Press Enter To Return To Main Menu...");
                while (true)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Enter) return;
                }
            }
            else
            {
                Console.Clear();
                PrintMenu.Clover(37 + UI.StartingPosX, 10);
                Printer.UI.YouWin(34 + UI.StartingPosX, 32);
                Sounds.PlaySFX(Sounds.WinSFX);
                Console.SetCursorPosition(45 + UI.StartingPosX, 7);
                Console.Write("Press Enter To Return To Main Menu...");
                while (true)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Enter) return;
                }
            }
        }
        public static void ChangePlayerName()
        {
            Console.CursorVisible = true;
            string name = Console.ReadLine();
            if (name == null || name.Length > 7) return;
            PlayersName = name;
        }
        public static void ChangeAvatarColor(ConsoleColor color)
        {
            AvatarsColor = color;
        }
        public static void SetDifficulty(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    FogOfWar.On = false;
                    NoPause = false;
                    break;
                case Difficulty.Medium:
                    FogOfWar.On = false;
                    NoPause = true;
                    break;
                case Difficulty.Hard:
                    FogOfWar.On = true;
                    NoPause = true;
                    break;
            }
        }
        public static void ToggleSEX()
        {
            if (PlayerIsMale) PlayerIsMale = false;
            else PlayerIsMale = true;
        }
        public static void ToggleSFX()
        {
            if (SFXon) SFXon = false;
            else SFXon = true;
        }
        public static void ToggleMusic()
        {
            if (Musicon)
            {
                Musicon = false;
                Sounds.StopAllMusic();
            }
            else
            {
                Musicon = true;
                Sounds.PlayMusic(Sounds.MenuMusic);
            }
        }
        public static void LoseGame()
        {
            if (GameLost) GameLost = false;
            else GameLost = true;
        }
        public static void LoadStats()
        {
            GameStats.Clear();
            GameStats.Add(StatType.EnemiesKilled, 0);
            GameStats.Add(StatType.BossesKilled, 0);
            GameStats.Add(StatType.ChestsOpened, 0);
            GameStats.Add(StatType.CoinsCollected, 0);
            GameStats.Add(StatType.TrapsRevealed, 0);
            GameStats.Add(StatType.LevelsPassed, 0);
        }
        public static void AddToStat(StatType statType)
        {
            switch (statType)
            {
                case StatType.EnemiesKilled:
                    GameStats[StatType.EnemiesKilled]++;
                    break;
                case StatType.BossesKilled:
                    GameStats[StatType.BossesKilled]++;
                    break;
                case StatType.ChestsOpened:
                    GameStats[StatType.ChestsOpened]++;
                    break;
                case StatType.TrapsRevealed:
                    GameStats[StatType.TrapsRevealed]++;
                    break;
                case StatType.LevelsPassed:
                    GameStats[StatType.LevelsPassed]++;
                    break;
            }
        }
        public static void AddToStat(int coins)
        {
            GameStats[StatType.CoinsCollected] += coins;
        }
    }
}
