using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ConsoleDungeonCrawler.Character;

namespace ConsoleDungeonCrawler.Level_Elements
{
    enum Tile
    {
        Empty,
        Wall,
        Player,
        Enemy,
        Chest,
        Trap,
        Key,
        Door,
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
        public int Number { get; private set; }
        public Vector2 MapLength;
        public Vector2 EntryPos = new Vector2();
        public Vector2 ExitPos = new Vector2();
        public Tile[,] Map { get; private set; }
        public bool IsComplete { get; private set; } = false;
        public List<Enemy> Enemies { get; private set; }
        public List<Chest> Chests { get; private set; }
        public List<Door> Doors { get; private set; }
        public List<Key> Keys { get; private set; }
        public List<Key> PlayerKeys { get; private set; }
        public int EnemiesKilled { get; private set; }
        private Player _player;
        public Level(int levelNum, Player player)
        {
            Number = levelNum;
            _player = player;
            Enemies = new List<Enemy>();
            Chests = new List<Chest>();
            Doors = new List<Door>();
            Keys = new List<Key>();
            PlayerKeys = new List<Key>();
            MapLength = new Vector2();
            switch (levelNum)
            {
                case 1:
                    char[,] mapSeed1 = MapBuilder.ReadTextFile("C:\\Users\\עידו פולקמן\\Documents\\Tiltan\\Courses\\C# basics\\ConsoleDungeonCrawler\\Level_Elements\\Level_Presets\\Level_1.txt");
                    SetLevel(mapSeed1);
                    break;
                case 2:
                    char[,] mapSeed2 = MapBuilder.ReadTextFile("C:\\Users\\עידו פולקמן\\Documents\\Tiltan\\Courses\\C# basics\\ConsoleDungeonCrawler\\Level_Elements\\Level_Presets\\Level_2.txt");
                    SetLevel(mapSeed2);
                    
                    break;
                default:
                    Console.WriteLine("level not loaded...");
                    Environment.Exit(0);
                    break;
            }

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
                        case '$':
                            map[i, j] = Tile.Chest;
                            Chests.Add(new Chest());
                            Chests[Chests.Count - 1].Pos.Y = i;
                            Chests[Chests.Count - 1].Pos.X = j;
                            break;
                        case '▓':
                            map[i, j] = Tile.Door;
                            Doors.Add(new Door());
                            Doors[Doors.Count - 1].Pos.Y = i;
                            Doors[Doors.Count - 1].Pos.X = j;
                            break;
                        case '¶':
                            map[i, j] = Tile.Key;
                            Keys.Add(new Key(ConsoleColor.White));
                            Keys[Keys.Count - 1].Pos.Y = i;
                            Keys[Keys.Count - 1].Pos.X = j;
                            break;
                        case '☻':
                            map[i, j] = Tile.Enemy;
                            Enemies.Add(new Enemy());
                            Enemies[Enemies.Count - 1].Pos.Y = i;
                            Enemies[Enemies.Count - 1].Pos.X = j;
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
                    else if (_player.Pos.Y == i && _player.Pos.X == j) Map[i, j] = Tile.Player;
                    else Map[i, j] = Tile.Empty;
                    CheckInstances(Keys, i, j);
                    CheckInstances(Doors, i, j);
                    CheckInstances(Enemies, i, j);
                    CheckInstances(Chests, i, j);
                }
            }

        }
        public void CheckInstances(List<Enemy> enemies,int i, int j)
        {
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.Pos.Y == i && enemy.Pos.X == j)
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
            foreach (Door door in Doors)
            {
                if (door.Pos.Y == i && door.Pos.X == j)
                {
                        Map[i, j] = Tile.Door;
                }
            }
        }
        public void CheckInstances(List<Key> keys, int i, int j)
        {
            foreach (Key key in Keys)
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
        public void Combat(Enemy enemy, int enemyNum)
        {
            int playersAttack = _player.Attack();
            int enemysAttack = enemy.Attack(_player);
            enemy.TakeDamage(playersAttack);
            _player.TakeDamage(enemysAttack);
            Printer.HUD.CombatLog(_player, enemy, playersAttack);
            Printer.HUD.CombatLog(enemy, _player, enemysAttack);
            Printer.HUD.EnemyStats(enemy);
            if (enemy.IsDead())
            {
                Map[enemy.Pos.Y, enemy.Pos.X] = Tile.Empty;
                Enemies.RemoveAt(enemyNum);
                EnemiesKilled++;
            }
        }
        public void OpenDoor(Door door, int doorNum)
        {
            Map[door.Pos.Y, door.Pos.X] = Tile.Empty;
            Doors.RemoveAt(doorNum);
        }
        
    }
}
