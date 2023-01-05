using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Level_Elements
{
    class Trap
    {
        public Vector2 Pos = new Vector2();
        public int Damage { get; private set; }
        public bool Activated { get; private set; }
        public Trap()
        {
            Damage = 2;
            Activated = false;
        }
        public void Activate()
        {
            Activated = true;
        }
    }
}
