// using System;
// namespace MyPathFinding
// {

//     public class PathNode
//     {   
//         // First change to IGrid
//         private Grid<PathNode> _pathNodeGrid;
//         public int X{ get; set; }
//         public int Y{ get; set; }
//         public int GCost { get; set; }
//         public int HCost {set; get;}
//         public int FCost {set; get;}

//         public PathNode CameFromNode{ get; set; }

//         public bool IsWalkAble { get; set; }

//         public PathNode(Grid<PathNode> pathNodeGrid, int x, int y)
//         {
//             _pathNodeGrid = pathNodeGrid;
//             this.X = x;
//             this.Y = y;
//             IsWalkAble = true;
//         }

//         public void CalculateFCost()
//         {
//             FCost = HCost + GCost;
//         }

//         public override string ToString()
//         {
//             return X + "," + Y;
//         }


//     }
// }