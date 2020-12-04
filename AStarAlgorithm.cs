// using System;
// using System.Collections.Generic;
// using SplashKitSDK;

// namespace MyPathFinding
// {
//     public class AStartPathFinding : IWallManagement, IFindPath<PathNode>
//     {
//         // Mathematic value of squre root 2 multipliies by 10 becasue we work with non decimal index.
//         private const int DIAGONAL_COST = 14;

//         // Just value of 1 but multiplies by 10 becasue we work with non decimal index.
//         private const int ADJACENT_COST = 10;
//         private Grid<PathNode> _pathNodeGrid;
//         private List<PathNode> _openList;
//         private List<PathNode> _closedList;
//         private List<PathNode> _visitedList;

//         public Grid<PathNode> GetGrid()
//         {
            
//             return _pathNodeGrid;
            
//         }

//         public AStartPathFinding(int column, int row, int cellSize, Point2D point)
//         {
//             _pathNodeGrid = new Grid<PathNode>(column, row, cellSize, point, (grid, column, row) => (PathNode) Factory.CreatePathNode(grid, column, row));
//         }
//         public List<PathNode> GetVisitedPath(){
//             return _visitedList;
//         }

//         public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
//         {
//             PathNode startNode = _pathNodeGrid.GetValue(startX, startY);
//             PathNode endNode = _pathNodeGrid.GetValue(endX, endY);
//             if (startNode == null || endNode == null) return null;

//             _openList = new List<PathNode> { startNode };
//             _closedList = new List<PathNode>();
//             _visitedList = new List<PathNode>();

//             // Initialize f,g,h costs to each pathnode object in _pathNodeGrid
//             for (int x = 0; x < _pathNodeGrid.Column; x++)
//             {
//                 for (int y = 0; y < _pathNodeGrid.Row; y++)
//                 {
//                     PathNode pathNode = _pathNodeGrid.GetValue(x, y);
//                     pathNode.GCost = int.MaxValue;
//                     pathNode.CalculateFCost();
//                     pathNode.CameFromNode = null;
//                 }
//             }

//             startNode.GCost = 0;
//             startNode.HCost = CalculateDistance(startNode, endNode);
//             startNode.CalculateFCost();

//             while (_openList.Count > 0)
//             {
//                 PathNode currentLowestFCostNode = GetLowestFCostNode(_openList);
//                 if (currentLowestFCostNode == endNode)
//                 {
//                     return CalculatePath(endNode);
//                 }

//                 _openList.Remove(currentLowestFCostNode);
//                 _closedList.Add(currentLowestFCostNode);
//                 _visitedList.Add(currentLowestFCostNode);

//                 foreach (PathNode neighbour in GetNeighbourList(currentLowestFCostNode))
//                 {
//                     if (_closedList.Contains(neighbour))
//                     {
//                         continue;
//                     }

//                     if (!neighbour.IsWalkAble)
//                     {
//                         _closedList.Add(neighbour);
//                         continue;
//                     }

//                     int tGcost = currentLowestFCostNode.GCost + CalculateDistance(currentLowestFCostNode, neighbour);
//                     if (tGcost < neighbour.GCost)
//                     {
//                         neighbour.CameFromNode = currentLowestFCostNode;
//                         neighbour.GCost = tGcost;
//                         neighbour.HCost = CalculateDistance(neighbour, endNode);
//                         neighbour.CalculateFCost();

//                         if (!_openList.Contains(neighbour))
//                         {
//                             _openList.Add(neighbour);
//                         }
//                     }
//                 }
//             }
//             // searched all areas of the grid, with no path found.
//             return null;

//         }

//         private int CalculateDistance(PathNode startNode, PathNode endNode)
//         {
//             int distanceX = Math.Abs(startNode.X - endNode.X);
//             int distanceY = Math.Abs(startNode.Y - endNode.Y);
//             int remaining = Math.Abs(distanceX - distanceY);
//             return DIAGONAL_COST * Math.Min(distanceX, distanceY) + ADJACENT_COST * remaining;
//         }

//         private PathNode GetNode(int x, int y)
//         {
//             return _pathNodeGrid.GetValue(x, y);
//         }
//         private PathNode GetLowestFCostNode(List<PathNode> pathNodes)
//         {
//             PathNode lowestFcostNode = pathNodes[0];
//             for (int i = 1; i < pathNodes.Count; i++)
//             {
//                 if (pathNodes[i].FCost < lowestFcostNode.FCost)
//                 {
//                     lowestFcostNode = pathNodes[i];
//                 }
//             }
//             return lowestFcostNode;
//         }

//         private List<PathNode> CalculatePath(PathNode endNode)
//         {
//             List<PathNode> pathList = new List<PathNode>();
//             pathList.Add(endNode);
//             PathNode currenNode = endNode;
//             while (currenNode.CameFromNode != null)
//             {
//                 pathList.Add(currenNode.CameFromNode);
//                 currenNode = currenNode.CameFromNode;
//             }
//             pathList.Reverse();
//             return pathList;
//         }

//         private List<PathNode> GetNeighbourList(PathNode currentNode)
//         {
//             List<PathNode> neighbourList = new List<PathNode>();
//             if (currentNode.X - 1 >= 0)
//             {
//                 neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y));

//                 if (currentNode.Y - 1 >= 0)
//                 {
//                     neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
//                 }
//                 if (currentNode.Y + 1 < _pathNodeGrid.Row)
//                 {
//                     neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y + 1));
//                 }
//             }

//             if (currentNode.X + 1 < _pathNodeGrid.Column)
//             {
//                 neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y));

//                 if (currentNode.Y - 1 >= 0)
//                 {
//                     neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y - 1));
//                 }
//                 if (currentNode.Y + 1 < _pathNodeGrid.Row)
//                 {
//                     neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y + 1));
//                 }
//             }
//             if (currentNode.Y - 1 >= 0)
//             {
//                 neighbourList.Add(GetNode(currentNode.X, currentNode.Y - 1));
//             }
//             if (currentNode.Y + 1 < _pathNodeGrid.Row)
//             {
//                 neighbourList.Add(GetNode(currentNode.X, currentNode.Y + 1));
//             }

//             return neighbourList;

//         }

//         public void MakeWall(int x, int y)
//         {
//             PathNode wallNode = _pathNodeGrid.GetValue(x, y);
//             if (wallNode != null)
//             {
//                 wallNode.IsWalkAble = false;
//                 _closedList.Add(wallNode);
//             }
//         }
//         public void RemoveWall(int x, int y)
//         {
//             PathNode wallNode = _pathNodeGrid.GetValue(x, y);
//             if (wallNode != null)
//             {
//                 wallNode.IsWalkAble = true;
//                 _closedList.Remove(wallNode);
//             }
//         }

//     }
// }