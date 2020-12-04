// using SplashKitSDK;

// namespace MyPathFinding{
//     public static class Factory{
//         public static IFindPath<PathNode> CreatePathFindingObject(int width, int height, int cellSize, Point2D startPoint){
//             return new AStartPathFinding(width, height, cellSize, startPoint);
//         }

//         public static Window CreateNewWindow(string name, int width, int height){
//             return new Window(name, width, height);
//         }

//         public static Point2D CreatePoint(int x, int y){
//             return new Point2D(){X=x, Y=y};
//         }

//         public static PathNode CreatePathNode(Grid<PathNode> grid, int x, int y){
//             return new PathNode(grid, x, y);
//         } 
//     }
// }