using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Character
{
    class Weapon
    {
        public string Name { get; private set; }
        public int Damage { get; private set; }
        public int Durability { get; private set; }
        public float HitChance { get; private set; }
        public bool IsEquipped { get; private set; } = false;
        public Weapon(string name, int damage, float hitChance, int durability)
        {
            Name = name;
            Damage = damage;
            HitChance = hitChance;
            Durability = durability;
        }
        public int Attack()
        {
            int hit = Random.Shared.Next(0, 101);
            if (HitChance < hit)
                return 0;
            return Damage;
        }
        public void SetEquipped()
        {
            IsEquipped = true;
        }
        public void RemoveEquipped()
        {
            IsEquipped = false;
        }
        public void Tear()
        {
            Durability--;
            if (Durability < 0) Durability = 0;
        }
    }
}
