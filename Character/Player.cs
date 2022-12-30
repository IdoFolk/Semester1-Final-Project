using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ConsoleDungeonCrawler.Level_Elements;

namespace ConsoleDungeonCrawler.Character
{
    class Player
    {
        public string Name { get; private set; }
        public int MaxHP { get; private set; }
        public int CurrentHP { get; private set; }
        public Range AgroRange { get; private set; }
        public float Evasion { get; private set; } = 0f;
        //Inventory:
        public List<Weapon> PlayerWeapons = new List<Weapon>();
        public Weapon EquippedWeapon;
        public Vector2 Pos = new Vector2();
        public Player(string name, int hp)
        {
            Name = name;
            MaxHP = hp;
            CurrentHP = MaxHP;
            PlayerWeapons = new List<Weapon>();
            PlayerWeapons.Add(Game.Weapons[0]); //starting weapon
            EquippedWeapon = PlayerWeapons[0];
            AgroRange = new Range(10,5);
        }
        public int Attack()
        {
            return EquippedWeapon.Attack();
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
        public void Action(Level level)
        {
            ConsoleKey key = (Console.ReadKey(true).Key);
            Move(key);
            switch (level.Map[Pos.Y, Pos.X])
            {
                case Tile.Wall:
                    DontMove(key);
                    break;
                case Tile.Door:
                    TryOpenDoor(level);
                    DontMove(key);
                    break;
                case Tile.Key:
                    GetKey(level);
                    DontMove(key);
                    break;
                case Tile.Enemy:
                    FightEnemy(level);
                    DontMove(key);
                    break;
                case Tile.Chest:
                    OpenChest(level);
                    DontMove(key);
                    break;
                case Tile.Exit:
                    level.Complete();
                    break;
            }

        }
        private void Move(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                    Pos.Y -= 1;
                    break;
                case ConsoleKey.UpArrow:
                    Pos.Y -= 1;
                    break;
                case ConsoleKey.S:
                    Pos.Y += 1;
                    break;
                case ConsoleKey.DownArrow:
                    Pos.Y += 1;
                    break;
                case ConsoleKey.A:
                    Pos.X -= 1;
                    break;
                case ConsoleKey.LeftArrow:
                    Pos.X -= 1;
                    break;
                case ConsoleKey.D:
                    Pos.X += 1;
                    break;
                case ConsoleKey.RightArrow:
                    Pos.X += 1;
                    break;
            }
        }
        private void DontMove(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                    Pos.Y += 1;
                    break;
                case ConsoleKey.UpArrow:
                    Pos.Y += 1;
                    break;
                case ConsoleKey.S:
                    Pos.Y -= 1;
                    break;
                case ConsoleKey.DownArrow:
                    Pos.Y -= 1;
                    break;
                case ConsoleKey.A:
                    Pos.X += 1;
                    break;
                case ConsoleKey.LeftArrow:
                    Pos.X += 1;
                    break;
                case ConsoleKey.D:
                    Pos.X -= 1;
                    break;
                case ConsoleKey.RightArrow:
                    Pos.X -= 1;
                    break;
            }
        }
        private void FightEnemy(Level level)
        {
            for(int enemyNum = 0; enemyNum < level.Enemies.Count; enemyNum++)
            {
                if (level.Enemies[enemyNum].Pos.Y == Pos.Y && level.Enemies[enemyNum].Pos.X == Pos.X)
                    level.Combat(level.Enemies[enemyNum], enemyNum);
            }
        }
        private void OpenChest(Level level)
        {
            for (int chestNum = 0; chestNum < level.Chests.Count; chestNum++)
            {
                Chest chest = level.Chests[chestNum]; 
                if (level.Chests[chestNum].Pos.Y == Pos.Y && level.Chests[chestNum].Pos.X == Pos.X)
                {
                    Printer.HUD.OpenChestLog();
                    GetReward(chest);
                    level.Chests.RemoveAt(chestNum);
                }
            }
        }
        private void GetReward(Chest chest)
        {
            ItemType item = chest.RewardType();
            switch (item)
            {
                case ItemType.Weapon:
                    Weapon weapon = chest.WeaponReward();
                    PlayerWeapons.Add(weapon);
                    Printer.HUD.GotWeaponLog(weapon);
                    break;
                case ItemType.Potion:
                    break;
                case ItemType.Armor:
                    break;
                case ItemType.Coin:
                    break;
                default:
                    break;
            }
        }
        private void GetKey(Level level)
        {
            for (int keyNum = 0; keyNum < level.Keys.Count; keyNum++)
            {
                Key key = level.Keys[keyNum];
                if (key.Pos.X == Pos.X && key.Pos.Y == Pos.Y)
                {
                    level.PlayerKeys.Add(new Key());
                    Printer.HUD.GotKeyLog(key);
                    level.PlayerKeys[level.PlayerKeys.Count-1].Color = level.Keys[keyNum].Color;
                    level.Keys.RemoveAt(keyNum);
                }
            }
        }
        private void TryOpenDoor(Level level)
        {
            for (int doorNum = 0; doorNum < level.Doors.Count; doorNum++)
            {
                Door door = level.Doors[doorNum];
                for (int keyNum = 0; keyNum < level.PlayerKeys.Count; keyNum++)
                { 
                    Key key = level.PlayerKeys[keyNum];
                    if (door.Pos.Y == Pos.Y && door.Pos.X == Pos.X)
                    {
                        if (key.Color == door.Color)
                        {
                            level.OpenDoor(door, doorNum);
                            key.UseKey();
                            Printer.HUD.OpenDoorLog(door);
                        }
                    }
                }
            }
        }
    }
}
