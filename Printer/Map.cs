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
        private static ConsoleColor DefaultForeground = ConsoleColor.Gray;
        private static ConsoleColor DefaultBackground = ConsoleColor.Black;
        public static void PrintMap(Level level, Player player)
        {
            if (Game.FogOfWar.On)
            {
                MapLayoutFOW(level, player);
            }
            else
            {
                MapLayout(level, player);
            }
        }
        public static void MapLayout(Level level, Player player)
        {
            for (int i = 0; i < level.MapLength.Y; i++)
            {
                for (int j = 0; j < level.MapLength.X; j++)
                {
                    Console.SetCursorPosition(UI.MapBox.PosX + 2 + j, UI.MapBox.PosY + 1 + i);
                    switch (level.Map[i, j])
                    {
                        case Tile.Wall:
                            Wall();
                            break;
                        case Tile.Enemy:
                            if (level.Dor != null)
                            {
                                if (player.Pos.X == j && player.Pos.Y == i)
                                    Avatar(player);
                                else if (level.Dor.Pos.Y == i && level.Dor.Pos.X == j)
                                    Enemy(level.Dor);
                            }
                            foreach (Enemy enemy in level.Enemies)
                            {
                                if (enemy.Pos.X == j && enemy.Pos.Y == i)
                                {
                                    if (player.Pos.X == j && player.Pos.Y == i)
                                        Avatar(player);
                                    else
                                        Enemy(enemy);
                                }
                            }
                            break;
                        case Tile.Player:
                            Avatar(player);
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
                        case Tile.Computer:
                            Computer();
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
                Console.WriteLine();
            }
            
        }
        public static void MapLayoutFOW(Level level, Player player)
        {
            for (int i = 0; i < level.MapLength.Y; i++)
            {
                for (int j = 0; j < level.MapLength.X; j++)
                {
                    Console.SetCursorPosition(UI.MapBox.PosX + 2 + j, UI.MapBox.PosY + 1 + i);
                    switch (level.Map[i, j])
                    {
                        case Tile.Wall:
                            if (InRange(player, i, j))
                                Wall();
                            break;
                        case Tile.Enemy:
                            if (level.Dor != null)
                            {
                                if (InRange(player, i, j))
                                {
                                    if (player.Pos.X == j && player.Pos.Y == i)
                                        Avatar(player);
                                    else if (level.Dor.Pos.Y == i && level.Dor.Pos.X == j)
                                        Enemy(level.Dor);
                                }
                            }
                            foreach (Enemy enemy in level.Enemies)
                            {
                                if (enemy.Pos.X == j && enemy.Pos.Y == i)
                                {
                                    if (InRange(player, i, j))
                                    {
                                        if (player.Pos.X == j && player.Pos.Y == i)
                                            Avatar(player);
                                        else
                                            Enemy(enemy);
                                    }
                                    else 
                                        Empty();
                                }
                            }
                            break;
                        case Tile.Player:
                            Avatar(player);
                            break;
                        case Tile.Chest:
                            if (InRange(player, i, j))
                                Chest();
                            else
                                Empty();
                            break;
                        case Tile.Trap:
                            foreach (Trap trap in level.Traps)
                            {
                                if (trap.Pos.X == j && trap.Pos.Y == i)
                                {
                                    if (InRange(player, i, j))
                                        Trap(trap);
                                    else
                                        Empty();
                                }
                            }
                            break;
                        case Tile.Coin:
                            if (InRange(player, i, j))
                                Coin();
                            else
                                Empty();
                            break;
                        case Tile.Computer:
                            if (InRange(player, i, j))
                                Computer();
                            else
                                Empty();
                            break;
                        case Tile.Key:
                            foreach (Key key in level.Keys)
                            {
                                if (key.Pos.X == j && key.Pos.Y == i)
                                {
                                    if (InRange(player, i, j))
                                        Key(key);
                                    else
                                        Empty();
                                }
                            }
                            break;
                        case Tile.Door:
                            foreach (Door door in level.Doors)
                            {
                                if (door.Pos.X == j && door.Pos.Y == i)
                                {
                                    if (InRange(player, i, j))
                                        Door(door);
                                    else
                                        Empty();
                                }
                            }
                            break;
                        case Tile.Entry:
                            if (InRange(player, i, j))
                                Entry();
                            else
                                Empty();
                            break;
                        case Tile.Exit:
                            if (InRange(player, i, j))
                                Exit();
                            else
                                Empty();
                            break;
                        case Tile.Empty:
                            if (InRange(player, i, j))
                                EmptyVisible();
                            else
                                Empty();
                            break;
                    }
                }
                Console.WriteLine();
            }

        }
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
        public static bool InRange(Player player, int i, int j)
        {
            if (player.Pos.Y - Game.FogOfWar.Height < i && i < player.Pos.Y + Game.FogOfWar.Height)
            {
                if (player.Pos.X - Game.FogOfWar.Width < j && j < player.Pos.X + Game.FogOfWar.Width)
                {
                    return true;
                }
            }
            return false;
        }
        public static void Wall()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("■");
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;
        }
        public static void Avatar(Player player)
        {
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = player.Color;
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
        public static void Enemy(DBD dor)
        {
            if (dor.Activated)
            {
                if (dor.IsDead())
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("☻");
                    Console.BackgroundColor = DefaultBackground;
                    Console.ForegroundColor = DefaultForeground;
                    return;
                }
                Console.ForegroundColor = dor.Color;
                Console.Write("☻");
                Console.ForegroundColor = DefaultForeground;
            }
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
        public static void Computer()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("■");
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
        public static void EmptyVisible()
        {
            Console.Write(".");
        }
    }
}
