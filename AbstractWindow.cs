// using SplashKitSDK;

// namespace MyPathFinding
// {
//     public abstract class AbstractWindow:IGUI{
//         public Window window;
//         protected Point2D _startPoint;
//         public int CellSize {get;} = 20;
//         public int StartX {get;set;}= 10;
//         public int StartY {get;set;}= 10;
//         public int EndX {get;set;}= 35;
//         public int EndY {get;set;}= 10;
//         protected int _width = 1005;
//         protected int _height = 565;

//         public AbstractWindow(string name, int width, int height){
//             _width = width;
//             _height = height;
//             if(window != null) window.Close();
//             window = Factory.CreateNewWindow(name, width, height);
//             _startPoint = Factory.CreatePoint(0, 0);
//             StartX = _width /10 /CellSize;
//             StartY = _height / 2  / CellSize;
//             EndX = _width / CellSize - StartX;
//             EndY = _height/ 2 / CellSize;
//         }
//         // Open window with default value.
//         public AbstractWindow(string name){
//             if(window != null) window.Close();
//             window = Factory.CreateNewWindow(name, _width, _height);
//             _startPoint = Factory.CreatePoint(0,0);
//             StartX = _width /10 /CellSize;
//             StartY = _height / 2  / CellSize;
//             EndX = _width / CellSize - StartX;
//             EndY = _height/ 2 / CellSize;
//         }
//         public abstract void ResetGUI(string name, int width, int height);
//         public abstract void DisplayGUI();
//     }
    
// }