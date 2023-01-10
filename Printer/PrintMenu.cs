using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Printer
{
    static class PrintMenu
    {
        private static ConsoleColor DefaultForeground = ConsoleColor.Gray;
        private static ConsoleColor DefaultBackground = ConsoleColor.Black;
        public static int ButtonPosX { get; private set; } = 55;
        public static int ButtonPosY { get; private set; } = 15;
        public static void PrintMainMenu()
        {
            PrintBackground();
            StartButton(0);
            OptionsButton(1);
            CreditsButton(2);
            ExitButton(3);
        }
        public static void PrintPauseMenu()
        {
            Console.SetCursorPosition(ButtonPosX -2 + UI.StartingPosX, ButtonPosY-2);
            Console.Write("PAUSED");
            ResumeButton(0);
            PausedOptionsButton(1);
            ExitToMenuButton(2);
        }
        public static void PrintOptionsMenu()
        {
            PrintBackground();
            OptionsButton(0);
            AvatarButton(1);
            DifficultyButton(2);
            ControlsButton(3);
            SoundButton(4);
        }
        public static void AvatarOptions()
        {
            PrintBackground();
            AvatarButton(0);
            AvatarName(1);
            AvatarSex(2);
            AvatarColor(3);
        }
        public static void AvatarColorOptions()
        {
            PrintBackground();
            AvatarButton(0);
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + 2);
            Console.Write($"NAME: {Game.PlayersName}");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + 4);
            Console.Write("SEX: ");
            if (Game.PlayerIsMale)
                Console.Write("MALE");
            else
                Console.Write("FEMALE");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + 6);
            Console.Write("COLOR:");
            IfSelectedArrow(Menu.MenuIndicator);
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX + 8, ButtonPosY + 6);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("@");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX + 8, ButtonPosY + 8);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("@");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX + 8, ButtonPosY + 10);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("@");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX + 8, ButtonPosY + 12);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("@");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX + 8, ButtonPosY + 14);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("@");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX + 8, ButtonPosY + 16);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("@");
            DefaultText();
        }
        public static void DifficultyOptions()
        {
            PrintBackground();
            DifficultyButton(0);
            EasyButton(1);
            MediumButton(2);
            HardButton(3);
        }
        public static void PrintSoundOptions()
        {
            PrintBackground();
            SoundButton(0);
            MusicButton(1);
            SFXButton(2);
        }
        private static void EasyButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("EASY");
            DefaultText();
        }
        private static void MediumButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("MEDIUM");
            DefaultText();
        }
        private static void HardButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("HARD");
            DefaultText();
        }
        private static void AvatarColor(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("COLOR:");
            DefaultText();
            Console.ForegroundColor = Game.AvatarsColor;
            Console.Write(" @");
            DefaultText();
        }
        private static void AvatarName(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("NAME:");
            DefaultText();
            Console.Write($" {Game.PlayersName}");
        }
        private static void AvatarSex(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("SEX:");
            DefaultText();
            if (Game.PlayerIsMale)
                Console.Write($" MALE");
            else
                Console.Write($" FEMALE");
        }
        private static void StartButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos*2));
            IfSelected(pos);
            Console.Write("START");
            DefaultText();
        }
        private static void OptionsButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY+ (pos * 2));
            IfSelected(pos);
            Console.Write("OPTIONS");
            if (Menu.OptionsIsOpen) Console.Write(":");
            DefaultText();
        }
        private static void CreditsButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("CREDITS");
            DefaultText();
        }
        private static void ExitButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("EXIT");
            DefaultText();
        }
        private static void ResumeButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX-2 + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("RESUME");
            DefaultText();
        }
        private static void PausedOptionsButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX-2 + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("OPTIONS");
            DefaultText();
        }
        private static void ExitToMenuButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX-2 + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("MAIN MENU");
            DefaultText();
        }
        private static void AvatarButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("AVATAR");
            if (Menu.SubOptionsIsOpen) Console.Write(":");
            DefaultText();
        }
        private static void DifficultyButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("DIFFICULTY");
            if (Menu.SubOptionsIsOpen)
            {
                Console.Write(": ");
                switch (Game.Difficulty)
                {
                    case Difficulty.Easy:
                        Console.Write("EASY");
                        break;
                    case Difficulty.Medium:
                        Console.Write("MEDIUM");
                        break;
                    case Difficulty.Hard:
                        Console.Write("HARD");
                        break;
                }
            }
            DefaultText();
        }
        private static void ControlsButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("CONTROLS");
            DefaultText();
        }
        private static void SoundButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("SOUND");
            if (Menu.SubOptionsIsOpen) Console.Write(":");
            DefaultText();
        }
        private static void MusicButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("MUSIC:");
            DefaultText();
            if (Game.Musicon) Console.Write(" ON");
            else Console.Write(" OFF");
        }
        private static void SFXButton(int pos)
        {
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + (pos * 2));
            IfSelected(pos);
            Console.Write("SFX:");
            DefaultText();
            if (Game.SFXon) Console.Write(" ON");
            else Console.Write(" OFF");
        }
        public static void PrintControls()
        {
            PrintBackground();
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY);
            Console.Write("CONTROLS:");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + 2);
            Console.Write("W/Up Arrow - Move up");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + 4);
            Console.Write("S/Down Arrow - Move Down");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + 6);
            Console.Write("A/Left Arrow - Move Left");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + 8);
            Console.Write("D/Right Arrow - Move Right");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + 10);
            Console.Write("ENTER - Select/Equip Item");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + 12);
            Console.Write("ESC - Back");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + 14);
            Console.Write("I - Open Inventory");
            Console.SetCursorPosition(ButtonPosX + UI.StartingPosX, ButtonPosY + 16);
            Console.Write("DELETE - Throw Item");

        }
        public static void PrintCredits()
        {
            PrintBackground();
            Console.SetCursorPosition(45 + UI.StartingPosX, 20);
            Console.Write("Everything is made by Me :)");
        }
        public static void PrintBackground()
        {
            UI.GameTitle(UI.StartingPosX+2, 3);
            Clover(40, 15);
            Character(150, 15);
            BasicControls();
        }
        public static void BasicControls()
        {
            Console.SetCursorPosition(45 + UI.StartingPosX, 35);
            Console.Write("ESC - Back");
            Console.SetCursorPosition(70 + UI.StartingPosX, 35);
            Console.Write("ENTER - Select");

        }
        public static void Clover(int posX, int posY)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            UI.TextReader(posX, posY, "Templates\\Clover.txt");
            Console.ForegroundColor = DefaultForeground;
        }
        public static void Character(int posX, int posY)
        {
            UI.TextReader(posX, posY, "Templates\\Avatar.txt");
        }
        private static void IfSelected(int pos)
        {
            if (Menu.OptionsIsOpen && pos == 0) return;
            if (pos == Menu.MenuIndicator)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }
        private static void IfSelectedArrow(int pos)
        {
            if (Menu.OptionsIsOpen && pos == 0) return;
            if (pos == Menu.MenuIndicator)
            {
                Console.SetCursorPosition(ButtonPosX + UI.StartingPosX + 7,4 + ButtonPosY + (pos * 2));
                Console.Write('>');
            }
        }
        private static void DefaultText()
        {
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;
        }
    }
}
