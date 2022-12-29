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
        private const ConsoleColor TextColor = ConsoleColor.Gray;
        private const ConsoleColor BackgroundColor = ConsoleColor.Black;
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
            MapBox = new UIBox("Map", 30, 6, 53, 21,BackgroundColor,TextColor);
            LogBox = new UIBox("Log", 84, 6, 35, 32, BackgroundColor, TextColor);
            LevelBox = new UIBox("Level", 0, 28, 29, 10, BackgroundColor, TextColor);
            InventoryBox = new UIBox("Inventory", 0, 6, 29, 21, BackgroundColor, TextColor);
            PlayerStatBox = new UIBox("Player Stats", 30, 28, 26, 10, BackgroundColor, TextColor);
            EnemyStatBox = new UIBox("Enemy Stats", 57, 28, 26, 10, BackgroundColor, TextColor);
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
            Console.ForegroundColor = TextColor;
            Console.BackgroundColor = BackgroundColor;
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
                        Console.BackgroundColor = BackgroundColor;
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
            TextReader(20, 0, "C:\\Users\\עידו פולקמן\\Documents\\Tiltan\\Courses\\C# basics\\ConsoleDungeonCrawler\\Printer\\Templates\\GameTitle.txt");
        }
        public static void YouWin()
        {
            TextReader(25, 10, "C:\\Users\\עידו פולקמן\\Documents\\Tiltan\\Courses\\C# basics\\ConsoleDungeonCrawler\\Printer\\Templates\\YouWin.txt");
        }
        public static void YouDied()
        {
            TextReader(25, 10, "C:\\Users\\עידו פולקמן\\Documents\\Tiltan\\Courses\\C# basics\\ConsoleDungeonCrawler\\Printer\\Templates\\YouDied.txt");
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
