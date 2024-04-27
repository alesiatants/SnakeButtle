using PluginInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BotHuntingSnake
{
    public class BotHuntingSnake : ISmartSnake
    {
        public string Name { get; set; } = "HuntingSnake";
        public Move Direction { get; set; }
        public bool Reverse { get; set; }
        public Color Color { get; set; } = Color.Blue;

        private Size size = Size.Empty;
        private int[,] stones = null;

        private static Random rnd = new Random();

        public void Startup(Size size, List<Point> stones)
        {
            this.size = size;
            this.stones = new int[size.Width, size.Height];

            foreach (var p in stones)
            {
                this.stones[p.X, p.Y] = 1;
            }
        }

        public void Update(Snake snake, List<Snake> enemies, List<Point> food, List<Point> dead)
        {                    
            var grid = (int[,])stones.Clone();

            foreach (var point in snake.Tail)
            {
                grid[point.X, point.Y] = 1;
            }

            foreach (var point in dead)
            {
                grid[point.X, point.Y] = 1;
            }

            foreach (var enemy in enemies)
            {
                foreach (var point in enemy.Tail)
                {
                    grid[point.X, point.Y] = 1;
                }
            }                

            var prioritet = Point.Empty;

            if (snake.Tail.Count >= FindAverageSnakesLength(enemies) * 1.3)
            {
                prioritet = FindClosestHead(snake.Position, enemies);                
            }

            if (snake.Tail.Count < FindAverageSnakesLength(enemies) * 1.3 || enemies.Count == 0)
            {
                prioritet = FindClosestFood(snake.Position, food);
            }

            if (prioritet != Point.Empty)
            {
                var path = FindPath(grid, snake.Position, prioritet);

                if (path.Count > 0)
                {
                    Direction = PointToDirection(snake.Position, path.First());
                }
                else
                {
                    prioritet = Point.Empty;
                }
            }
            else
            {
                Direction = (Move)rnd.Next(1, 5);
            }

            if (GetAdjacentPoints(snake.Position).All(obj => grid[obj.X, obj.Y] != 0))
            {
                Reverse = true;
            }
        }

        public Move PointToDirection(Point start, Point target)
        {
            var dx = start.X - target.X;
            var dy = start.Y - target.Y;

            if (dx > 0)
            {
                return Move.Left;
            }

            if (dx < 0)
            {
                return Move.Right;
            }

            if (dy > 0)
            {
                return Move.Up;
            }

            if (dy < 0)
            {
                return Move.Down;
            }

            return Move.Nothing;
        }     
        
        public Point FindClosestFood(Point position, List<Point> food)
        {
            var dict = new Dictionary<Point, double>();

            foreach (var p in food)
            {
                dict.Add(p, GetDistance(position, p));
            }

            var sorted = dict.OrderBy(o => o.Value);

            if (sorted.Count() > 0)
            {
                return sorted.First().Key;
            }

            return Point.Empty;
        }

        public Point FindClosestHead(Point position, List<Snake> enemies)
        {
            var dict = new Dictionary<Point, double>();

            foreach (var enemy in enemies)
            {
                dict.Add(enemy.Position, GetDistance(position, enemy.Position));              
            }

            var sorted = dict.OrderBy(obj => obj.Value);

            if (sorted.Count() > 0)
            {
                return sorted.First().Key;
            }

            return Point.Empty;
        }

        public double FindAverageSnakesLength(List<Snake> snakes)
        {
            if (snakes.Count > 0)
                return snakes.Average(obj => obj.Tail.Count());

            return 0;
        }

        public List<Point> FindPath(int[,] grid, Point start, Point target)
        {
            var current = start;
            var path = new List<Point>();
            var queue = new Queue<Point>();                    
            var wave = new int[grid.GetLength(0), grid.GetLength(1)];

            queue.Enqueue(start);
            wave[current.X, current.Y] = 1;

            while (queue.Count > 0)
            {                
                current = queue.Dequeue();

                foreach (var p in GetAdjacentPoints(current))
                {
                    if (grid[p.X, p.Y] == 0 && wave[p.X, p.Y] == 0)
                    {
                        wave[p.X, p.Y] = wave[current.X, current.Y] + 1;
                        queue.Enqueue(p);
                    }
                }

                if (current == target)
                {
                    path.Add(current);

                    while (current != start)
                    {
                        var dict = new Dictionary<Point, int>();

                        foreach (var p in GetAdjacentPoints(current))
                        {
                            if (wave[p.X, p.Y] != 0 && !dict.ContainsKey(p))
                            {
                                dict.Add(p, wave[p.X, p.Y]);
                            }
                        }

                        current = dict.OrderBy(v => v.Value).First().Key;
                        path.Add(current);
                    }

                    break;
                }                
            }

            path.Reverse();

            if (path.Count > 1)
            {
                path.RemoveAt(0);
            }

            return path;
        }

        public double GetDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow((point1.X - point2.X), 2) + Math.Pow((point1.Y - point2.Y), 2));
        }

        public List<Point> GetAdjacentPoints(Point position)
        {
            return new List<Point>
            {
                new Point(position.X, position.Y + 1),
                new Point(position.X + 1, position.Y),
                new Point(position.X, position.Y - 1),
                new Point(position.X - 1, position.Y)
            }.Where(arg => arg.X < size.Width && arg.X >= 0 && arg.Y < size.Height && arg.Y >= 0).ToList();
        }
    }
}
