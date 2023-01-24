using ConsoleDungeonCrawler.Character;
using ConsoleDungeonCrawler.Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Level_Elements
{
    class Boss
    {
        public Vector2 Pos = new Vector2();
        public ConsoleColor Color { get; private set; }
        public string Name { get; private set; }
        public int MaxHP { get; private set; }
        public int CurrentHP { get; set; }
        public int Damage { get; private set; }
        public bool Activated { get; private set; }
        public Boss(ConsoleColor color, string name, int damage, int maxHP, bool activated)
        {
            Color = color;
            MaxHP = maxHP;
            CurrentHP = MaxHP;
            Damage = damage;
            Name = name;
            Activated = activated;
        }
        public int Attack(Player player)
        {
            float hit = Random.Shared.NextSingle();
            if (hit < player.EquippedArmor.Evasion) return 0;
            return Damage;
        }
        public void TakeDamage(int damage)
        {
            CurrentHP -= damage;
            if (CurrentHP < 0) CurrentHP = 0;
        }
        public bool IsDead()
        {
            if (CurrentHP == 0)
                return true;
            return false;
        }
        public void Activate()
        {
            Activated = true;
            HUD.BossCutscene(this);
        }
    }
}
