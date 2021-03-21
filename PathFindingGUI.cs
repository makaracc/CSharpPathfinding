using System;
using System.Collections.Generic;
using System.Threading;
using SplashKitSDK;

namespace MyPathFinding
{
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
}
