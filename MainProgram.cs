using SplashKitSDK;
using System.Threading;
using System;
using System.Collections.Generic;
namespace MyPathFinding{

    /*
        This is the Testing class which use a facade class called PathFindingGUI
        to run the program inside a while loop.

    */
    class TestingClass{
        static void Main(){
            // Using singleton GetInstance.
            PathFindingGUI pathFindingGUI = PathFindingGUI.GetInstance();
            while(!SplashKit.QuitRequested()){
                // Display the window.
                pathFindingGUI.DisplayGUI();
                // Handle events.
                pathFindingGUI.HandleEvents();
                Thread.Sleep(30);
            }
            
        }

    }

    /* This is a facade class that puts together all objects and methods
        that is needed for specific activities such as user input and window in order to run the program.
        This Class extends AbstractWindow class and implements IHandEvent interface.
    */ 
    public class PathFindingGUI:AbstractWindow, IHandleEvent
    {
        private event EventHandler<OnPathChangedEventArgs> OnSpacePressed;
        private event EventHandler<OnWallChangedEventArgs> OnWKeyPressed;
        private event EventHandler<OnPathChangedEventArgs> OnEKeyPressed;
        private event EventHandler<OnPathChangedEventArgs> OnSKeyPressed;
        private static PathFindingGUI _instance;
        private IFindPath<PathNode> _pathFinding;
        private IWallManagement _wallManagement;
        public List<PathNode> pathNodes;

        // Creates singleton
        public static PathFindingGUI GetInstance(){
            if(_instance == null){
                _instance = new PathFindingGUI();
                return _instance;
            }else{
                return _instance;
            }
        }

        // Constructor
        private PathFindingGUI():base("A* Path finding")
        {   
            _pathFinding = Factory.CreatePathFindingObject(_width/CellSize, _height/CellSize, CellSize, _startPoint);
            pathNodes = _pathFinding.FindPath(StartX, StartY, EndX, EndY);
            _wallManagement = (IWallManagement) _pathFinding;
            OnSpacePressed += Events.TestOnSpace;
            OnWKeyPressed += Events.TestOnWKey;
            OnSKeyPressed += Events.TestOnSKey;
            OnEKeyPressed += Events.TestOnEKey;
        }

        // Reset the the window name, width and height.
        public override void ResetGUI(string name, int width, int height){
            _width = width;
            _height = height;
            if(window != null) window.Close();
            window = Factory.CreateNewWindow(name, _width, _height);
            _startPoint = Factory.CreatePoint(0,0);
            StartX = _width /10 /CellSize;
            StartY = _height / 2  / CellSize;
            EndX = _width / CellSize - StartX;
            EndY = _height/ 2 / CellSize;
            _pathFinding = Factory.CreatePathFindingObject(_width/CellSize, _height/CellSize, CellSize, _startPoint);
           _wallManagement = (IWallManagement) _pathFinding;
            pathNodes = _pathFinding.FindPath(StartX, StartY, EndX, EndY);
        }
        
        // Displayd window and render everything including grid, startnode, endnode, path and walls.
        public override void DisplayGUI()
        {
            window.Clear(Color.White);
            _pathFinding.GetGrid().PrintGrid();
            // Draw StartNode.
            SplashKit.FillRectangle(Color.Green, StartX * CellSize, StartY * CellSize, CellSize, CellSize);
            // Draw EndNode.
            SplashKit.FillRectangle(Color.Red, EndX * CellSize, EndY * CellSize, CellSize, CellSize);
            // Draw Walls.
            for (int x = 0; x < _pathFinding.GetGrid().Column; x++)
            {
                for (int y = 0; y < _pathFinding.GetGrid().Row; y++)
                {
                    PathNode wall = _pathFinding.GetGrid().GetValue(x, y);
                    if (!wall.IsWalkAble)
                    {
                        SplashKit.FillRectangle(Color.Black, wall.X * CellSize, wall.Y * CellSize, CellSize, CellSize);
                    }

                }
            }
            window.Refresh();
            Thread.Sleep(10);

        }

