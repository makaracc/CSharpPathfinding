// using System;
// using System.Collections.Generic;
// using System.Threading;
// using SplashKitSDK;
// using MyPathFinding;

// namespace MyPathFindinga
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             // Window w = new Window("Path finding", 1005, 565);
//             // // Grid<int> grid = new Grid<int>(25, 14, 40);
//             // Grid<int> grid = new Grid<int>(15, 12, 40, new Point2D{X=10,Y=30});
//             // grid.SetValue(10, 10, 34);


//             // while(! w.CloseRequested){
//             //     w.Clear(Color.White);
//             // // Point2D p1 = new Point2D{X=10,Y=0};
//             // // Point2D p2 = new Point2D{X=10,Y=30};
//             //     grid.PrintGrid();
//             //     w.Refresh(60);
//             //     SplashKit.ProcessEvents();
//             //     if(SplashKit.MouseClicked(MouseButton.LeftButton)){
//             //         grid.SetValue(SplashKit.MousePosition(), 1);
//             //         Console.WriteLine(SplashKit.MousePosition().X + ", " + SplashKit.MousePosition().Y);
//             //         Console.WriteLine(grid.GetValue(SplashKit.MousePosition()));
//             //     }
//             //     if(SplashKit.MouseClicked(MouseButton.RightButton)){
//             //         grid.SetValue(SplashKit.MousePosition(), 0);
//             //         Console.WriteLine(SplashKit.MousePosition().X + ", " + SplashKit.MousePosition().Y);
//             //         Console.WriteLine(grid.GetValue(SplashKit.MousePosition()));
//             //     }

//             // }
//             Window w = new Window("Path finding", 1005, 565);
            
//             int cellSize = 20;
//             bool IsDestinatioReached = false;
//             Point2D startPoint = new Point2D { X = 0, Y = 0 };
//             PathFinding path = new PathFinding(50, 28, cellSize, new Point2D { X = 0, Y = 0 });
//             List<PathNode> pathNodes;
//             int startX = 0, startY = 0, endX = 10, endY = 10;
//             pathNodes = path.FindPath(startX, startY, endX, endY);

//             while (!w.CloseRequested)
//             {
//                 w.Clear(Color.White);
//                 path.Grid.PrintGrid();
//                 SplashKit.ProcessEvents();

//                 SplashKit.FillRectangle(Color.Green, startX * cellSize, startY * cellSize, cellSize, cellSize);
//                 SplashKit.FillRectangle(Color.Red, endX * cellSize, endY * cellSize, cellSize, cellSize);
//                 for (int x = 0; x < path.Grid.GetColumn; x++)
//                 {
//                     for (int y = 0; y < path.Grid.GetRow; y++)
//                     {
//                         PathNode wall = path.Grid.GetValue(x, y);
//                         if (!wall.IsWalkAble)
//                         {
//                             SplashKit.FillRectangle(Color.Black, wall.x * cellSize, wall.y * cellSize, cellSize, cellSize);
//                         }

//                     }
//                 }
//                 w.Refresh();
//                 if (SplashKit.KeyTyped(KeyCode.SKey))
//                 {
//                     startX = path.Grid.GetColumnNumber(SplashKit.MousePosition());
//                     startY = path.Grid.GetRowNumber(SplashKit.MousePosition());
//                     pathNodes = path.FindPath(startX, startY, endX, endY);
//                     path.Grid.PrintGrid();
//                     Console.WriteLine("yes");
//                     w.Refresh(60);
//                 }
//                 if (SplashKit.KeyTyped(KeyCode.EKey))
//                 {
//                     endX = path.Grid.GetColumnNumber(SplashKit.MousePosition());
//                     endY = path.Grid.GetRowNumber(SplashKit.MousePosition());
//                     pathNodes = path.FindPath(startX, startY, endX, endY);
//                     path.Grid.PrintGrid();
//                     Console.WriteLine("yes");
//                     w.Refresh(60);
//                 }
//                 if (SplashKit.KeyTyped(KeyCode.WKey))
//                 {
//                     path.Grid.GetRowColumnNumber(SplashKit.MousePosition(), out double x, out double y);
//                     if (!path.Grid.GetValue(Convert.ToInt32(x), Convert.ToInt32(y)).IsWalkAble)
//                     {
//                         path.RemoveWall(Convert.ToInt32(x), Convert.ToInt32(y));
//                         pathNodes = path.FindPath(startX, startY, endX, endY);
//                         SplashKit.FillRectangle(Color.White, x * cellSize, y * cellSize, cellSize, cellSize);
//                     }
//                     else
//                     {
//                         path.MakeWall(Convert.ToInt32(x), Convert.ToInt32(y));
//                         pathNodes = path.FindPath(startX, startY, endX, endY);
//                         SplashKit.FillRectangle(Color.Black, x * cellSize, y * cellSize, cellSize, cellSize);
//                     }
//                     w.Refresh(60);
//                 }

