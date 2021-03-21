using System;
namespace MyPathFinding
{

    // This is this pathnode class.
    public class PathNode
    {
        private Grid<PathNode> _pathNodeGrid;
        public int X { get; set; }
        public int Y { get; set; }
        public int GCost { get; set; }
        public int HCost { set; get; }
        public int FCost { set; get; }

        public PathNode CameFromNode { get; set; }

        public bool IsWalkAble { get; set; }

        // PathNode constructor.
        public PathNode(Grid<PathNode> pathNodeGrid, int x, int y)
        {
            _pathNodeGrid = pathNodeGrid;
            this.X = x;
            this.Y = y;
            IsWalkAble = true;
        }

        // Calculates F cost of node.
        public void CalculateFCost()
        {
            FCost = HCost + GCost;
        }


        public override string ToString()
        {
            return X + "," + Y;
        }



    }
}