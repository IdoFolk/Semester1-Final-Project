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
            float random = Random.Shared.NextSingle();
            if (random < 0.4f) return ItemType.Coin;
            else if (random < 0.7f) return ItemType.Weapon;
            else if (random < 0.9f) return ItemType.Armor;
            else return ItemType.Potion;
        }
        public Weapon WeaponReward()
        {
            float random = Random.Shared.NextSingle();
            for (int i = 0; i < Game.Weapons.Count; i++)
            {
                if (random < Game.Weapons[i].RareChance)
                    return Game.Weapons[i];
            }
            throw new Exception("Weapon reward error");
        }
        public Potion PotionReward()
        {
            float random = Random.Shared.NextSingle();
            for (int i = 0; i < Game.Potions.Count; i++)
            {
                if (random < Game.Potions[i].RareChance)
                    return Game.Potions[i];
            }
            throw new Exception("Potion reward error");
        }
        public Armor ArmorReward()
        {
            float random = Random.Shared.NextSingle();
            for (int i = 0; i < Game.Armors.Count; i++)
            {
                if (random < Game.Armors[i].RareChance) 
                    return Game.Armors[i];
            }
            throw new Exception("Armor reward error");
        }
        public int CoinBagReward()
        {
            int coinAmount = 0;
            float random = Random.Shared.NextSingle();
            if (random < 0.4f) coinAmount = 10;
            else if (random < 0.7f) coinAmount = 15;
            else if (random < 0.9f) coinAmount = 20;
            else if (random < 1f) coinAmount = 25;
            return coinAmount;
        }
    }
}
