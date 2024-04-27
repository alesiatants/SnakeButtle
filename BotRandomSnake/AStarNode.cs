using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotRandomSnake
{
    class AStarNode
    {
        public Point coord { get; set; }
        public AStarNode parent { get; set; }
        public double G { get; set; }
        public double H { get; set; }
        public double F { get { return this.G + this.H; } }

    }
}
