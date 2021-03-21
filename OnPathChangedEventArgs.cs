using System;

namespace MyPathFinding
{
    // This is an OnPathChnaged class that extends EventArgs class.
    public class OnPathChangedEventArgs : EventArgs
    {
        public IFindPath<PathNode> pathFinding;
    }

}
