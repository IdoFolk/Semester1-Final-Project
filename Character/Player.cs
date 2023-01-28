using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ConsoleDungeonCrawler.Level_Elements;
using ConsoleDungeonCrawler.Printer;

namespace ConsoleDungeonCrawler.Character
{
    class Player
    {
        public Vector2 Pos = new Vector2();
        public Range AgroRange { get; private set; }
        public string Name { get; private set; }
        public ConsoleColor Color { get; private set; }
        public int MaxHP { get; private set; }
        public int CurrentHP { get; private set; }
        //Inventory:
        public List<Weapon> PlayerWeapons = new List<Weapon>();
        public List<Potion> PlayerPotions = new List<Potion>();
        public List<Armor> PlayerArmors = new List<Armor>();
        public Armor EquippedArmor { get; private set; }
        public Armor DefaultArmor { get; private set; }
        public Weapon EquippedWeapon { get; private set; }
        public Weapon DefaultWeapon { get; private set; }
        public int WeaponCap { get; private set; }
        public int PotionCap { get; private set; }
        public int ArmorCap { get; private set; }
        public int Coins { get; private set; }
        public bool HasScript { get; private set; } = false;
        public bool WeaponBreakState;
        public bool ArmorBreakState;
        public bool ItemCapState;
        public Player(string name,ConsoleColor color, int hp)
        {
            Name = name;
            Color = color;
            MaxHP = hp;
            CurrentHP = MaxHP;
            DefaultWeapon = Game.Weapons[0];
            PlayerWeapons.Add(DefaultWeapon); //starting weapon
            EquipWeapon(DefaultWeapon);
            DefaultArmor = Game.Armors[0];
            PlayerArmors.Add(DefaultArmor); //starting armor
            EquipArmor(DefaultArmor);
            AgroRange = new Range(7,3);
            WeaponCap = 6;
            PotionCap = 8;
            ArmorCap = 3;
        }
        public int Attack()
        {
            if (Inventory.IsOpen) return 0;
            int attack = EquippedWeapon.Attack();
            if (attack == 0) return attack;
            EquippedWeapon.Tear();
            if (EquippedWeapon.Durability == 0)
            {
                if (EquippedWeapon == DefaultWeapon)
                    return attack;
                WeaponBreak(EquippedWeapon);
                HUD.ClearInventory();
            }
            return attack;
        }
        public void TakeDamage(int damage)
        {
            if (EquippedArmor.ArmorPoints > 0)
            {
                int block = EquippedArmor.ArmorPoints;
                EquippedArmor.TakeDamage(damage);
                if (EquippedArmor.ArmorPoints == 0)
                {
                    ArmorBreak(EquippedArmor);
                    HUD.ClearInventory();
                }
                damage -= block;
                if (damage < 0) damage = 0;
            }
            CurrentHP -= damage;
            if (CurrentHP < 0) CurrentHP = 0;
        }
        public void Heal(int heal)
        {
            CurrentHP += heal;
            if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        }
        public bool IsDead()
        {
            if (CurrentHP == 0)
                return true;
            return false;
        }
        public void Action(Level level)
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            if (IsDead())
            {
                Game.LoseGame();
                return;
            }
            Pause(key, level);
            OpenInventory(key, level);
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
                case Tile.Trap:
                    StepOnTrap(level);
                    DontMove(key);
                    break;
                case Tile.Enemy:
                    if(EnemyIsDead(level)) break;
                    FightEnemy(level);
                    DontMove(key);
                    break;
                case Tile.Chest:
                    level.OpenChest();
                    DontMove(key);
                    break;
                case Tile.Coin:
                    GetCoin(level);
                    DontMove(key);
                    break;
                case Tile.Computer:
                    TryToChangeCode(level);
                    DontMove(key);
                    break;
                case Tile.Script:
                    GetScript(level);
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
        private bool EnemyIsDead(Level level)
        {
            bool isDead = false;
            foreach (Boss boss in level.Bosses)
            {
                if (boss.Pos.Y == Pos.Y && boss.Pos.X == Pos.X)
                {
                    if (boss.IsDead()) isDead = true;
                    else isDead = false;
                }
            }
            foreach (Enemy enemy in level.Enemies)
            {
                if (enemy.Pos.Y == Pos.Y && enemy.Pos.X == Pos.X)
                {
                    if (enemy.IsDead()) isDead = true;
                    else isDead = false;
                }
            }
            return isDead;
        }
        private void FightEnemy(Level level)
        {
            for(int enemyNum = 0; enemyNum < level.Enemies.Count; enemyNum++)
            {
                if (level.Enemies[enemyNum].Pos.Y == Pos.Y && level.Enemies[enemyNum].Pos.X == Pos.X)
                    level.Combat(level.Enemies[enemyNum]);
            }
            foreach (Boss boss in level.Bosses)
            {
                if (boss.Pos.Y == Pos.Y && boss.Pos.X == Pos.X)
                {
                    if (!boss.Activated) boss.Activate();
                    level.Combat(boss);
                }
            }
        }
        private void StepOnTrap(Level level)
        {
            foreach (Trap trap in level.Traps)
            {
                if (trap.Pos.X == Pos.X && trap.Pos.Y == Pos.Y)
                {
                    if (!trap.Activated)
                    {
                        trap.Activate();
                        Game.AddToStat(StatType.TrapsRevealed);
                    }
                    TakeDamage(trap.Damage);
                    if (IsDead()) Sounds.PlaySFX(Sounds.DyingSFX);
                    else if (!ArmorBreakState) Sounds.PlaySFX(Sounds.HitSFX);
                    ArmorBreakState = false;
                    HUD.SteppedOnTrapLog(trap);
                }
            }
        }
        public void GetReward(Chest chest)
        {
            ItemType item = chest.RewardType(this);
            switch (item)
            {
                case ItemType.Weapon:
                    if (PlayerWeapons.Count > WeaponCap)
                    {
                        ItemCapState = true;
                        HUD.ItemCappedLog(item);
                        return;
                    }
                    Weapon weapon = new Weapon(chest.WeaponReward());
                    PlayerWeapons.Add(weapon);
                    HUD.GotWeaponLog(weapon);
                    break;
                case ItemType.Potion:
                    if (PlayerPotions.Count > PotionCap)
                    {
                        ItemCapState = true;
                        HUD.ItemCappedLog(item);
                        return;
                    }
                    Potion potion = new Potion(chest.PotionReward());
                    PlayerPotions.Add(potion);
                    HUD.GotPotionLog(potion);
                    break;
                case ItemType.Armor:
                    if (PlayerArmors.Count > ArmorCap)
                    {
                        ItemCapState = true;
                        HUD.ItemCappedLog(item);
                        return;
                    }
                    Armor armor = new Armor(chest.ArmorReward());
                    PlayerArmors.Add(armor);
                    HUD.GotArmorLog(armor);
                    break;
                case ItemType.Coin:
                    int coinBag = chest.CoinBagReward();
                    Coins += coinBag;
                    Game.AddToStat(coinBag);
                    HUD.GotCoinLog(coinBag);
                    break;
            }
        }
        private void GetCoin(Level level)
        {
            int amount = 0;
            level.Map[Pos.Y, Pos.X] = Tile.Empty;
            float rand = Random.Shared.NextSingle();
            if (rand > 0.3f) amount++;
            if (rand > 0.6f) amount++;
            if (rand > 0.9f) amount++;
            amount++;
            Sounds.PlaySFX(Sounds.CoinSFX);
            HUD.GotCoinLog(amount);
            Coins += amount;
            Game.AddToStat(amount);
        }
        private void GetKey(Level level)
        {
            for (int keyNum = 0; keyNum < level.Keys.Count; keyNum++)
            {
                Key key = level.Keys[keyNum];
                if (key.Pos.X == Pos.X && key.Pos.Y == Pos.Y)
                {
                    bool buyKey = false;
                    if (key.InShop)
                    {
                        buyKey = HUD.BuyKey(key);
                        if (buyKey)
                        {
                            if (Coins >= key.Price)
                            {
                                Coins -= key.Price;
                                HUD.ResetCoins();
                                Sounds.PlaySFX(Sounds.BuySFX);
                            }
                            else
                            {
                                HUD.NotEnoughCoinsLog();
                                return;
                            }
                        }
                        else return;
                    }
                    level.PlayerKeys.Add(new Key(key.Color));
                    if (!buyKey) Sounds.PlaySFX(Sounds.KeySFX);
                    Printer.HUD.GotKeyLog(key);
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
                            Sounds.PlaySFX(Sounds.DoorSFX);
                        }
                    }
                }
            }
        }
        private void OpenInventory(ConsoleKey key, Level level)
        {
            if (key == ConsoleKey.I)
            {
                Sounds.PlaySFX(Sounds.OpenInventorySFX);
                Inventory.MenuNav(this, level);
            }
        }
        public void EquipWeapon(Weapon weapon)
        {
            foreach (Weapon otherWeapon in PlayerWeapons)
            {
                otherWeapon.RemoveEquipped();
            }
            EquippedWeapon = weapon;
            weapon.SetEquipped();
            HUD.ClearPlayerStats();
        }
        private void WeaponBreak(Weapon weapon)
        {
            EquipWeapon(DefaultWeapon);
            HUD.WeaponBreakLog(weapon);
            PlayerWeapons.Remove(weapon);
            WeaponBreakState = true;
        }
        public void EquipArmor(Armor armor)
        {
            foreach (Armor otherArmor in PlayerArmors)
            {
                otherArmor.RemoveEquipped();
            }
            EquippedArmor = armor;
            armor.SetEquipped();
            HUD.ClearPlayerStats();
        }
        private void ArmorBreak(Armor armor)
        {
            EquipArmor(DefaultArmor);
            HUD.ArmorBreakLog(armor);
            PlayerArmors.Remove(armor);
            ArmorBreakState = true;
        }
        public void UsePotion(Potion potion)
        {
            Heal(potion.Heal);
            Sounds.PlaySFX(Sounds.PotionSFX);
            HUD.UsePotionLog(potion);
            PlayerPotions.Remove(potion);
        }
        public void DropWeapon(Weapon weapon)
        {
            PlayerWeapons.Remove(weapon);
            HUD.DropWeaponLog(weapon);
        }
        public void DropPotion(Potion potion)
        {
            PlayerPotions.Remove(potion);
            HUD.DropPotionLog(potion);
        }
        public void DropArmor(Armor armor)
        {
            PlayerArmors.Remove(armor);
            HUD.DropArmorLog(armor);
        }
        private void Pause(ConsoleKey key, Level level)
        {
            if(key == ConsoleKey.Escape)
                Menu.PauseMenu(level,this);
        }
        private void GetScript(Level level)
        {
            if (HasScript)
            {
                HUD.AlreadyGotScriptLog();
                return;
            }
            HasScript = true;
            level.Map[Pos.Y, Pos.X] = Tile.Empty;
            HUD.GotScriptLog();
            Sounds.PlaySFX(Sounds.ScriptSFX);
        }
        private void TryToChangeCode(Level level)
        {
            if (HasScript)
            {
                HUD.ChangeCodeLog(this);
                level.Bosses[level.Bosses.Count-1].CurrentHP = 15;
                HasScript = false;
            }
            else
                HUD.ChangeCodeLog(this);
        }

    }
}
