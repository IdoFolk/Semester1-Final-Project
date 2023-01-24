using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Character
{
    class Potion
    {
        public string Name { get; private set; }
        public bool Used { get; private set; }
        public int Heal { get; private set; }
        public float RareChance { get; private set; }
        public ConsoleColor Color { get; private set; }
        public Potion(ConsoleColor color, int heal, float rareChance)
        {
            Used = false;
            Color = color;
            Heal = heal;
            RareChance = rareChance;
            AssignName(color);
        }
        public Potion(Potion potion)
        {
            Used = false;
            Color = potion.Color;
            Heal = potion.Heal;
            RareChance = potion.RareChance;
            AssignName(potion.Color);
        }
        public void Use()
        {
            Used = true;
        }
        private void AssignName(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Cyan:
                    Name = "Water";
                    break;
                case ConsoleColor.Yellow:
                    Name = "Snack";
                    break;
                case ConsoleColor.Magenta:
                    Name = "Sandwich";
                    break;
            }
        }
    }
}
