using System.Collections.Generic;

namespace MyPathFinding
{
     // This is an interface that is responsible for finding path.
    public interface IFindPath<T>{
        List<T> FindPath(int startX, int startY, int endX, int endY);
        Grid<T> GetGrid();
        List<T> GetVisitedPath();
    }
    
}