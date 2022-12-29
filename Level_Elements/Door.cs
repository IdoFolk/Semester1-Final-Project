using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Level_Elements
{
    class Door
    {
        public Vector2 Pos = new Vector2();
        public ConsoleColor Color;
        public bool IsOpen { get; private set; } = false;
        public void Open()
        {
            if (IsOpen) return;
            IsOpen = true;
        }
    }
}
