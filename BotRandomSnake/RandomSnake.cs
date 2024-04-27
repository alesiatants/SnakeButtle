using System;
using System.Collections.Generic;
using System.Drawing;
using PluginInterface;

namespace BotRandomSnake
{
    public class RandomSnake : ISmartSnake
    {
        public Move Direction { get; set; }
        public bool Reverse { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }

        private DateTime dt = DateTime.Now;
        private static Random rnd = new Random();
        private List<Point> _stones;

        public void Startup(Size size, List<Point> stones)
        {
            Name = "BotRandomSnake";
            Color = Color.Brown;
            _stones = stones;
        }

        public void Update(Snake snake, List<Snake> enemies, List<Point> food, List<Point> dead)
        {
            double l = 0;
            double lk = 0;
            double minl = 100;
            double minlk = 100;
            Point fl = food[0];
            int xs = snake.Position.X;
            int ys = snake.Position.Y;
            int xf = 0;
            int yf = 0;
            bool end = false;
            bool empty = true;
            foreach (Point j in food)
            {
                xf = j.X;
                yf = j.Y;
                if (snake.Tail.Count > 0)
                {
                    Point k = snake.Tail[snake.Tail.Count - 1];
                    lk = Math.Sqrt(Math.Pow((xf - k.X), 2) + Math.Pow((yf - k.Y), 2));
                }
                l = Math.Sqrt(Math.Pow((xf - xs), 2) + Math.Pow((yf - ys), 2));
                if (l < minl)
                {
                    minl = l;
                    fl = j;
                    end = false;
                }
            }
            double minle = 100;
            l = 0;
            int ltail = 0;
            int xe = 0;
            int ye = 0;
            Point el = new Point();
            if (enemies.Count > 0)
            {
                el = enemies[0].Position;
                xs = snake.Position.X;
                ys = snake.Position.Y;


                foreach (Snake s in enemies)
                {
                    xe = s.Position.X;
                    ye = s.Position.Y;

                    l = Math.Sqrt(Math.Pow((xe - xs), 2) + Math.Pow((ye - ys), 2));
                    ltail = s.Tail.Count;
                    if (l < minle && ltail < snake.Tail.Count)
                    {
                        minle = l;
                        el = s.Position;
                        end = false;
                    }
                }
                xe = el.X;
                ye = el.Y;
            }
            else minle = 1000;
            xf = fl.X;
            yf = fl.Y;
            Console.WriteLine("Snake - ("+xs + ", " +ys + ")");
            Console.WriteLine("Food - (" + xf + ", " + yf + ")");
            Console.WriteLine("Enemy - (" + xe + ", " + ye + ")");
            // print();
            List<AStarNode> list = new List<AStarNode>();
            if (minl<minle)
            {
               list = AStar.findPath(snake.Position, fl, _stones, snake.Tail, enemies, dead); }
            else {
                if(minle!=1000)
                list = AStar.findPath(snake.Position, el, _stones, snake.Tail, enemies, dead);
            }
                int c = 1;
                if (list != null)
                {
                    foreach (AStarNode s in list)
                    {
                        Console.WriteLine("(" + s.coord.X + ", " + s.coord.Y + ")");
                        if (c == list.Count) Console.WriteLine("-----------------------------------------------------");
                        c++;
                    }
                }
                if (list == null)
                {
                    Console.WriteLine("null");
                }
                bool one = true;
                bool two = true;
                bool three = true;
                bool four = true;
                foreach (Point s in _stones)
                {
                    if (s.X == xs && s.Y == ys + 1) one = false;
                    if (s.X == xs && s.Y == ys - 1) two = false;
                    if (s.X == xs + 1 && s.Y == ys) three = false;
                    if (s.X == xs - 1 && s.Y == ys) four = false;
                }
                if (!one && !two && !three || !one && !two && !four || !one && !three && !four || !two && !four && !three) Reverse = true;
                int i = 1;
                /*for (int i=1; i<list.Count; i++)
                {*/
                if (list != null)
                {
                    if (list[i].coord.X == xs && list[i].coord.Y == ys + 1)
                    {
                        if (snake.Tail.Count > 0)
                            foreach (Point t in snake.Tail) if (t.X == xs && t.Y == ys + 1) Reverse = true;
                        if(enemies.Count>0)foreach(Snake s in enemies) if(s.Position.X==xs && s.Position.Y==ys+1) Reverse = true;
                    Direction = Move.Down;
                    }
                    if (list[i].coord.X == xs && list[i].coord.Y == ys - 1)
                    {
                        if (snake.Tail.Count > 0)
                            foreach (Point t in snake.Tail) if (t.X == xs && t.Y == ys - 1) Reverse = true;
                    if (enemies.Count > 0) foreach (Snake s in enemies) if (s.Position.X == xs && s.Position.Y == ys - 1) Reverse = true;
                    Direction = Move.Up;
                    }
                    if (list[i].coord.X == xs + 1 && list[i].coord.Y == ys)
                    {
                        if (snake.Tail.Count > 0)
                            foreach (Point t in snake.Tail) if (t.X == xs + 1 && t.Y == ys) Reverse = true;
                    if (enemies.Count > 0) foreach (Snake s in enemies) if (s.Position.X == xs+1 && s.Position.Y == ys) Reverse = true;
                    Direction = Move.Right;
                    }
                    if (list[i].coord.X == xs - 1 && list[i].coord.Y == ys)
                    {
                        if (snake.Tail.Count > 0)
                            foreach (Point t in snake.Tail) if (t.X == xs - 1 && t.Y == ys) Reverse = true;
                    if (enemies.Count > 0) foreach (Snake s in enemies) if (s.Position.X == xs - 1 && s.Position.Y == ys) Reverse = true;
                    Direction = Move.Left;
                    }
                }
                else
                {
                    list = AStar.findPath(snake.Position, food[0], _stones, snake.Tail, enemies, dead);
                    if (list != null)
                    {
                        if (list[i].coord.X == xs && list[i].coord.Y == ys + 1)
                        {
                            if (snake.Tail.Count > 0)
                                foreach (Point t in snake.Tail) if (t.X == xs && t.Y == ys + 1)
                                    {
                                        Reverse = true;
                                    }
                        if (enemies.Count > 0) foreach (Snake s in enemies) if (s.Position.X == xs && s.Position.Y == ys + 1) Reverse = true;
                        Direction = Move.Down;
                        }
                        if (list[i].coord.X == xs && list[i].coord.Y == ys - 1)
                        {
                            if (snake.Tail.Count > 0)
                                foreach (Point t in snake.Tail) if (t.X == xs && t.Y == ys - 1) Reverse = true;
                        if (enemies.Count > 0) foreach (Snake s in enemies) if (s.Position.X == xs && s.Position.Y == ys - 1) Reverse = true;
                        Direction = Move.Up;
                        }
                        if (list[i].coord.X == xs + 1 && list[i].coord.Y == ys)
                        {
                            if (snake.Tail.Count > 0)
                                foreach (Point t in snake.Tail) if (t.X == xs + 1 && t.Y == ys) Reverse = true;
                        if (enemies.Count > 0) foreach (Snake s in enemies) if (s.Position.X == xs + 1 && s.Position.Y == ys) Reverse = true;
                        Direction = Move.Right;
                        }
                        if (list[i].coord.X == xs - 1 && list[i].coord.Y == ys)
                        {
                            if (snake.Tail.Count > 0)
                                foreach (Point t in snake.Tail) if (t.X == xs - 1 && t.Y == ys) Reverse = true;
                        if (enemies.Count > 0) foreach (Snake s in enemies) if (s.Position.X == xs - 1 && s.Position.Y == ys) Reverse = true;
                        Direction = Move.Left;
                        }
                    }
                }
            
                       //}
            /*int[,] grid = new int[100,100];
            bool emptyc = true;
            for(int i=0; i<100; i++)
            {
                for(int j=0; j<100; j++)
                {
                    foreach(Point s in _stones)
                    {
                        if (s.X == j && s.Y == i) emptyc = false;
                    }
                    if (emptyc) grid[i,j] = 0;
                    else grid[i, j] = 1;
                }
            }
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Console.WriteLine(grid[i, j]);
                }
            }*/
           /* List<AStarNode> list =AStar.FindPath(grid, new AStarNode(xs, ys), new AStarNode(fl.X, fl.Y));
           foreach(AStarNode a in list)
            {
                if (a.x == xs && a.y < ys) Direction = Move.Up;
                if (a.x == xs && a.y > ys) Direction = Move.Down;
                if (a.x > xs && a.y == ys) Direction = Move.Right;
                if (a.x < xs && a.y == ys) Direction = Move.Left;
            }*/
            /* bool b1 = false;
            bool b2 = false;
            bool b3 = false;
            bool b4 = false;
            foreach (Point s in _stones)
            {
                if (s.X == xs && s.Y == ys - 1) b1 = true;
                if (s.X == xs && s.Y == ys + 1) b2 = true;
                if (s.X == xs-1 && s.Y == ys) b3 = true;
                if (s.X == xs+1 && s.Y == ys) b4 = true;
            }
            if (b1 && b2 && b3 || b2 && b3 && b4 || b1 && b2 && b4 || b1 && b3 && b4) Reverse = true;
            if (b1 && b2 && b3) Direction = Move.Right;
            if (b2 && b3 && b4) Direction = Move.Up;
            if (b1 && b3 && b4) Direction = Move.Down;
            if (b1 && b2 && b4) Direction = Move.Left;*/
            /*if (yf > ys && xf < xs)
            {
                //Слева внизу
                
                foreach(Point t in snake.Tail)
                {
                    if(t.X==xs-1 && t.Y == ys)
                    {
                        Reverse = true;
                    }
                }
                Direction = Move.Left;
                
            }
            if (yf > ys && xf > xs)
            {
                //Справа внизу
                foreach (Point t in snake.Tail)
                {
                    if (t.X == xs + 1 && t.Y == ys)
                    {
                        Reverse = true;
                    }
                }
               
                Direction = Move.Right;
            }
            if (yf < ys && xf < xs)
            {
                //Слева вверху
                foreach (Point t in snake.Tail)
                {
                    if (t.X == xs - 1 && t.Y == ys)
                    {
                        Reverse = true;
                    }
                }
                Direction = Move.Left;
                
            }
            if (yf < ys && xf > xs)
            {
                //Справа вверху
                foreach (Point t in snake.Tail)
                {
                    if (t.X == xs + 1 && t.Y == ys)
                    {
                        Reverse = true;
                    }
                }
               
                Direction = Move.Right;
            }
            if (yf > ys && xf == xs)
            {
                //Снизу
                foreach (Point t in snake.Tail)
                {
                    if (t.X == xs && t.Y == ys + 1)
                    {
                        Reverse = true;
                    }
                }
                Direction = Move.Down;
            }
            if (yf < ys && xf == xs)
            {
                //Сверху
                foreach (Point t in snake.Tail)
                {
                    if (t.X == xs && t.Y == ys-1)
                    {
                        Reverse = true;
                    }
                }
                Direction = Move.Up;
            }
            if (yf == ys && xf < xs)
            {
                //Слева
                foreach (Point t in snake.Tail)
                {
                    if (t.X == xs - 1 && t.Y == ys)
                    {
                        Reverse = true;
                    }
                }
                Direction = Move.Left;
            }
            if (yf == ys && xf > xs)
            {
                //Справа
                foreach (Point t in snake.Tail)
                {
                    if (t.X == xs + 1 && t.Y == ys)
                    {
                        Reverse = true;
                    }
                }
                Direction = Move.Right;
            }*/

            // Змейка двигается в случайном направлении
            /* Direction = (Move) rnd.Next(1, 5);

             // Змейка разворачивается каждую секунду (1000мс)
             if ((DateTime.Now - dt).TotalMilliseconds > 1000)
             {
                 Reverse = true;
                 dt = DateTime.Now;
             }*/

        }
       /* public List<Point> findPath(Snake snake, Point food)
        {
              if(snake.Position.X<food.X && snake.Position.Y > food.Y)
            {
                Point v = snake.Position;
                List<Point> listv = new List<Point>();
                int countv = 0;
                Point current = v;
            
                while (v.Y != food.Y)
                {
                    bool emptyup = true;
                    foreach(Point s in _stones)
                    {
                        if (s.X == v.X && s.Y == v.Y - 1) emptyup = false;
                    }
                    if (emptyup)
                    {
                        current = new Point(v.X, v.Y-1);
                        listv.Add(current);
                        countv++;
                    }
                    else
                    {
                        bool emptyleft = true;
                        bool emptyright = true;
                        foreach (Point s in _stones)
                        {
                            if (s.X == v.X && s.Y == v.Y - 1) emptyup = false;
                        }

                    }
                }
            }
        }*/
        
        
        
        }
}