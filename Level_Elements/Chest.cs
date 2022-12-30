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
        public Weapon WeaponReward()
        {
            int rand = Random.Shared.Next(1, 3);
            switch (rand)
            {
                case 1:
                    return Game.Weapons[1];
                case 2:
                    return Game.Weapons[2];
                default:
                    break;
            }
            return Game.Weapons[0];
        }
        public ItemType RewardType()
        {
            int rand = Random.Shared.Next(1, 2);
            switch (rand)
            {
                case 1:
                    return ItemType.Weapon;
                case 2:
                    return ItemType.Armor;
                case 3:
                    return ItemType.Potion;
                case 4:
                    return ItemType.Coin;
                default:
                    break;
            }
            return ItemType.Coin;
        }
    }
}
