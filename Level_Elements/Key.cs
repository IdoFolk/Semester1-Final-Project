using ConsoleDungeonCrawler.Level_Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleDungeonCrawler
{
    class Key
    {
        public Vector2 Pos = new Vector2();
        public string Name { get; private set; }
        public ConsoleColor Color { get; private set; }
        public ConsoleColor UsedColor { get; private set; }
        public bool Used { get; private set; } = false;
        public bool InShop { get; private set; } = false;
        public int Price { get; private set; } 
        public Key(ConsoleColor color)
        {
            Color = color;
            AssignUsedColor(color);
            AssignName(color);
        }
        public void UseKey()
        {
            if (Used) return;
            
            Used = true;
        }
        public void KeyInShop()
        {
            if (InShop) return;
            InShop = true;
        }
        public void SetPrice(int price)
        {
            Price = price;
        }
        public void SetColor(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Blue:
                    Color = ConsoleColor.Blue;
                    break;
                case ConsoleColor.Green:
                    Color = ConsoleColor.Green;
                    break;
                case ConsoleColor.Cyan:
                    Color = ConsoleColor.Cyan;
                    break;
                case ConsoleColor.Red:
                    Color = ConsoleColor.Red;
                    break;
                case ConsoleColor.Magenta:
                    Color = ConsoleColor.Magenta;
                    break;
                case ConsoleColor.Yellow:
                    Color = ConsoleColor.Yellow;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(color));

            }
        }
        private void AssignUsedColor(ConsoleColor color)
        {
            switch (color)
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
                case ConsoleColor.White:
                    UsedColor = ConsoleColor.Gray;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(color));

            }
        }
        private void AssignName(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Blue:
                    Name = "Blue";
                    break;
                case ConsoleColor.Green:
                    Name = "Green";
                    break;
                case ConsoleColor.Cyan:
                    Name = "Cyan";
                    break;
                case ConsoleColor.Red:
                    Name = "Red";
                    break;
                case ConsoleColor.Magenta:
                    Name = "Purple";
                    break;
                case ConsoleColor.Yellow:
                    Name = "Yellow";
                    break;
                case ConsoleColor.White:
                    Name = "Default";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(color));
            }
        }
    }
}
