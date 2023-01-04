using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

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
        public Weapon(Weapon weapon)
        {
            Name = weapon.Name;
            Damage = weapon.Damage;
            HitChance = weapon.HitChance;
            Durability = weapon.Durability;
        }
        public int Attack()
        {
            float hit = Random.Shared.NextSingle();
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
