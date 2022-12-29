using ConsoleDungeonCrawler.Level_Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler
{
    class Key
    {
        public Vector2 Pos = new Vector2();
        public ConsoleColor Color;
        public ConsoleColor UsedColor;
        public bool Used { get; private set; } = false;
        public void UseKey()
        {
            if (Used) return;
            switch (Color)
            {
                case ConsoleColor.Blue:
                    UsedColor = ConsoleColor.DarkBlue;
                    break;
                case ConsoleColor.Green:
                    UsedColor = ConsoleColor.DarkGreen;
                    break;
                case ConsoleColor.Cyan:
                    UsedColor = ConsoleColor.DarkCyan;
                    break;
                case ConsoleColor.Red:
                    UsedColor = ConsoleColor.DarkRed;
                    break;
                case ConsoleColor.Magenta:
                    UsedColor = ConsoleColor.DarkMagenta;
                    break;
                case ConsoleColor.Yellow:
                    UsedColor = ConsoleColor.DarkYellow;
                    break;

            }
            Used = true;
        }
    }
}
