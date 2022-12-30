using ConsoleDungeonCrawler.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Level_Elements
{
    enum Direction
    {
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }
    class Enemy
    {
        public Vector2 Pos = new Vector2();
        public string Name;
        public int Id;
        public ConsoleColor Color;
        public int MaxHP;
        public int CurrentHP;
        public int Damage;
        public float ChaseChance;
        public void TakeDamage(int damage)
        {
            CurrentHP -= damage;
            if (CurrentHP < 0) CurrentHP = 0;
        }
        public int Attack(Player player)
        {
            int hit = Random.Shared.Next(1, 101);
            if (hit < player.Evasion) return 0;
            return Damage;
        }
        public bool IsDead()
        {
            if (CurrentHP == 0)
                return true;
            return false;
        }
        public Direction MovePattern(Level level, Player player)
        {
            Direction move;
            int rand = Random.Shared.Next(1, 101);
            if (rand < ChaseChance)
            {
                move = ChasePlayer(player);
                Move(move);
                Interaction(level, player, move);
            }
            else
            {
                move = MoveRandom();
                Move(move);
                Interaction(level, player, move);
            }
            return move;
        }
        public bool IsClose(Player player)
        {
            if (player.Pos.Y - player.AgroRange.Height < Pos.Y && Pos.Y < player.Pos.Y + player.AgroRange.Height)
            {
                if (player.Pos.X - player.AgroRange.Width < Pos.X && Pos.X < player.Pos.X + player.AgroRange.Width)
                {
                    return true;
                }
            }
            return false;
        }
        public Direction ChasePlayer(Player player)
        {
            int rand = Random.Shared.Next(1,3);
            switch (rand)
            {
                case 1:
                    if (Pos.Y > player.Pos.Y) return Direction.Up;
                    else if (Pos.Y < player.Pos.Y) return Direction.Down;
                    else if (Pos.X > player.Pos.X) return Direction.Left;
                    else if (Pos.X < player.Pos.X) return Direction.Right;
                    break;
                case 2:
                    if (Pos.X > player.Pos.X) return Direction.Left;
                    else if (Pos.X < player.Pos.X) return Direction.Right;
                    else if (Pos.Y > player.Pos.Y) return Direction.Up;
                    else if (Pos.Y < player.Pos.Y) return Direction.Down;
                    break;
            }
            Console.Clear();
            Console.WriteLine("Enemy ChasePlayer Error...");
            Environment.Exit(0);
            return Direction.Up;
        }
        public Direction MoveRandom()
        {
            int direction = Random.Shared.Next(1, 5);
            return (Direction)direction;
        }
        public void Interaction(Level level,Player player, Direction direction)
        {
            switch (level.Map[Pos.Y, Pos.X])
            {
                case Tile.Wall:
                    DontMove((Direction)direction);
                    break;
                case Tile.Door:
                    DontMove((Direction)direction);
                    break;
                case Tile.Key:
                    DontMove((Direction)direction);
                    break;
                case Tile.Enemy:
                    DontMove((Direction)direction);
                    break;
                case Tile.Player:
                    //FightPlayer(level, player);
                    DontMove((Direction)direction);
                    break;
                case Tile.Chest:
                    DontMove((Direction)direction);
                    break;
                case Tile.Entry:
                    DontMove((Direction)direction);
                    break;
                case Tile.Exit:
                    DontMove((Direction)direction);
                    break;
            }
        }
        public void Move(Direction input)
        {
            switch (input)
            {
                case Direction.Up:
                    Pos.Y -= 1;
                    break;
                case Direction.Down:
                    Pos.Y += 1;
                    break;
                case Direction.Left:
                    Pos.X -= 1;
                    break;
                case Direction.Right:
                    Pos.X += 1;
                    break;
            }
        }
        public void DontMove(Direction input)
        {
            switch (input)
            {
                case Direction.Up:
                    Pos.Y += 1;
                    break;
                case Direction.Down:
                    Pos.Y -= 1;
                    break;
                case Direction.Left:
                    Pos.X += 1;
                    break;
                case Direction.Right:
                    Pos.X -= 1;
                    break;
            }
        }
        
        
    }
}
