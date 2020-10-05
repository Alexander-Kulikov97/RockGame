using System;
using System.Collections.Generic;
using System.Text;

namespace RockGame.Core
{
    public class Cell
    {
        public double x { get; }
        public double y { get; }

        public Cell(double x1, double y1)
        {
            x = x1;
            y = y1;
        }

        public static Cell operator +(Cell v1, Cell v2)
        {
            return new Cell(v1.x + v2.x, v1.y + v2.y);
        }
    }
}
