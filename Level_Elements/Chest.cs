using ConsoleDungeonCrawler.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Level_Elements
{
    enum ItemType
    {
        Weapon = 1,
        Potion = 2,
        Armor = 3,
        Coin = 4
    }
    class Chest
    {
        public Vector2 Pos = new Vector2();
        
        public ItemType RewardType()
        {
            int rand = Random.Shared.Next(1, 4);
            switch (rand)
            {
                case 1:
                    return ItemType.Weapon;
                case 2:
                    return ItemType.Potion;
                case 3:
                    return ItemType.Armor;
                case 4:
                    return ItemType.Coin;
                default:
                    break;
            }
            return ItemType.Coin;
        }
        public Weapon WeaponReward()
        {
            int rand = Random.Shared.Next(1, 6);
            switch (rand)
            {
                case 1:
                    return Game.Weapons[1];
                case 2:
                    return Game.Weapons[2];
                case 3:
                    return Game.Weapons[3];
                case 4:
                    return Game.Weapons[4];
                case 5:
                    return Game.Weapons[5];
                default:
                    return Game.Weapons[1];
            }
        }
        public Potion PotionReward()
        {
            int rand = Random.Shared.Next(1, 4);
            switch (rand)
            {
                case 1:
                    return Game.Potions[0];
                case 2:
                    return Game.Potions[1];
                case 3:
                    return Game.Potions[2];
                default:
                    return Game.Potions[0];
            }
        }
        public Armor ArmorReward()
        {
            float rand = Random.Shared.NextSingle();
            for (int i = 0; i < Game.Armors.Count; i++)
            {
                if (rand < Game.Armors[i].RareChance) 
                    return Game.Armors[i];
            }
            throw new Exception("Armor reward error");
        }
    }
}