        // Handles input accordingly.
        public void HandleEvents()
        {
            if (SplashKit.KeyTyped(KeyCode.SKey))
            {
                if(OnSKeyPressed != null) OnSKeyPressed(this, new OnPathChangedEventArgs() {pathFinding = _pathFinding});
            }
            if (SplashKit.KeyTyped(KeyCode.EKey))
            {
                if(OnEKeyPressed != null) OnEKeyPressed(this, new OnPathChangedEventArgs() {pathFinding = _pathFinding});
            }
            if (SplashKit.KeyTyped(KeyCode.WKey))
            {
                if(OnWKeyPressed != null) OnWKeyPressed(this, new OnWallChangedEventArgs() {pathFinding = _pathFinding, wallManagement = _wallManagement});
            }

            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
                if(OnSpacePressed != null) { OnSpacePressed(this, new OnPathChangedEventArgs() {pathFinding = _pathFinding});}
            }
            window.Refresh(60);
            SplashKit.ProcessEvents();
            Thread.Sleep(10);
        }
    }

    // This is an abstract class that is responsible for displaying the window.
    public abstract class AbstractWindow:IGUI{
        public Window window;
        protected Point2D _startPoint;
        public int CellSize {get;} = 20;
        public int StartX {get;set;}= 10;
        public int StartY {get;set;}= 10;
        public int EndX {get;set;}= 35;
        public int EndY {get;set;}= 10;
        protected int _width = 1005;
        protected int _height = 565;

        // Constructor that needs name, width, and height to build window.
        public AbstractWindow(string name, int width, int height){
            _width = width;
            _height = height;
            if(window != null) window.Close();
            window = Factory.CreateNewWindow(name, width, height);
            _startPoint = Factory.CreatePoint(0, 0);
            StartX = _width /10 /CellSize;
            StartY = _height / 2  / CellSize;
            EndX = _width / CellSize - StartX;
            EndY = _height/ 2 / CellSize;
        }

        // An overloaded constructor that open window with given name and default width and height.
        public AbstractWindow(string name){
            if(window != null) window.Close();
            window = Factory.CreateNewWindow(name, _width, _height);
            _startPoint = Factory.CreatePoint(0,0);
            StartX = _width /10 /CellSize;
            StartY = _height / 2  / CellSize;
            EndX = _width / CellSize - StartX;
            EndY = _height/ 2 / CellSize;
        }
        // Resets GUI with given name, window width, and height.
        public abstract void ResetGUI(string name, int width, int height);
        // Displays the GUI.
        public abstract void DisplayGUI();
    }

    // This is an A* algorithm class that implements IWallManagement and IFindPath. 
    public class AStartPathFinding : IWallManagement, IFindPath<PathNode>
    {
        // Closest integer of squre root of 2 multipliies by 10 becasue we work with non decimal number.
        private const int DIAGONAL_COST = 14;

        // Just value of 1 but multiplies by 10 becasue we work with non decimal number (index).
        private const int ADJACENT_COST = 10;
        private Grid<PathNode> _pathNodeGrid;
        private List<PathNode> _openList;
        private List<PathNode> _closedList;
        private List<PathNode> _visitedList;

        // Returns _pathNodeGrid
        public Grid<PathNode> GetGrid()
        {
            
            return _pathNodeGrid;
            
        }

        // Constructor that takes number of column, row, cell size, and starting point.
        // It will create a Grid size of column * row that each cell contains pathNode object.
        public AStartPathFinding(int column, int row, int cellSize, Point2D point)
        {
            _pathNodeGrid = new Grid<PathNode>(column, row, cellSize, point, (grid, column, row) => (PathNode) Factory.CreatePathNode(grid, column, row));
        }

        // Returns visited path.
        public List<PathNode> GetVisitedPath(){
            return _visitedList;
        }

        // Finds the shortest path from startx,y to endx,y in the grid.
        public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            PathNode startNode = _pathNodeGrid.GetValue(startX, startY);
            PathNode endNode = _pathNodeGrid.GetValue(endX, endY);
            if (startNode == null || endNode == null) return null;

            _openList = new List<PathNode> { startNode };
            _closedList = new List<PathNode>();
            _visitedList = new List<PathNode>();

            // Initialize f,g,h costs to each pathnode object in _pathNodeGrid
            for (int x = 0; x < _pathNodeGrid.Column; x++)
            {
                for (int y = 0; y < _pathNodeGrid.Row; y++)
                {
                    PathNode pathNode = _pathNodeGrid.GetValue(x, y);
                    pathNode.GCost = int.MaxValue;
                    pathNode.CalculateFCost();
                    pathNode.CameFromNode = null;
                }
            }

            startNode.GCost = 0;
            startNode.HCost = CalculateDistance(startNode, endNode);
            startNode.CalculateFCost();

            while (_openList.Count > 0)
            {
                PathNode currentLowestFCostNode = GetLowestFCostNode(_openList);
                if (currentLowestFCostNode == endNode)
                {
                    return CalculatePath(endNode);
                }

                _openList.Remove(currentLowestFCostNode);
                _closedList.Add(currentLowestFCostNode);
                _visitedList.Add(currentLowestFCostNode);

                foreach (PathNode neighbour in GetNeighbourList(currentLowestFCostNode))
                {
                    if (_closedList.Contains(neighbour))
                    {
                        continue;
                    }

                    if (!neighbour.IsWalkAble)
                    {
                        _closedList.Add(neighbour);
                        continue;
                    }

                    int tGcost = currentLowestFCostNode.GCost + CalculateDistance(currentLowestFCostNode, neighbour);
                    if (tGcost < neighbour.GCost)
                    {
                        neighbour.CameFromNode = currentLowestFCostNode;
                        neighbour.GCost = tGcost;
                        neighbour.HCost = CalculateDistance(neighbour, endNode);
                        neighbour.CalculateFCost();

                        if (!_openList.Contains(neighbour))
                        {
                            _openList.Add(neighbour);
                        }
                    }
                }
            }
            // searched all areas of the grid, but no valid path found.
            return null;

        }

        // Calsulates distance in integer from nodeA to nodeB
        private int CalculateDistance(PathNode nodeA, PathNode nodeB)
        {
            int distanceX = Math.Abs(nodeA.X - nodeB.X);
            int distanceY = Math.Abs(nodeA.Y - nodeB.Y);
            int remaining = Math.Abs(distanceX - distanceY);
            return DIAGONAL_COST * Math.Min(distanceX, distanceY) + ADJACENT_COST * remaining;
        }

        // Returns the node object for given axis(x) and ordinate(y).
        private PathNode GetNode(int x, int y)
        {
            return _pathNodeGrid.GetValue(x, y);
        }

        // Returns the lowest FCost node from given list.
        private PathNode GetLowestFCostNode(List<PathNode> pathNodes)
        {
            PathNode lowestFcostNode = pathNodes[0];
            for (int i = 1; i < pathNodes.Count; i++)
            {
                if (pathNodes[i].FCost < lowestFcostNode.FCost)
                {
                    lowestFcostNode = pathNodes[i];
                }
            }
            return lowestFcostNode;
        }

        // Returns a list of all nodes that a given node came from.
        private List<PathNode> CalculatePath(PathNode endNode)
        {
            List<PathNode> pathList = new List<PathNode>();
            pathList.Add(endNode);
            PathNode currenNode = endNode;
            while (currenNode.CameFromNode != null)
            {
                pathList.Add(currenNode.CameFromNode);
                currenNode = currenNode.CameFromNode;
            }
            pathList.Reverse();
            return pathList;
        }

        // Returns a list of all nodes around a given node.
        private List<PathNode> GetNeighbourList(PathNode currentNode)
        {
            List<PathNode> neighbourList = new List<PathNode>();
            if (currentNode.X - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y));

                if (currentNode.Y - 1 >= 0)
                {
                    neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
                }
                if (currentNode.Y + 1 < _pathNodeGrid.Row)
                {
                    neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y + 1));
                }
            }

            if (currentNode.X + 1 < _pathNodeGrid.Column)
            {
                neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y));

                if (currentNode.Y - 1 >= 0)
                {
                    neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y - 1));
                }
                if (currentNode.Y + 1 < _pathNodeGrid.Row)
                {
                    neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y + 1));
                }
            }
            if (currentNode.Y - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.X, currentNode.Y - 1));
            }
            if (currentNode.Y + 1 < _pathNodeGrid.Row)
            {
                neighbourList.Add(GetNode(currentNode.X, currentNode.Y + 1));
            }

            return neighbourList;

        }

        // Create a not walkable node (wall) with given axis(x) and ordinate(y).
        public void MakeWall(int x, int y)
        {
            PathNode wallNode = _pathNodeGrid.GetValue(x, y);
            if (wallNode != null)
            {
                wallNode.IsWalkAble = false;
                _closedList.Add(wallNode);
            }
        }

        // Removes a not walkable node (wall) with given axis(x) and ordinate(y).
        public void RemoveWall(int x, int y)
        {
            PathNode wallNode = _pathNodeGrid.GetValue(x, y);
            if (wallNode != null)
            {
                wallNode.IsWalkAble = true;
                _closedList.Remove(wallNode);
            }
        }

    }

    // This is a static event class that contains static methods that handles inputs from user.
    public static class Events
    {
        // This is called when S key is pressesd.
        public static void TestOnSKey(object sender, OnPathChangedEventArgs onPathChangedEventArgs)
        {
            PathFindingGUI pathFindingGUI = (PathFindingGUI)sender;
            pathFindingGUI.StartX = onPathChangedEventArgs.pathFinding.GetGrid().GetColumnNumber(SplashKit.MousePosition());
            pathFindingGUI.StartY = onPathChangedEventArgs.pathFinding.GetGrid().GetRowNumber(SplashKit.MousePosition());
            if (onPathChangedEventArgs.pathFinding.FindPath(pathFindingGUI.StartX, pathFindingGUI.StartY, pathFindingGUI.EndX, pathFindingGUI.EndY) != null)
            {
                pathFindingGUI.pathNodes = onPathChangedEventArgs.pathFinding.FindPath(pathFindingGUI.StartX, pathFindingGUI.StartY, pathFindingGUI.EndX, pathFindingGUI.EndY);
            }
            onPathChangedEventArgs.pathFinding.GetGrid().PrintGrid();
        }

        // This is called when E key is pressesd.
        public static void TestOnEKey(object sender, OnPathChangedEventArgs onPathChangedEventArgs)
        {
            PathFindingGUI pathFindingGUI = (PathFindingGUI)sender;
            pathFindingGUI.EndX = onPathChangedEventArgs.pathFinding.GetGrid().GetColumnNumber(SplashKit.MousePosition());
            pathFindingGUI.EndY = onPathChangedEventArgs.pathFinding.GetGrid().GetRowNumber(SplashKit.MousePosition());
            if (onPathChangedEventArgs.pathFinding.FindPath(pathFindingGUI.StartX, pathFindingGUI.StartY, pathFindingGUI.EndX, pathFindingGUI.EndY) != null)
            {
                pathFindingGUI.pathNodes = onPathChangedEventArgs.pathFinding.FindPath(pathFindingGUI.StartX, pathFindingGUI.StartY, pathFindingGUI.EndX, pathFindingGUI.EndY);
            }
            onPathChangedEventArgs.pathFinding.GetGrid().PrintGrid();
        }

        // This is called when W key is pressesd.
        public static void TestOnWKey(object sender, OnWallChangedEventArgs onWallChangedEventArgs)
        {
            PathFindingGUI pathFindingGUI = (PathFindingGUI)sender;
            onWallChangedEventArgs.pathFinding.GetGrid().GetRowColumnNumber(SplashKit.MousePosition(), out double x, out double y);
            if (onWallChangedEventArgs.pathFinding.GetGrid().GetValue(SplashKit.MousePosition()) != null)
            {
                if (!onWallChangedEventArgs.pathFinding.GetGrid().GetValue(SplashKit.MousePosition()).IsWalkAble)
                {

                    onWallChangedEventArgs.wallManagement.RemoveWall(Convert.ToInt32(x), Convert.ToInt32(y));
                    pathFindingGUI.pathNodes = onWallChangedEventArgs.pathFinding.FindPath(pathFindingGUI.StartX, pathFindingGUI.StartY, pathFindingGUI.EndX, pathFindingGUI.EndY);
                    Util.FillSquare(Color.White, Convert.ToInt32(x), Convert.ToInt32(y), pathFindingGUI.CellSize);
                }
                else
                {
                    onWallChangedEventArgs.wallManagement.MakeWall(Convert.ToInt32(x), Convert.ToInt32(y));
                    pathFindingGUI.pathNodes = onWallChangedEventArgs.pathFinding.FindPath(pathFindingGUI.StartX, pathFindingGUI.StartY, pathFindingGUI.EndX, pathFindingGUI.EndY);
                    Util.FillSquare(Color.Black, Convert.ToInt32(x), Convert.ToInt32(y), pathFindingGUI.CellSize);
                }
            }
        }

        // This is called when Space key is pressesd.
        public static void TestOnSpace(object sender, OnPathChangedEventArgs onPathChangedEventArgs)
        {
            PathFindingGUI pathFindingGUI = (PathFindingGUI)sender;
            if (onPathChangedEventArgs.pathFinding != null)
            {
                for (int i = 1; i < onPathChangedEventArgs.pathFinding.GetVisitedPath().Count; i++)
                {
                    PathNode searchNode = onPathChangedEventArgs.pathFinding.GetVisitedPath()[i];
                    SplashKit.FillRectangle(Color.LightBlue, searchNode.X * pathFindingGUI.CellSize + 2, searchNode.Y * pathFindingGUI.CellSize + 2, pathFindingGUI.CellSize - 2, pathFindingGUI.CellSize - 2);
                    if (i % 5 == 0) pathFindingGUI.window.Refresh(60);
                }
                // If there is no path found, return.
                if(pathFindingGUI.pathNodes == null) return;
                
                for (int i = 1; i < pathFindingGUI.pathNodes.Count - 1; i++)
                {
                    // Color Green, x is axis of pathNodes[i] in grid * cellSize, y is thr ordinate location of pathNodes[i] in grid * cellSize, with and height = cellSize
                    Util.FillSquare(Color.Yellow, pathFindingGUI.pathNodes[i].X, pathFindingGUI.pathNodes[i].Y, pathFindingGUI.CellSize);
                    pathFindingGUI.window.Refresh(60);
                }
                SplashKit.Delay(5000);

            }
        }
    }

    // This is a factory class which creates object of class that is need in classes in the program
    // without using new keyword.
    public static class Factory{

        // Creates an a star object for IFindPath interface.
        public static IFindPath<PathNode> CreatePathFindingObject(int width, int height, int cellSize, Point2D startPoint){
            return new AStartPathFinding(width, height, cellSize, startPoint);
        }

        // Creates a window object.
        public static Window CreateNewWindow(string name, int width, int height){
            return new Window(name, width, height);
        }

        // Creates a point with given x and y.
        public static Point2D CreatePoint(int x, int y){
            return new Point2D(){X=x, Y=y};
        }

        // Creates a path node object.
        public static PathNode CreatePathNode(Grid<PathNode> grid, int x, int y){
            return new PathNode(grid, x, y);
        } 
    }

    // This is a grid Generic (class) with placeholder T.
    public class Grid<T>
    {
        private int _column;
        private int _row;
        private double _cellSize;
        private T[,] _gridArea;
        private Point2D _startPoint;

        public int Row
        {
            get
            {
                return _row;
            }
        }

        public int Column
        {
            get
            {
                return _column;
            }
        }

        public double CellSize
        {
            get
            {
                return _cellSize;
            }
        }

        // Constructor for most of primitive data type.
        public Grid(int column, int row, int singleCellSize)
        {
            _column = column;
            _row = row;
            _cellSize = singleCellSize;
            _gridArea = new T[column, row];
            _startPoint = Factory.CreatePoint(0, 0);
        }

        // Constructor that is made specifically when working with pathnode.
        public Grid(int column, int row, int singleCellSize, Point2D startPoint, Func<Grid<T>, int, int, T> addGridObjFunc)
        {
            _column = column;
            _row = row;
            _cellSize = singleCellSize;
            _gridArea = new T[column, row];
            _startPoint = startPoint;

            for (int x = 0; x < _gridArea.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArea.GetLength(1); y++)
                {
                    _gridArea[x, y] = addGridObjFunc(this, x, y);
                }
            }
        }
        
        // Prints the grid to screen.
        public void PrintGrid()
        {
            for (int columnIndex = 0; columnIndex < _gridArea.GetLength(0); columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < _gridArea.GetLength(1); rowIndex++)
                {
                    SplashKit.DrawLine(Color.Black, GetAPoint(columnIndex, rowIndex), GetAPoint(columnIndex, rowIndex + 1));
                    SplashKit.DrawLine(Color.Black, GetAPoint(columnIndex, rowIndex + 1), GetAPoint(columnIndex + 1, rowIndex + 1));

                }
                SplashKit.DrawLine(Color.Black, GetAPoint(0, 0), GetAPoint(_column, 0));
                SplashKit.DrawLine(Color.Black, GetAPoint(_column, 0), GetAPoint(_column, _row));
            }
        }

        // Gets a point2d object from given x and y.
        public Point2D GetAPoint(int x, int y)
        {
            return Factory.CreatePoint(Convert.ToInt32(x * _cellSize + _startPoint.X), Convert.ToInt32(y * _cellSize + _startPoint.Y));
        }

        // Gets location of an object in grid from a point on screen and output x and y value of the object.
        public void GetRowColumnNumber(Point2D point, out double x, out double y)
        {
            x = GetColumnNumber(point);
            y = GetRowNumber(point);
        }

        // Returns column number from a given point on screen.
        public int GetColumnNumber(Point2D point)
        {
            return Convert.ToInt32(Math.Floor((point.X - _startPoint.X) / _cellSize));
        }
        // Returns row number from a given point on screen.
        public int GetRowNumber(Point2D point)
        {
            return Convert.ToInt32(Math.Floor((point.Y - _startPoint.Y) / _cellSize));
        }

        // Sets vaule for given row and column.
        public void SetValue(int columnIndex, int rowIndex, T value)
        {
            if (columnIndex >= 0 && columnIndex < _column && rowIndex >= 0 && rowIndex < _row)
            {
                _gridArea[columnIndex, rowIndex] = value;
            }
        }

        // Sets value for given point.
        public void SetValue(Point2D point, T value)
        {
            SetValue(GetColumnNumber(point), GetRowNumber(point), value);

        }

        // Gets value from given row and column.
        public T GetValue(int columnIndex, int rowIndex)
        {
            if (columnIndex >= 0 && columnIndex < _column && rowIndex >= 0 && rowIndex < _row)
            {
                return _gridArea[columnIndex, rowIndex];
            }
            return default(T);
        }

        // Get value from given point.
        public T GetValue(Point2D point)
        {
            return GetValue(GetColumnNumber(point), GetRowNumber(point));
        }
    }

    // This is an interface that is responsible for finding path.
    public interface IFindPath<T>{
        List<T> FindPath(int startX, int startY, int endX, int endY);
        Grid<T> GetGrid();
        List<T> GetVisitedPath();
    }

    // This is an interface that is responsible for managing user interface.
    public interface IGUI{
        void ResetGUI(string name, int width, int height);
        void DisplayGUI();
    }

    // This is an interface that is responsible for handling events.
    public interface IHandleEvent{
        void HandleEvents();
    }

    // This is an interface that is reponsible for adding/removing wall.
    public interface IWallManagement{
        void RemoveWall(int x, int y);
        void MakeWall(int x, int y);
    }

    // This is an OnPathChnaged class that extends EventArgs class.
    public class OnPathChangedEventArgs : EventArgs
    {
        public IFindPath<PathNode> pathFinding;
    }

    // This is an OnPathWall class that extends EventArgs class.
    public class OnWallChangedEventArgs : EventArgs
    {
        public IFindPath<PathNode> pathFinding;
        public IWallManagement wallManagement;
    }

    // This is this pathnode class.
    public class PathNode
    {   
        private Grid<PathNode> _pathNodeGrid;
        public int X{ get; set; }
        public int Y{ get; set; }
        public int GCost { get; set; }
        public int HCost {set; get;}
        public int FCost {set; get;}

        public PathNode CameFromNode{ get; set; }

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
    }
    
    // This is a util class that contains static methods.
    public static class Util{
        // Fills a squre with given color, axis, ordinate, and size.
        public static void FillSquare(Color color, int x, int y, double cellSize ){
            SplashKit.FillRectangle(color, x * cellSize, y * cellSize, cellSize, cellSize);
        }
    }
}