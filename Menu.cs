using ConsoleDungeonCrawler.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDungeonCrawler.Level_Elements;
using ConsoleDungeonCrawler.Character;

namespace ConsoleDungeonCrawler
{
    static class Menu
    {
        public static int MenuIndicator { get; private set; } 
        public static bool MenuIsOpen { get; private set; } = false;
        public static bool PauseMenuIsOpen { get; private set; } = false;
        public static bool OptionsIsOpen { get; private set; } = false;
        public static bool SubOptionsIsOpen { get; private set; } = false;
        public static bool SubSubOptionsIsOpen { get; private set; } = false;
        public static bool CloseGame { get; private set; } = false;
        public static void MainMenu()
        {
            OpenMenu();
            Sounds.PlayMusic(Sounds.MenuMusic);
            while (MenuIsOpen)
            {
                Console.CursorVisible = false;
                PrintMenu.PrintMainMenu();
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow :
                        MenuIndicator--;
                        break;
                    case  ConsoleKey.W:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.DownArrow:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.S:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        switch (MenuIndicator)
                        {
                            case 0:
                                CloseGame = false;
                                Sounds.StopMusic(Sounds.MenuMusic);
                                Game.Start();
                                break;
                            case 1:
                                OptionsMenu();
                                break;
                            case 2:
                                Credits();
                                break;
                            case 3:
                                Environment.Exit(0);
                                break;
                        }
                        MenuIndicator = 0;
                        Console.Clear();
                        break;
                }
                if (MenuIndicator < 0) MenuIndicator = 0;
                if (MenuIndicator > 3) MenuIndicator = 3;
            }
        }
        private static void OptionsMenu()
        {
            MenuIndicator = 1;
            OpenOptions();
            while (OptionsIsOpen)
            {
                Console.CursorVisible = false;
                PrintMenu.PrintOptionsMenu();
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.W:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.DownArrow:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.S:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        switch (MenuIndicator)
                        {
                            case 1:
                                AvatarMenu();
                                break;
                            case 2:
                                if (PauseMenuIsOpen)
                                    break;
                                DifficultyMenu();
                                break;
                            case 3:
                                Controls();
                                break;
                            case 4:
                                SoundOptions();
                                break;
                        }
                        Console.Clear();
                        MenuIndicator = 1;
                        break;
                    case ConsoleKey.Escape:
                        CloseOptions();
                        break;
                }
                if (MenuIndicator < 1) MenuIndicator = 1;
                if (MenuIndicator > 4) MenuIndicator = 4;
            }
        }
        private static void AvatarMenu()
        {
            MenuIndicator = 1;
            SubOptionsIsOpen = true;
            while (SubOptionsIsOpen)
            {
                Console.CursorVisible = false;
                PrintMenu.AvatarOptions();
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.W:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.DownArrow:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.S:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.Enter:
                        switch (MenuIndicator)
                        {
                            case 1:
                                ChangeName();
                                break;
                            case 2:
                                Game.ToggleSEX();
                                break;
                            case 3:
                                ChangeColor();
                                break;
                        }
                        Console.Clear();
                        break;
                    case ConsoleKey.Escape:
                        SubOptionsIsOpen = false;
                        break;
                }
                if (MenuIndicator < 1) MenuIndicator = 1;
                if (MenuIndicator > 3) MenuIndicator = 3;
            }
        }
        private static void ChangeName()
        {
            Console.SetCursorPosition(PrintMenu.ButtonPosX + UI.StartingPosX + 6, PrintMenu.ButtonPosY + 2);
            Console.Write("               Max 7 characters.");
            Console.SetCursorPosition(PrintMenu.ButtonPosX + UI.StartingPosX + 6, PrintMenu.ButtonPosY + 2);
            Game.ChangePlayerName();
        }
        private static void ChangeColor()
        {
            MenuIndicator = 1;
            SubSubOptionsIsOpen = true;
            while (SubSubOptionsIsOpen)
            {
                Console.CursorVisible = false;
                PrintMenu.AvatarColorOptions();
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.W:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.DownArrow:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.S:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.Enter:
                        switch (MenuIndicator)
                        {
                            case 1:
                                Game.ChangeAvatarColor(ConsoleColor.DarkRed);
                                break;
                            case 2:
                                Game.ChangeAvatarColor(ConsoleColor.DarkGreen);
                                break;
                            case 3:
                                Game.ChangeAvatarColor(ConsoleColor.DarkBlue);
                                break;
                            case 4:
                                Game.ChangeAvatarColor(ConsoleColor.DarkMagenta);
                                break;
                            case 5:
                                Game.ChangeAvatarColor(ConsoleColor.DarkYellow);
                                break;
                            case 6:
                                Game.ChangeAvatarColor(ConsoleColor.DarkCyan);
                                break;
                        }
                        SubSubOptionsIsOpen = false;
                        break;
                    case ConsoleKey.Escape:
                        SubSubOptionsIsOpen = false;
                        break;
                }
                Console.Clear();
                if (MenuIndicator < 1) MenuIndicator = 1;
                if (MenuIndicator > 6) MenuIndicator = 6;
            }
        }
        private static void DifficultyMenu()
        {
            MenuIndicator = 1;
            SubOptionsIsOpen = true;
            while (SubOptionsIsOpen)
            {
                Console.CursorVisible = false;
                PrintMenu.DifficultyOptions();
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.W:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.DownArrow:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.S:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.Enter:
                        switch (MenuIndicator)
                        {
                            case 1:
                                Game.Difficulty = Difficulty.Easy;
                                Game.SetDifficulty(Difficulty.Easy);
                                break;
                            case 2:
                                Game.Difficulty = Difficulty.Medium;
                                Game.SetDifficulty(Difficulty.Medium);
                                break;
                            case 3:
                                Game.Difficulty = Difficulty.Hard;
                                Game.SetDifficulty(Difficulty.Hard);
                                break;
                        }
                        Console.Clear();
                        SubOptionsIsOpen = false;
                        break;
                    case ConsoleKey.Escape:
                        SubOptionsIsOpen = false;
                        break;
                }
                if (MenuIndicator < 1) MenuIndicator = 1;
                if (MenuIndicator > 3) MenuIndicator = 3;
            }
        }
        private static void Controls()
        {
            PrintMenu.PrintControls();
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape) return;
            }
        }
        private static void SoundOptions()
        {
            MenuIndicator = 1;
            SubOptionsIsOpen = true;
            while (SubOptionsIsOpen)
            {
                Console.CursorVisible = false;
                PrintMenu.PrintSoundOptions();
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.W:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.DownArrow:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.S:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.Enter:
                        switch (MenuIndicator)
                        {
                            case 1:
                                Game.ToggleMusic();
                                break;
                            case 2:
                                Game.ToggleSFX();
                                break;
                        }
                        Console.Clear();
                        break;
                    case ConsoleKey.Escape:
                        SubOptionsIsOpen = false;
                        break;
                }
                if (MenuIndicator < 1) MenuIndicator = 1;
                if (MenuIndicator > 2) MenuIndicator = 2;
            }
        }
        private static void Credits()
        {
            Sounds.PlayMusic(Sounds.IntroMusic);
            PrintMenu.PrintCredits();
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    Sounds.PlayMusic(Sounds.MenuMusic);
                    return;
                }
            }
        }
        private static void OpenMenu()
        {
            MenuIsOpen = true;
        }
        private static void OpenOptions()
        {
            OptionsIsOpen = true;
        }
        private static void CloseOptions()
        {
            OptionsIsOpen = false;
        }
        public static void PauseMenu(Level level, Player player)
        {
            PauseMenuIsOpen = true;
            Map.MapClear();
            while (PauseMenuIsOpen)
            {
                Console.CursorVisible = false;
                PrintMenu.PrintPauseMenu();
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.W:
                        MenuIndicator--;
                        break;
                    case ConsoleKey.DownArrow:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.S:
                        MenuIndicator++;
                        break;
                    case ConsoleKey.Enter:
                        switch (MenuIndicator)
                        {
                            case 0:
                                PauseMenuIsOpen = false;
                                break;
                            case 1:
                                Console.Clear();
                                OptionsMenu();
                                break;
                            case 2:
                                Console.Clear();
                                CloseGame = true;
                                PauseMenuIsOpen = false;
                                return;
                        }
                        Console.Clear();
                        UI.GameUI();
                        HUD.GameStatePaused(level, player);
                        MenuIndicator = 0;
                        break;
                    case ConsoleKey.Escape:
                        PauseMenuIsOpen = false;
                        break;
                }
                if (MenuIndicator < 0) MenuIndicator = 0;
                if (MenuIndicator > 2) MenuIndicator = 2;
            }
        }
    }
}
