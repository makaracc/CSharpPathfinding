namespace MyPathFinding
{
   // This is an interface that is reponsible for adding/removing wall.
    public interface IWallManagement{
        void RemoveWall(int x, int y);
        void MakeWall(int x, int y);
    }
    
}