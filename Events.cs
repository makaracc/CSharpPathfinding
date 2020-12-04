// using System;
// using SplashKitSDK;

// namespace MyPathFinding
// {

//     public static class Events
//     {

//         public static void TestOnSKey(object sender, OnPathChangedEventArgs onPathChangedEventArgs)
//         {
//             PathFindingGUI pathFindingGUI = (PathFindingGUI)sender;
//             pathFindingGUI.StartX = onPathChangedEventArgs.pathFinding.GetGrid().GetColumnNumber(SplashKit.MousePosition());
//             pathFindingGUI.StartY = onPathChangedEventArgs.pathFinding.GetGrid().GetRowNumber(SplashKit.MousePosition());
//             if (onPathChangedEventArgs.pathFinding.FindPath(pathFindingGUI.StartX, pathFindingGUI.StartY, pathFindingGUI.EndX, pathFindingGUI.EndY) != null)
//             {
//                 pathFindingGUI.pathNodes = onPathChangedEventArgs.pathFinding.FindPath(pathFindingGUI.StartX, pathFindingGUI.StartY, pathFindingGUI.EndX, pathFindingGUI.EndY);
//             }
//             onPathChangedEventArgs.pathFinding.GetGrid().PrintGrid();
//         }

//         public static void TestOnEKey(object sender, OnPathChangedEventArgs onPathChangedEventArgs)
//         {
//             PathFindingGUI pathFindingGUI = (PathFindingGUI)sender;
//             pathFindingGUI.EndX = onPathChangedEventArgs.pathFinding.GetGrid().GetColumnNumber(SplashKit.MousePosition());
//             pathFindingGUI.EndY = onPathChangedEventArgs.pathFinding.GetGrid().GetRowNumber(SplashKit.MousePosition());
//             if (onPathChangedEventArgs.pathFinding.FindPath(pathFindingGUI.StartX, pathFindingGUI.StartY, pathFindingGUI.EndX, pathFindingGUI.EndY) != null)
//             {
//                 pathFindingGUI.pathNodes = onPathChangedEventArgs.pathFinding.FindPath(pathFindingGUI.StartX, pathFindingGUI.StartY, pathFindingGUI.EndX, pathFindingGUI.EndY);
//             }
//             onPathChangedEventArgs.pathFinding.GetGrid().PrintGrid();
//         }

//         public static void TestOnWKey(object sender, OnWallChangedEventArgs onWallChangedEventArgs)
//         {
//             PathFindingGUI pathFindingGUI = (PathFindingGUI)sender;
//             onWallChangedEventArgs.pathFinding.GetGrid().GetRowColumnNumber(SplashKit.MousePosition(), out double x, out double y);
//             if (onWallChangedEventArgs.pathFinding.GetGrid().GetValue(SplashKit.MousePosition()) != null)
//             {
//                 if (!onWallChangedEventArgs.pathFinding.GetGrid().GetValue(SplashKit.MousePosition()).IsWalkAble)
//                 {

//                     onWallChangedEventArgs.wallManagement.RemoveWall(Convert.ToInt32(x), Convert.ToInt32(y));
//                     pathFindingGUI.pathNodes = onWallChangedEventArgs.pathFinding.FindPath(pathFindingGUI.StartX, pathFindingGUI.StartY, pathFindingGUI.EndX, pathFindingGUI.EndY);
//                     Util.FillSquare(Color.White, Convert.ToInt32(x), Convert.ToInt32(y), pathFindingGUI.CellSize);
//                 }
//                 else
//                 {
//                     onWallChangedEventArgs.wallManagement.MakeWall(Convert.ToInt32(x), Convert.ToInt32(y));
//                     pathFindingGUI.pathNodes = onWallChangedEventArgs.pathFinding.FindPath(pathFindingGUI.StartX, pathFindingGUI.StartY, pathFindingGUI.EndX, pathFindingGUI.EndY);
//                     Util.FillSquare(Color.Black, Convert.ToInt32(x), Convert.ToInt32(y), pathFindingGUI.CellSize);
//                 }
//             }
//         }

//         public static void TestOnSpace(object sender, OnPathChangedEventArgs onPathChangedEventArgs)
//         {
//             PathFindingGUI pathFindingGUI = (PathFindingGUI)sender;
//             if (onPathChangedEventArgs.pathFinding != null)
//             {
//                 for (int i = 1; i < onPathChangedEventArgs.pathFinding.GetVisitedPath().Count; i++)
//                 {
//                     PathNode searchNode = onPathChangedEventArgs.pathFinding.GetVisitedPath()[i];
//                     SplashKit.FillRectangle(Color.LightBlue, searchNode.X * pathFindingGUI.CellSize + 2, searchNode.Y * pathFindingGUI.CellSize + 2, pathFindingGUI.CellSize - 2, pathFindingGUI.CellSize - 2);
//                     if (i % 5 == 0) pathFindingGUI.window.Refresh(60);
//                 }

//                 for (int i = 1; i < pathFindingGUI.pathNodes.Count - 1; i++)
//                 {
//                     // Color Green, x is axis of pathNodes[i] in grid * cellSize, y is thr ordinate location of pathNodes[i] in grid * cellSize, with and height = cellSize
//                     Util.FillSquare(Color.Yellow, pathFindingGUI.pathNodes[i].X, pathFindingGUI.pathNodes[i].Y, pathFindingGUI.CellSize);
//                     pathFindingGUI.window.Refresh(60);
//                 }
//                 SplashKit.Delay(5000);

//             }
//         }
//     }
// }