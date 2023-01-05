using ConsoleDungeonCrawler.Character;
using ConsoleDungeonCrawler.Level_Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Printer
{
    static class Map
    {
        private const ConsoleColor DefaultForeground = ConsoleColor.Gray;
        private const ConsoleColor DefaultBackground = ConsoleColor.Black;
        public static void PrintMap(Level level, Player player)
        {
            if (Game.FogOfWar.On)
            {
                //MapClear();
                for (int i = 0; i < level.MapLength.Y; i++)
                {
                    for (int j = 0; j < level.MapLength.X; j++)
                    {
                        if (player.Pos.Y - Game.FogOfWar.Height < i && i < player.Pos.Y + Game.FogOfWar.Height)
                        {
                            if (player.Pos.X - Game.FogOfWar.Width < j && j < player.Pos.X + Game.FogOfWar.Width)
                            {
                                MapLayout(level, player, i, j);
                            }
                        }

                    }
                }
            }
            else
            {
                for (int i = 0; i < level.MapLength.Y; i++)
                {
                    for (int j = 0; j < level.MapLength.X; j++)
                    {
                        MapLayout(level, player, i, j);
                    }
                    Console.WriteLine();
                }
            }
        }
        public static void MapLayout(Level level, Player player, int i, int j)
        {
            Console.SetCursorPosition(UI.MapBox.PosX + 2 + j, UI.MapBox.PosY + 1 + i);
            switch (level.Map[i, j])
            {
                case Tile.Wall:
                    Wall();
                    break;
                case Tile.Enemy:
                    foreach (Enemy enemy in level.Enemies)
                    {
                        if(enemy.Pos.X == j && enemy.Pos.Y == i)
                        {
                            if (player.Pos.X == j && player.Pos.Y == i)
                                Avatar();
                            else
                                Enemy(enemy);
                        }
                    }
                    break;
                case Tile.Player:
                    Avatar();
                    break;
                case Tile.Chest:
                    Chest();
                    break;
                case Tile.Trap:
                    foreach (Trap trap in level.Traps)
                    {
                        if (trap.Pos.X == j && trap.Pos.Y == i)
                            Trap(trap);
                    }
                    break;
                case Tile.Coin:
                    Coin();
                    break;
                case Tile.Key:
                    foreach (Key key in level.Keys)
                    {
                        if (key.Pos.X == j && key.Pos.Y == i)
                            Key(key);
                    }
                    break;
                case Tile.Door:
                    foreach (Door door in level.Doors)
                    {
                        if (door.Pos.X == j && door.Pos.Y == i)
                            Door(door);
                    }
                    break;
                case Tile.Entry:
                    Entry();
                    break;
                case Tile.Exit:
                    Exit();
                    break;
                case Tile.Empty:
                    Empty();
                    break;
            }
        }
        //public static void PrintOuterWalls(Level level)
        //{
        //    for (int i = 0; i < level.MapLength.Y; i++)
        //    {
        //        for (int j = 0; j < level.MapLength.X; j++)
        //        {
        //            Console.SetCursorPosition(UI.MapBox.PosX + 2 + j, UI.MapBox.PosY + 1 + i);
        //            if (i == 0 || j == 0) Wall();
        //            if (i ==level.MapLength.Y-1 || j == level.MapLength.X-1) Wall();
        //        }
        //    }
        //}
        public static void MapClear()
        {
            for (int i = 0; i < UI.MapBox.Height-1; i++)
            {
                for (int j = 0; j < UI.MapBox.Width-2; j++)
                {
                    Console.SetCursorPosition(UI.MapBox.PosX + 2 + j, UI.MapBox.PosY + 1 + i);
                    Console.Write(" ");
                }
            }
        }
        public static void Wall()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("■");
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;
        }
        public static void Avatar()
        {
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("@");
            Console.ForegroundColor = DefaultForeground;

        }
        public static void Enemy(Enemy enemy)
        {
            if (enemy.IsDead())
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("☻");
                Console.BackgroundColor = DefaultBackground;
                Console.ForegroundColor = DefaultForeground;
                return;
            }
            Console.ForegroundColor = enemy.Color;
            Console.Write("☻");
            Console.ForegroundColor = DefaultForeground;
        }
        public static void Entry()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("E");
            Console.ForegroundColor = DefaultForeground;
        }
        public static void Trap(Trap trap)
        {
            if (trap.Activated)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("#");
                Console.ForegroundColor = DefaultForeground;
            }
        }
        public static void Chest()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("$");
            Console.ForegroundColor = DefaultForeground;
        }
        public static void Coin()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("♣");
            Console.ForegroundColor = DefaultForeground;
        }
        public static void Key(Key key)
        {
            Console.ForegroundColor = key.Color;
            Console.Write("¶");
            Console.ForegroundColor = DefaultForeground;
        }
        public static void Door(Door door)
        {
            Console.ForegroundColor = door.Color;
            Console.Write("▓");
            Console.ForegroundColor = DefaultForeground;
        }
        public static void Exit()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("X");
            Console.ForegroundColor = DefaultForeground;
        }
        public static void Empty()
        {
            Console.Write(" ");
        }
    }
}