//                 if (SplashKit.KeyTyped(KeyCode.SpaceKey))
//                 {
//                     if (pathNodes != null)
//                     {
//                         for (int i = 1; i < path.VisitedList.Count ; i++)
//                         {   PathNode searchNode = path.VisitedList[i];
//                             SplashKit.FillRectangle(Color.LightBlue, searchNode.x * cellSize+2, searchNode.y * cellSize+2, cellSize-2, cellSize-2);
//                             SplashKit.RefreshScreen(60);
//                             Thread.Sleep(1);
//                         }
//                         GC.Collect();
//                         for (int i = 1; i < pathNodes.Count - 1; i++)
//                         {   // Color Green, x is axis of pathNodes[i] in grid * cellSize, y is thr ordinate location of pathNodes[i] in grid * cellSize, with and height = cellSize
//                             SplashKit.FillRectangle(Color.Yellow, pathNodes[i].x * cellSize, pathNodes[i].y * cellSize, cellSize, cellSize); 
//                             w.Refresh(60);
//                         }
//                         // SplashKit.FillRectangle(Color.Green, startX * cellSize, startY * cellSize, cellSize, cellSize);
//                         // SplashKit.FillRectangle(Color.Red, endX * cellSize, endY * cellSize, cellSize, cellSize);  
//                         w.Refresh(60); 
//                         // for (int i = 0; i < pathNodes.Count - 1; i++)
//                         // {
//                         //     SplashKit.DrawLine(Color.Black, pathNodes[i].x * cellSize + cellSize / 2, pathNodes[i].y * cellSize + cellSize / 2, pathNodes[i + 1].x * cellSize + cellSize / 2, pathNodes[i + 1].y * cellSize + cellSize / 2);
//                         //     w.Refresh();
//                         // }
//                         IsDestinatioReached = true;
//                         if (IsDestinatioReached)
//                         {
//                             // System.Threading.Thread.Sleep(2000);
//                             SplashKit.Delay(5000);
//                         }
//                         IsDestinatioReached = false;
//                     }

//                     SplashKit.RefreshScreen(60);
//                     Console.WriteLine(SplashKit.MousePosition().X + ", " + SplashKit.MousePosition().Y);
//                 }
//             }
//         }
//     }
// }



//from pathFindingGUI
// private void TestOnSKey(object sender, EventArgs e){
        //     StartX = _pathFinding.GetGrid().GetColumnNumber(SplashKit.MousePosition());
        //         StartY = _pathFinding.GetGrid().GetRowNumber(SplashKit.MousePosition());
        //         if (_pathFinding.FindPath(StartX, StartY, EndX, EndY) != null)
        //         {
        //             pathNodes = _pathFinding.FindPath(StartX, StartY, EndX, EndY);
        //         }
        //         _pathFinding.GetGrid().PrintGrid();
        // }

        // private void TestOnEKey(object sender, EventArgs e){
        //     EndX = _pathFinding.GetGrid().GetColumnNumber(SplashKit.MousePosition());
        //         EndY = _pathFinding.GetGrid().GetRowNumber(SplashKit.MousePosition());
        //         if (_pathFinding.FindPath(StartX, StartY, EndX, EndY) != null)
        //         {
        //             pathNodes = _pathFinding.FindPath(StartX, StartY, EndX, EndY);
        //         }
        //         _pathFinding.GetGrid().PrintGrid();
        // }

        // private void TestOnWKey(object sender, EventArgs e){
        //     _pathFinding.GetGrid().GetRowColumnNumber(SplashKit.MousePosition(), out double x, out double y);
        //         if (_pathFinding.GetGrid().GetValue(SplashKit.MousePosition()) != null)
        //         {
        //             if (!_pathFinding.GetGrid().GetValue(SplashKit.MousePosition()).IsWalkAble)
        //             {

        //                 _wallManagement.RemoveWall(Convert.ToInt32(x), Convert.ToInt32(y));
        //                 pathNodes = _pathFinding.FindPath(StartX, StartY, EndX, EndY);
        //                 SplashKit.FillRectangle(Color.White, x * CellSize, y * CellSize, CellSize, CellSize);
        //             }
        //             else
        //             {
        //                 _wallManagement.MakeWall(Convert.ToInt32(x), Convert.ToInt32(y));
        //                 pathNodes = _pathFinding.FindPath(StartX, StartY, EndX, EndY);
        //                 SplashKit.FillRectangle(Color.Black, x * CellSize, y * CellSize, CellSize, CellSize);
        //             }
        //         }
        // }

        // private void TestOnSpace(object sender, EventArgs e){
        //         if (pathNodes != null)
        //         {
        //             for (int i = 1; i < _pathFinding.GetVisitedPath().Count; i++)
        //             {
        //                 PathNode searchNode = _pathFinding.GetVisitedPath()[i];
        //                 SplashKit.FillRectangle(Color.LightBlue, searchNode.X * CellSize + 2, searchNode.Y * CellSize + 2, CellSize - 2, CellSize - 2);
        //                 if (i % 5 == 0) Window.Refresh(60);
        //             }

        //             for (int i = 1; i < pathNodes.Count - 1; i++)
        //             {
        //                 // Color Green, x is axis of pathNodes[i] in grid * cellSize, y is thr ordinate location of pathNodes[i] in grid * cellSize, with and height = cellSize
        //                 SplashKit.FillRectangle(Color.Yellow, pathNodes[i].X * CellSize, pathNodes[i].Y * CellSize, CellSize, CellSize);
        //                 Window.Refresh(60);
        //             }
        //             SplashKit.Delay(5000);
                   
        //         }
        // }
