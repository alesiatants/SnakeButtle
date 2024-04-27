using PluginInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotRandomSnake
{
    class AStar
    {
       public  static List<AStarNode> findPath(Point start, Point end, List<Point> stones, List<Point> tail, List<Snake> enemies, List<Point> dead)
        {
            List<AStarNode> open = new List<AStarNode>();
            List<AStarNode> exit = new List<AStarNode>();
            List<AStarNode> deadlock = new List<AStarNode>();
            AStarNode _start = new AStarNode { coord = start, G = 0, H = 0, parent = null };
            open.Add(_start);
            int co = 0;
            int cpov = 0;
            while (open.Count > 0)
            {
                if (open[0].coord == _start.coord) cpov++;
                if (cpov > 5000) return null ;
                co++;
                Console.WriteLine("Шаг " + co);
                AStarNode current = open[0];
              Console.WriteLine("Точка (" + current.coord.X + ", " + current.coord.Y + ")");
                if (current.coord.X == end.X && current.coord.Y == end.Y)
                {
                    List<AStarNode> path = new List<AStarNode>();
                    AStarNode cur = current;
                    while (cur != null)
                    {
                        path.Add(cur);
                        cur = cur.parent;
                    }
                    path.Reverse();
                    return path;
                }
                AStarNode one = new AStarNode { coord = new Point(current.coord.X + 1, current.coord.Y), G = 0, H = 0, parent = current };
                AStarNode two = new AStarNode { coord = new Point(current.coord.X - 1, current.coord.Y), G = 0, H = 0, parent = current };
                AStarNode three = new AStarNode { coord = new Point(current.coord.X, current.coord.Y+1), G = 0, H = 0, parent = current };
                AStarNode four = new AStarNode { coord = new Point(current.coord.X, current.coord.Y-1), G = 0, H = 0, parent = current };
                List<AStarNode> neighbour = new List<AStarNode>();
                neighbour.Add(one);
                neighbour.Add(two);
                neighbour.Add(three);
                neighbour.Add(four);

                /* foreach (Point s in stones)
             {
                 foreach (AStarNode p in neighbour)
                 {
                     if (p.coord.X==s.X && p.coord.Y == s.Y)
                     {
                         p.parent = null;
                         bool inl = false;
                         foreach (AStarNode d in deadlock) {
                             if (d.coord == p.coord) inl = true;
                         }
                         if (!inl)
                         {
                             deadlock.Add(p);
                         }

                     }
                 }
             }


                 foreach (AStarNode s in deadlock)
             {
                 foreach (AStarNode p in neighbour)
                 {

                     if (p.coord.X == s.coord.X && p.coord.Y == s.coord.Y)
                     {
                         p.parent = null;
                     }
                 }
             }
             int countd = 0;
             foreach (AStarNode p in neighbour)
             {
                 if (p.parent != null)
                 {
                     p.G = Math.Sqrt(Math.Pow((p.coord.X - current.coord.X), 2) + Math.Pow((p.coord.Y - current.coord.Y), 2));
                     p.H = Math.Sqrt(Math.Pow((end.X - p.coord.X), 2) + Math.Pow((end.Y - p.coord.Y), 2));

                 }
                 else countd++;
             }
             int countcl = 0;
             foreach(AStarNode e in exit)
             {
                 foreach (AStarNode n in neighbour)
                 {
                     if (e.coord.X == n.coord.X&&e.coord.Y==n.coord.Y) countcl++;
                 }
                 }
             if ((countd == 3 && countcl==1 || countd == 2 && countcl == 2|| countd == 1 && countcl == 3) && current.parent!=null)
             {

                 /* foreach (AStarNode p in neighbour)
                  {
                      if (p.parent == null)
                      { deadlock.Add(p); }
                  }*/
                /* bool inl = false;
                foreach(AStarNode d in deadlock)
                 {
                     if (d.coord.X == current.coord.X&&d.coord.Y==current.coord.Y) inl = true;
                 }
                if(!inl)deadlock.Add(current);
                 /*open.RemoveAt(0);
                 open.Add(current.parent);
                 exit.RemoveAt(exit.Count-1);
                 exit.RemoveAt(exit.Count - 1);
                 exit.RemoveAt(exit.Count - 1);*/
                /* exit.Clear();
                 open.Clear();
                 open.Add(new AStarNode { coord = start,G=0, H=0, parent=null });
             }
             else
             {
                 AStarNode minl = null;
                 foreach (AStarNode p in neighbour)
                 {
                     bool inl = false;
                     foreach (AStarNode c in exit)
                     {
                         if (p.parent != null && p.coord == c.coord) inl = true;

                     }
                     if (!inl) minl = p;
                 }
                         if (exit.Count > 0)
                 {

                         foreach (AStarNode p in neighbour)
                         {
                         bool inl = false;
                         foreach (AStarNode c in exit)
                         {

                             if (p.parent != null && p.coord == c.coord) inl = true;
                             /*if (p.parent != null && p.coord != c.coord)
                             {
                                 if (p.F < minl.F) minl = p;
                             }*/
                /* }
                 if (!inl) if (p.F < minl.F) minl = p;
             }
         }
         else
         {
             foreach (AStarNode p in neighbour)
             {

                 if (p.parent != null)
                 {
                     if (p.F < minl.F) minl = p;
                 }
             }
         }
         foreach (AStarNode p in neighbour)
         {
             if (p == minl)
             {
                 bool inl = false;
                 foreach(AStarNode e in exit) {
                     if (e.coord==open[0].coord) inl = true; }
                 if(!inl)exit.Add(open[0]);
                 open.Clear();
                 open.Add(p);
             }
             else
             {
                 bool inl = false;
                 if (p.parent != null)
                 {
                     foreach (AStarNode e in exit)
                     {
                         if (e.coord == p.coord) inl = true;
                     }
                     if (!inl) exit.Add(p);
                 }
             }
         }
     }*/
                //Если сосед камень - удалить из списка соседей и добавить в список deadlock, если его там еще нету
                List<AStarNode> pdel = new List<AStarNode>();
                int c = 0;
                foreach (Point s in stones)
                {
                    c = 0;
                    pdel.Clear();
                    foreach (AStarNode p in neighbour)
                       {
                   
                        bool iss = false;
                        bool ind = false;
                        if (p.coord == s)
                        {
                            iss = true;
                            if (deadlock.Count > 0)
                            {
                                foreach(AStarNode d in deadlock)
                                {
                                    if (p.coord == d.coord) ind = true;
                                }
                            }
                        }
                        if (iss) pdel.Add(p);
                        if (iss && !ind) { deadlock.Add(p); }
                        c++;
                    }
                    if (pdel.Count > 0 && neighbour.Count>0)
                    {
                        foreach (AStarNode i in pdel)
                        {
                            
                                neighbour.Remove(i);
                            
                        }
                    }

                }



                foreach (Point s in tail)
                {
                    c = 0;
                    pdel.Clear();
                    foreach (AStarNode p in neighbour)
                    {

                        bool iss = false;
                        bool ind = false;
                        if (p.coord == s)
                        {
                            iss = true;
                            if (deadlock.Count > 0)
                            {
                                foreach (AStarNode d in deadlock)
                                {
                                    if (p.coord == d.coord) ind = true;
                                }
                            }
                        }
                        if (iss) pdel.Add(p);
                        if (iss && !ind) { deadlock.Add(p); }
                        c++;
                    }
                    if (pdel.Count > 0 && neighbour.Count > 0)
                    {
                        foreach (AStarNode i in pdel)
                        {

                            neighbour.Remove(i);

                        }
                    }

                }


                foreach (Point s in dead)
                {
                    c = 0;
                    pdel.Clear();
                    foreach (AStarNode p in neighbour)
                    {

                        bool iss = false;
                        bool ind = false;
                        if (p.coord == s)
                        {
                            iss = true;
                            if (deadlock.Count > 0)
                            {
                                foreach (AStarNode d in deadlock)
                                {
                                    if (p.coord == d.coord) ind = true;
                                }
                            }
                        }
                        if (iss) pdel.Add(p);
                        if (iss && !ind) { deadlock.Add(p); }
                        c++;
                    }
                    if (pdel.Count > 0 && neighbour.Count > 0)
                    {
                        foreach (AStarNode i in pdel)
                        {

                            neighbour.Remove(i);

                        }
                    }

                }


                foreach (Snake k in enemies)
                {
                    foreach (Point s in k.Tail)
                    {
                        c = 0;
                        pdel.Clear();
                        foreach (AStarNode p in neighbour)
                        {

                            bool iss = false;
                            bool ind = false;
                            if (p.coord == s)
                            {
                                iss = true;
                                if (deadlock.Count > 0)
                                {
                                    foreach (AStarNode d in deadlock)
                                    {
                                        if (p.coord == d.coord) ind = true;
                                    }
                                }
                            }
                            if (iss) pdel.Add(p);
                            if (iss && !ind) { deadlock.Add(p); }
                            c++;
                        }
                        if (pdel.Count > 0 && neighbour.Count > 0)
                        {
                            foreach (AStarNode i in pdel)
                            {

                                neighbour.Remove(i);

                            }
                        }
                    }
                }


                /*if (pdel.Count > 0)
                {
                    foreach (int i in pdel)
                    {
                        neighbour.RemoveAt(i);
                    }
                }*/
                //Проверяем если сосед уже в списке deadlock удаляем его из списка соседей
                c = 0;
                pdel.Clear();
                if (deadlock.Count > 0)
                {
                    
                    foreach (AStarNode d in deadlock)
                    {
                        c = 0;
                        pdel.Clear();
                        bool ind = false;
                        foreach (AStarNode p in neighbour)
                        {
                            if (p.coord == d.coord) ind = true;
                            if (ind) pdel.Add(p);
                            c++;
                        }
                        if (pdel.Count > 0 && neighbour.Count > 0)
                        {
                            foreach (AStarNode i in pdel)
                            {
                                neighbour.Remove(i);
                            }
                        }


                    }
                    
                }
                /*foreach (AStarNode p in neighbour)
                {
                    bool ind = false;
                        foreach (AStarNode d in deadlock)
                        {
                            if (p.coord == d.coord) ind = true;
                        }
                    if (ind) pdel.Add(c);
                    c++;
                }
                if (pdel.Count > 0)
                {
                    foreach (int i in pdel)
                    {
                        neighbour.RemoveAt(i);
                    }
                }*/
                pdel.Clear();
                //Для оставшихся соседей, если они есть, расчитываем G и H
                if (neighbour.Count > 0)
                {
                    foreach(AStarNode n in neighbour)
                    {
                        n.G = Math.Sqrt(Math.Pow((n.coord.X - current.coord.X), 2) + Math.Pow((n.coord.Y - current.coord.Y), 2));
                        n.H = Math.Sqrt(Math.Pow((end.X - n.coord.X), 2) + Math.Pow((end.Y - n.coord.Y), 2));
                    }
                }
                //Подсчитывем количество из соседей в списке закрытых
                int counte = 0;
                if (neighbour.Count > 0)
                {
                    foreach (AStarNode n in neighbour)
                    {
                       
                        if (exit.Count > 0) {
                            foreach (AStarNode e in exit)
                            {
                                if (n.coord == e.coord) { counte++; n.parent = null; }
                            }
                                }
                    }
                    }
                //Если все соседи в списке закрытых - текущую точку добавляем в список deadlock, очищаем список закрытых и открытых точек, в список открытых добавляем начальную точку
                if (neighbour.Count == counte)
                {
                    bool ind = false;
                    if (deadlock.Count > 0)
                    {
                        foreach (AStarNode d in deadlock) {
                            if (current.coord == d.coord) ind = true;
                        }
                    }
                    if (!ind) deadlock.Add(current);
                    exit.Clear();
                    open.Clear();
                    open.Add(new AStarNode { coord = start, G = 0, H = 0, parent = null });
                }

                //Если не все закрытые - из списа не закрытых находим ближайшее к финишу и добавляем текущую точку в список закрытых а ближайшего соседа в спсиок открытых, всех других незакрытых соседей - в список закрытых
                else
                {
                    if (neighbour.Count > 0)
                    {
                        AStarNode minl = null;
                        foreach (AStarNode n in neighbour)
                        {
                            if (n.parent != null) minl = n;
                        }
                        foreach (AStarNode n in neighbour)
                        {
                            if (n.parent != null) if (n.F < minl.F) minl = n;
                        }
                        if (minl != null)
                        {
                            exit.Add(current);
                            open.Clear();
                            open.Add(minl);
                        }
                        else { return null; }
                        foreach (AStarNode n in neighbour)
                        {
                            if (n.parent != null && n != minl) exit.Add(n);
                        }
                    }
                    else { return null; }
                    }
            }
                return null;
            }



        public static List<AStarNode> findPathEnemy(Point start, Point end, List<Point> stones, List<Point> tail)
        {
            List<AStarNode> open = new List<AStarNode>();
            List<AStarNode> exit = new List<AStarNode>();
            List<AStarNode> deadlock = new List<AStarNode>();
            AStarNode _start = new AStarNode { coord = start, G = 0, H = 0, parent = null };
            open.Add(_start);
            int co = 0;
            int cpov = 0;
            while (open.Count > 0)
            {
                if (open[0].coord == _start.coord) cpov++;
                if (cpov > 5000) return null;
                co++;
                Console.WriteLine("Шаг " + co);
                AStarNode current = open[0];
                Console.WriteLine("Точка (" + current.coord.X + ", " + current.coord.Y + ")");
                if (current.coord.X == end.X && current.coord.Y == end.Y)
                {
                    List<AStarNode> path = new List<AStarNode>();
                    AStarNode cur = current;
                    while (cur != null)
                    {
                        path.Add(cur);
                        cur = cur.parent;
                    }
                    path.Reverse();
                    return path;
                }
                AStarNode one = new AStarNode { coord = new Point(current.coord.X + 1, current.coord.Y), G = 0, H = 0, parent = current };
                AStarNode two = new AStarNode { coord = new Point(current.coord.X - 1, current.coord.Y), G = 0, H = 0, parent = current };
                AStarNode three = new AStarNode { coord = new Point(current.coord.X, current.coord.Y + 1), G = 0, H = 0, parent = current };
                AStarNode four = new AStarNode { coord = new Point(current.coord.X, current.coord.Y - 1), G = 0, H = 0, parent = current };
                List<AStarNode> neighbour = new List<AStarNode>();
                neighbour.Add(one);
                neighbour.Add(two);
                neighbour.Add(three);
                neighbour.Add(four);

                //Если сосед камень - удалить из списка соседей и добавить в список deadlock, если его там еще нету
                List<AStarNode> pdel = new List<AStarNode>();
                int c = 0;
                foreach (Point s in stones)
                {
                    c = 0;
                    pdel.Clear();
                    foreach (AStarNode p in neighbour)
                    {

                        bool iss = false;
                        bool ind = false;
                        if (p.coord == s)
                        {
                            iss = true;
                            if (deadlock.Count > 0)
                            {
                                foreach (AStarNode d in deadlock)
                                {
                                    if (p.coord == d.coord) ind = true;
                                }
                            }
                        }
                        if (iss) pdel.Add(p);
                        if (iss && !ind) { deadlock.Add(p); }
                        c++;
                    }
                    if (pdel.Count > 0 && neighbour.Count > 0)
                    {
                        foreach (AStarNode i in pdel)
                        {

                            neighbour.Remove(i);

                        }
                    }

                }



                foreach (Point s in tail)
                {
                    c = 0;
                    pdel.Clear();
                    foreach (AStarNode p in neighbour)
                    {

                        bool iss = false;
                        bool ind = false;
                        if (p.coord == s)
                        {
                            iss = true;
                            if (deadlock.Count > 0)
                            {
                                foreach (AStarNode d in deadlock)
                                {
                                    if (p.coord == d.coord) ind = true;
                                }
                            }
                        }
                        if (iss) pdel.Add(p);
                        if (iss && !ind) { deadlock.Add(p); }
                        c++;
                    }
                    if (pdel.Count > 0 && neighbour.Count > 0)
                    {
                        foreach (AStarNode i in pdel)
                        {

                            neighbour.Remove(i);

                        }
                    }

                }



                /*if (pdel.Count > 0)
                {
                    foreach (int i in pdel)
                    {
                        neighbour.RemoveAt(i);
                    }
                }*/
                //Проверяем если сосед уже в списке deadlock удаляем его из списка соседей
                c = 0;
                pdel.Clear();
                if (deadlock.Count > 0)
                {

                    foreach (AStarNode d in deadlock)
                    {
                        c = 0;
                        pdel.Clear();
                        bool ind = false;
                        foreach (AStarNode p in neighbour)
                        {
                            if (p.coord == d.coord) ind = true;
                            if (ind) pdel.Add(p);
                            c++;
                        }
                        if (pdel.Count > 0 && neighbour.Count > 0)
                        {
                            foreach (AStarNode i in pdel)
                            {
                                neighbour.Remove(i);
                            }
                        }


                    }

                }
                /*foreach (AStarNode p in neighbour)
                {
                    bool ind = false;
                        foreach (AStarNode d in deadlock)
                        {
                            if (p.coord == d.coord) ind = true;
                        }
                    if (ind) pdel.Add(c);
                    c++;
                }
                if (pdel.Count > 0)
                {
                    foreach (int i in pdel)
                    {
                        neighbour.RemoveAt(i);
                    }
                }*/
                pdel.Clear();
                //Для оставшихся соседей, если они есть, расчитываем G и H
                if (neighbour.Count > 0)
                {
                    foreach (AStarNode n in neighbour)
                    {
                        n.G = Math.Sqrt(Math.Pow((n.coord.X - current.coord.X), 2) + Math.Pow((n.coord.Y - current.coord.Y), 2));
                        n.H = Math.Sqrt(Math.Pow((end.X - n.coord.X), 2) + Math.Pow((end.Y - n.coord.Y), 2));
                    }
                }
                //Подсчитывем количество из соседей в списке закрытых
                int counte = 0;
                if (neighbour.Count > 0)
                {
                    foreach (AStarNode n in neighbour)
                    {

                        if (exit.Count > 0)
                        {
                            foreach (AStarNode e in exit)
                            {
                                if (n.coord == e.coord) { counte++; n.parent = null; }
                            }
                        }
                    }
                }
                //Если все соеди в списке закрытых - текущую точку добавляем в список deadlock, очищаем список закрытых и открытых точек, в список открытых добавляем начальную точку
                if (neighbour.Count == counte)
                {
                    bool ind = false;
                    if (deadlock.Count > 0)
                    {
                        foreach (AStarNode d in deadlock)
                        {
                            if (current.coord == d.coord) ind = true;
                        }
                    }
                    if (!ind) deadlock.Add(current);
                    exit.Clear();
                    open.Clear();
                    open.Add(new AStarNode { coord = start, G = 0, H = 0, parent = null });
                }

                //Если не все закрытые - из списа не закрытых находим ближайшее к финишу и добавляем текущую точку в список закрытых а ближайшего соседа в спсиок открытых, всех других незакрытых соседей - в список закрытых
                else
                {
                    if (neighbour.Count > 0)
                    {
                        AStarNode minl = null;
                        foreach (AStarNode n in neighbour)
                        {
                            if (n.parent != null) minl = n;
                        }
                        foreach (AStarNode n in neighbour)
                        {
                            if (n.parent != null) if (n.F < minl.F) minl = n;
                        }
                        if (minl != null)
                        {
                            exit.Add(current);
                            open.Clear();
                            open.Add(minl);
                        }
                        else { return null; }
                        foreach (AStarNode n in neighbour)
                        {
                            if (n.parent != null && n != minl) exit.Add(n);
                        }
                    }
                    else { return null; }
                }
            }
            return null;
        }



    }
}
