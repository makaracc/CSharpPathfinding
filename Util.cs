using SplashKitSDK;
namespace MyPathFinding{
    // This is a util class that contains static methods.
    public static class Util{
        // Fills a squre with given color, axis, ordinate, and size.
        public static void FillSquare(Color color, int x, int y, double cellSize ){
            SplashKit.FillRectangle(color, x * cellSize, y * cellSize, cellSize, cellSize);
        }
    }
}