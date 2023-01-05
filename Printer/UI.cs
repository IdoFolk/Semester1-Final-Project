using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Printer
{
    struct UIBox
    {
        public string Name;
        public int PosX;
        public int PosY;
        public int Width;
        public int Height;
        public ConsoleColor BackgroundColor;
        public ConsoleColor TextColor;
        public UIBox(string name, int x, int y, int width, int height, ConsoleColor backgroundColor, ConsoleColor textColor)
        {
            Name = name;
            PosX = x;
            PosY = y;
            Width = width;
            Height = height;
            BackgroundColor = backgroundColor;
            TextColor = textColor;
        }
    }
    static class UI
    {
        private const ConsoleColor DefaultForeground = ConsoleColor.Gray;
        private const ConsoleColor DefaultBackground = ConsoleColor.Black;
        public static UIBox MapBox;
        public static UIBox LogBox;
        public static UIBox LevelBox;
        public static UIBox PlayerStatBox;
        public static UIBox EnemyStatBox;
        public static UIBox InventoryBox;
        public static void GameUI()
        {
            GameTitle();
            SetBoxes();
            Box(MapBox);
            Box(LogBox);
            Box(LevelBox);
            Box(PlayerStatBox);
            Box(EnemyStatBox);
            Box(InventoryBox);
        }
        public static void SetBoxes()
        {
            MapBox = new UIBox("Map", 70, 6, 53, 21,DefaultBackground,DefaultForeground);
            LogBox = new UIBox("Log", 124, 6, 35, 32, DefaultBackground, DefaultForeground);
            LevelBox = new UIBox("Level", 40, 28, 29, 10, DefaultBackground, DefaultForeground);
            InventoryBox = new UIBox("Inventory", 40, 6, 29, 21, DefaultBackground, DefaultForeground);
            PlayerStatBox = new UIBox("Player Stats", 70, 28, 26, 10, DefaultBackground, DefaultForeground);
            EnemyStatBox = new UIBox("Enemy Stats", 97, 28, 26, 10, DefaultBackground, DefaultForeground);
        }

        public static void Box(UIBox box)
        {
            BoxOutline(box.PosX, box.PosY, box.Width, box.Height, box.BackgroundColor);
            if (box.Name == "Map") return;
            Console.BackgroundColor = box.BackgroundColor;
            Console.ForegroundColor = box.TextColor;
            Console.SetCursorPosition(box.PosX + 1, box.PosY + 1);
            Console.WriteLine($"{box.Name}:");
            Console.SetCursorPosition(box.PosX + 1, box.PosY + 2);
            Dash(box.Width - 1);
            Console.ForegroundColor = DefaultForeground;
            Console.BackgroundColor = DefaultBackground;
        }
        public static void BoxOutline(int posX, int posY, int width, int height, ConsoleColor color)
        {
            for (int i = 0; i <= height; i++)
            {
                for (int j = 0; j <= width; j++)
                {
                    Console.SetCursorPosition(j + posX, i + posY);
                    if (i == 0 && j == 0) Console.Write('┌');
                    else if (i == 0 && j == width) Console.Write('┐');
                    else if (i == height && j == 0) Console.Write('└');
                    else if (i == height && j == width) Console.Write('┘');
                    else if (i == 0) Console.Write('─');
                    else if (i == height) Console.Write('─');
                    else if (j == 0) Console.Write('|');
                    else if (j == width) Console.Write('|');
                    else
                    {
                        Console.BackgroundColor = color;
                        Console.Write(' ');
                        Console.BackgroundColor = DefaultBackground;
                    }
                }
                Console.WriteLine();
            }
        }
        public static void Dash(int width)
        {
            for (int i = 0; i < width; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
        public static void GameTitle()
        {
            TextReader(60, 0, "Templates\\GameTitle.txt");
        }
        public static void YouWin()
        {
            TextReader(25, 10, "Templates\\YouWin.txt");
        }
        public static void YouDied()
        {
            TextReader(25, 10, "Templates\\YouDied.txt");
        }
        public static void TextReader(int posX, int posY, string path)
        {
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            int i = 0;
            foreach (string line in lines)
            {
                Console.SetCursorPosition(posX, posY + i);
                Console.Write(line);
                i++;
            }
        }
    }
}
