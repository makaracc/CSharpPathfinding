using SplashKitSDK;

namespace MyPathFinding{
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
}