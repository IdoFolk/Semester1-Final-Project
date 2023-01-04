using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Character
{
    class Armor
    {
        public string Name { get; private set; }
        public float Evasion { get; private set; }
        public float RareChance { get; private set; }
        public int ArmorPoints { get; private set; }
        public bool IsEquipped { get; private set; } = false;
        public Armor(string name, float evasion, int armorPoints, float rareChance)
        {
            Name = name;
            Evasion = evasion;
            ArmorPoints = armorPoints;
            RareChance = rareChance;
        }
        public Armor(Armor armor)
        {
            Name = armor.Name;
            Evasion = armor.Evasion;
            ArmorPoints = armor.ArmorPoints;
            RareChance = armor.RareChance;
        }
        public void TakeDamage(int damage)
        {
            ArmorPoints -= damage;
            if (ArmorPoints < 0) ArmorPoints = 0;
        }
        public void SetEquipped()
        {
            IsEquipped = true;
        }
        public void RemoveEquipped()
        {
            IsEquipped = false;
        }
    }
}
