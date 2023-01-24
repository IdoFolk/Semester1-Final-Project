using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ConsoleDungeonCrawler.Character;
using ConsoleDungeonCrawler.Printer;

namespace ConsoleDungeonCrawler.Level_Elements
{
    enum Tile
    {
        Empty,
        Wall,
        Player,
        Enemy,
        Chest,
        Coin,
        Trap,
        Key,
        Door,
        Computer,
        Script,
        Entry,
        Exit
    }
    struct Vector2
    {
        public int X;
        public int Y;
    }
    class Level
    {
        public string Name { get; private set; }
        public int Indicator { get; private set; }
        public Vector2 MapLength;
        public Vector2 EntryPos = new Vector2();
        public Vector2 ExitPos = new Vector2();
        public Tile[,] Map { get; private set; }
        public bool IsComplete { get; private set; } = false;
        public List<Enemy> Enemies { get; private set; }
        public List<Chest> Chests { get; private set; }
        public List<Trap> Traps { get; private set; }
        public List<Door> Doors { get; private set; }
        public List<Key> Keys { get; private set; }
        public List<Key> PlayerKeys { get; private set; }
        public List<Boss> Bosses { get; private set; }
        public int EnemiesKilled { get; private set; }
        public int EnemiesAmount { get; private set; }
        public int ChestAmount { get; private set; }
        public int ChestsOpened { get; private set; }
        private Player _player;
        public Level(int levelNum, Player player)
        {
            SetLevelName(levelNum);
            Indicator = levelNum;
            _player = player;
            Enemies = new List<Enemy>();
            Bosses = new List<Boss>();
            Chests = new List<Chest>();
            Traps = new List<Trap>();
            Doors = new List<Door>();
            Keys = new List<Key>();
            PlayerKeys = new List<Key>();
            MapLength = new Vector2();
            switch (levelNum)
            {
                case 1:
                    char[,] mapSeed1 = MapBuilder.ReadTextFile("Level_Presets\\Level_1.txt");
                    SetLevel(mapSeed1);
                    break;
                case 2:
                    char[,] mapSeed2 = MapBuilder.ReadTextFile("Level_Presets\\Level_2.txt");
                    SetLevel(mapSeed2);
                    break;
                case 3:
                    char[,] mapSeed3 = MapBuilder.ReadTextFile("Level_Presets\\Level_3.txt");
                    SetLevel(mapSeed3);
                    break;
                case 4:
                    char[,] mapSeed4 = MapBuilder.ReadTextFile("Level_Presets\\Level_4.txt");
                    SetLevel(mapSeed4);
                    break;
                case 5:
                    char[,] mapSeed5 = MapBuilder.ReadTextFile("Level_Presets\\Level_5.txt");
                    SetLevel(mapSeed5);
                    break;
                case 6:
                    char[,] mapSeed6 = MapBuilder.ReadTextFile("Level_Presets\\Level_6.txt");
                    SetLevel(mapSeed6);
                    break;
                case 7:
                    char[,] mapSeed7 = MapBuilder.ReadTextFile("Level_Presets\\Level_7.txt");
                    SetLevel(mapSeed7);
                    break;
                case 8:
                    char[,] mapSeed8 = MapBuilder.ReadTextFile("Level_Presets\\Level_8.txt");
                    SetLevel(mapSeed8);
                    break;
                case 9:
                    char[,] mapSeed9 = MapBuilder.ReadTextFile("Level_Presets\\Level_9.txt");
                    SetLevel(mapSeed9);
                    break;
                case 10:
                    char[,] mapSeed10 = MapBuilder.ReadTextFile("Level_Presets\\Level_10.txt");
                    SetLevel(mapSeed10);
                    break;
                default:
                    Console.WriteLine("level not loaded...");
                    Environment.Exit(0);
                    break;
            }
            ChestAmount = Chests.Count;
            EnemiesAmount = Enemies.Count;
        }
        public Tile[,] LoadMap(char[,] mapSeed)
        {
            Tile[,] map = new Tile[mapSeed.GetLength(0), mapSeed.GetLength(1)];
            for (int i = 0; i < mapSeed.GetLength(0); i++)
            {
                for (int j = 0; j < mapSeed.GetLength(1); j++)
                {
                    switch (mapSeed[i, j])
                    {
                        case ' ':
                            map[i, j] = Tile.Empty;
                            break;
                        case '■':
                            map[i, j] = Tile.Wall;
                            break;
                        case '*':
                            map[i, j] = Tile.Coin;
                            break;
                        case 'E':
                            map[i, j] = Tile.Entry;
                            EntryPos.Y = i;
                            EntryPos.X = j;
                            break;
                        case 'X':
                            map[i, j] = Tile.Exit;
                            ExitPos.Y = i;
                            ExitPos.X = j;
                            break;
                        case '#':
                            map[i, j] = Tile.Trap;
                            Traps.Add(new Trap());
                            Traps[Traps.Count - 1].Pos.Y = i;
                            Traps[Traps.Count - 1].Pos.X = j;
                            break;
                        case '$':
                            map[i, j] = Tile.Chest;
                            Chests.Add(new Chest());
                            Chests[Chests.Count - 1].Pos.Y = i;
                            Chests[Chests.Count - 1].Pos.X = j;
                            break;
                        case 'D':
                            map[i, j] = Tile.Door;
                            Doors.Add(new Door());
                            Doors[Doors.Count - 1].Pos.Y = i;
                            Doors[Doors.Count - 1].Pos.X = j;
                            break;
                        case 'K':
                            map[i, j] = Tile.Key;
                            Keys.Add(new Key(ConsoleColor.White));
                            Keys[Keys.Count - 1].Pos.Y = i;
                            Keys[Keys.Count - 1].Pos.X = j;
                            break;
                        case 'O':
                            map[i, j] = Tile.Enemy;
                            Enemies.Add(new Enemy());
                            Enemies[Enemies.Count - 1].Pos.Y = i;
                            Enemies[Enemies.Count - 1].Pos.X = j;
                            break;
                        case 'B':
                            map[i, j] = Tile.Enemy;
                            if (Indicator == 4)
                            {
                                Boss avishay = new Boss(ConsoleColor.Yellow, "Avishay", 1, 10, true);
                                Bosses.Add(avishay);
                            }
                            if (Indicator == 7)
                            {
                                Boss ofir = new Boss(ConsoleColor.Magenta, "Ofir", 2, 15, true);
                                Bosses.Add(ofir);
                            }
                            if (Indicator == 10)
                            {
                                Boss dor = new Boss(ConsoleColor.Cyan, "Dor", 3, 1000, false);
                                Bosses.Add(dor);
                            }
                            Bosses[Bosses.Count-1].Pos.Y = i;
                            Bosses[Bosses.Count - 1].Pos.X = j;
                            break;
                        case 'C':
                            map[i, j] = Tile.Computer;
                            break;
                        case 'S':
                            map[i, j] = Tile.Script;
                            break;
                    }
                }
            }
            return map;
        }
        public void UpdateGrid()
        {
            for (int i = 0; i < MapLength.Y; i++)
            {
                for (int j = 0; j < MapLength.X; j++)
                {
                    if (Map[i, j] == Tile.Wall) Map[i, j] = Tile.Wall;
                    else if (Map[i, j] == Tile.Entry) Map[i, j] = Tile.Entry;
                    else if (Map[i, j] == Tile.Exit) Map[i, j] = Tile.Exit;
                    else if (Map[i, j] == Tile.Trap) Map[i, j] = Tile.Trap;
                    else if (Map[i, j] == Tile.Coin) Map[i, j] = Tile.Coin;
                    else if (Map[i, j] == Tile.Computer) Map[i, j] = Tile.Computer;
                    else if (Map[i, j] == Tile.Script) Map[i, j] = Tile.Script;
                    else if (_player.Pos.Y == i && _player.Pos.X == j) Map[i, j] = Tile.Player;
                    else Map[i, j] = Tile.Empty;
                    CheckInstances(Keys, i, j);
                    CheckInstances(Doors, i, j);
                    CheckInstances(Enemies, i, j);
                    CheckInstances(Chests, i, j);
                    CheckInstances(Bosses, i, j);
                }
            }

        }
        public void CheckInstances(List<Enemy> enemies,int i, int j)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.Pos.Y == i && enemy.Pos.X == j)
                    Map[i, j] = Tile.Enemy;
            }
        }
        public void CheckInstances(List<Boss> bosses, int i, int j)
        {
            foreach (Boss boss in bosses)
            {
                if (boss.Pos.Y == i && boss.Pos.X == j)
                    Map[i, j] = Tile.Enemy;
            }
        }
        public void CheckInstances(List<Chest> chests, int i, int j)
        {
            foreach (Chest chest in chests)
            {
                if (chest.Pos.Y == i && chest.Pos.X == j)
                    Map[i, j] = Tile.Chest;
            }
        }
        public void CheckInstances(List<Door> doors, int i, int j)
        {
            foreach (Door door in doors)
            {
                if (door.Pos.Y == i && door.Pos.X == j)
                {
                        Map[i, j] = Tile.Door;
                }
            }
        }
        public void CheckInstances(List<Key> keys, int i, int j)
        {
            foreach (Key key in keys)
            {
                if (key.Pos.Y == i && key.Pos.X == j)
                    Map[i, j] = Tile.Key;
            }
        }
        public void SetLevel(char[,] mapSeed)
        {
            Map = LoadMap(mapSeed);
            MapLength.Y = Map.GetLength(0);
            MapLength.X = Map.GetLength(1);
            _player.Pos.X = EntryPos.X;
            _player.Pos.Y = EntryPos.Y;
        }
        public void Complete()
        {
            IsComplete = true;
        }
        public void Combat(Enemy enemy)
        {
            int playersAttack = _player.Attack();
            int enemysAttack = enemy.Attack(_player);
            enemy.TakeDamage(playersAttack);
            _player.TakeDamage(enemysAttack);
            //if (playersAttack == 0 && enemysAttack == 0)
                //Sounds.MissSFX.Play();
            //else 
            Printer.HUD.CombatLog(_player, enemy, playersAttack);
            Printer.HUD.CombatLog(enemy, _player, enemysAttack);
            Printer.HUD.EnemyStats(enemy);
            if (enemy.IsDead())
            {
                Chests.Add(new Chest());
                Chests[Chests.Count - 1].Pos.Y = enemy.Pos.Y;
                Chests[Chests.Count - 1].Pos.X = enemy.Pos.X;
                EnemiesKilled++;
                ChestAmount++;
            }
            //if (_player.IsDead()) Sounds.DyingSFX.Play();
        }
        public void Combat(Boss boss)
        {
            int playersAttack = _player.Attack();
            int enemysAttack = boss.Attack(_player);
            boss.TakeDamage(playersAttack);
            _player.TakeDamage(enemysAttack);
            //if (playersAttack == 0 && enemysAttack == 0)
            //Sounds.MissSFX.Play();
            //else
            Printer.HUD.CombatLog(_player, boss, playersAttack);
            Printer.HUD.CombatLog(boss, _player, enemysAttack);
            Printer.HUD.EnemyStats(boss);
            if (boss.IsDead())
            {
                if (boss.Name == "Dor")
                    HUD.BossDefeatedCutscene();
                EnemiesKilled++;
            }
            //if (_player.IsDead()) Sounds.DyingSFX.Play();
        }
        public void OpenDoor(Door door, int doorNum)
        {
            Map[door.Pos.Y, door.Pos.X] = Tile.Empty;
            Doors.RemoveAt(doorNum);
        }
        public void OpenChest()
        {
            for (int chestNum = 0; chestNum < Chests.Count; chestNum++)
            {
                Chest chest = Chests[chestNum];
                if (Chests[chestNum].Pos.Y == _player.Pos.Y && Chests[chestNum].Pos.X == _player.Pos.X)
                {
                    Printer.HUD.OpenChestLog();
                    _player.GetReward(chest);
                    //Sounds.ChestSFX.Play();
                    Chests.RemoveAt(chestNum);
                    ChestsOpened++;
                }
            }
        }
        private void SetLevelName(int levelNum)
        {
            switch (levelNum)
            {
                case 1:
                    Name = "1 - Roof";
                    break;
                case 2:
                    Name = "2 - Library";
                    break;
                case 3:
                    Name = "3 - Magenta Class";
                    break;
                case 4:
                    Name = "4 - Yellow Class";
                    break;
                case 5:
                    Name = "5 - Vending Machine";
                    break;
                case 6:
                    Name = "6 - pedagogi";
                    break;
                case 7:
                    Name = "7 - CloverBite Studios";
                    break;
                case 8:
                    Name = "8 - Classroom Studio";
                    break;
                case 9:
                    Name = "9 - Lost Cafeteria";
                    break;
                case 10:
                    Name = "10 - Ground Floor";
                    break;
            }
        }
    }
}
