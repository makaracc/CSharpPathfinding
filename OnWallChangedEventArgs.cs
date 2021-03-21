using System;

namespace MyPathFinding
{

    // This is an OnPathWall class that extends EventArgs class.
    public class OnWallChangedEventArgs : EventArgs
    {
        public IFindPath<PathNode> pathFinding;
        public IWallManagement wallManagement;
    }

}
