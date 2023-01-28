using ConsoleDungeonCrawler.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Level_Elements
{
    
    class Chest
    {
        public Vector2 Pos = new Vector2();
        
        public ItemType RewardType(Player player)
        {
            if (player.PlayerWeapons.Count -1 == player.WeaponCap)
            {
                if (player.PlayerPotions.Count == player.PotionCap)
                {
                    if (player.PlayerArmors.Count - 1 == player.ArmorCap)
                        return ItemType.Coin;
                }
            }
            if (player.PlayerWeapons.Count -1 == player.WeaponCap)
            {
                if (player.PlayerPotions.Count == player.PotionCap)
                {
                    float random = Random.Shared.NextSingle();
                    if (random < 0.1f) return ItemType.Coin;
                    else if (random < 0.45f) return ItemType.Armor;
                    else if (random < 0.75f) return ItemType.Armor;
                    else return ItemType.Coin;
                }
                else if (player.PlayerArmors.Count - 1  == player.ArmorCap)
                {
                    float random = Random.Shared.NextSingle();
                    if (random < 0.1f) return ItemType.Coin;
                    else if (random < 0.45f) return ItemType.Coin;
                    else if (random < 0.75f) return ItemType.Potion;
                    else return ItemType.Potion;
                }
                else
                {
                    float random = Random.Shared.NextSingle();
                    if (random < 0.1f) return ItemType.Coin;
                    else if (random < 0.45f) return ItemType.Potion;
                    else if (random < 0.75f) return ItemType.Armor;
                    else return ItemType.Potion;
                }
            }
            else if (player.PlayerPotions.Count == player.PotionCap)
            {
                if (player.PlayerWeapons.Count - 1 == player.WeaponCap)
                {
                    float random = Random.Shared.NextSingle();
                    if (random < 0.1f) return ItemType.Coin;
                    else if (random < 0.45f) return ItemType.Armor;
                    else if (random < 0.75f) return ItemType.Armor;
                    else return ItemType.Coin;
                }
                else if (player.PlayerArmors.Count - 1 == player.ArmorCap)
                {
                    float random = Random.Shared.NextSingle();
                    if (random < 0.1f) return ItemType.Coin;
                    else if (random < 0.45f) return ItemType.Weapon;
                    else if (random < 0.75f) return ItemType.Weapon;
                    else return ItemType.Coin;
                }
                else
                {
                    float random = Random.Shared.NextSingle();
                    if (random < 0.1f) return ItemType.Coin;
                    else if (random < 0.45f) return ItemType.Weapon;
                    else if (random < 0.75f) return ItemType.Armor;
                    else return ItemType.Coin;
                }
            }
            else if (player.PlayerArmors.Count - 1 == player.ArmorCap)
            {
                if (player.PlayerWeapons.Count - 1 == player.WeaponCap)
                {
                    float random = Random.Shared.NextSingle();
                    if (random < 0.1f) return ItemType.Coin;
                    else if (random < 0.45f) return ItemType.Potion;
                    else if (random < 0.75f) return ItemType.Potion;
                    else return ItemType.Coin;
                }
                else if (player.PlayerPotions.Count == player.PotionCap)
                {
                    float random = Random.Shared.NextSingle();
                    if (random < 0.1f) return ItemType.Coin;
                    else if (random < 0.45f) return ItemType.Weapon;
                    else if (random < 0.75f) return ItemType.Weapon;
                    else return ItemType.Coin;
                }
                else
                {
                    float random = Random.Shared.NextSingle();
                    if (random < 0.1f) return ItemType.Coin;
                    else if (random < 0.45f) return ItemType.Weapon;
                    else if (random < 0.75f) return ItemType.Potion;
                    else return ItemType.Potion;
                }
            }
            else
            {
                float random = Random.Shared.NextSingle();
                if (random < 0.1f) return ItemType.Coin;
                else if (random < 0.45f) return ItemType.Weapon;
                else if (random < 0.75f) return ItemType.Armor;
                else return ItemType.Potion;
            }
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
