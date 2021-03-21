using SplashKitSDK;
using System.Threading;
namespace MyPathFinding
{

    /*
        This is the Testing class which use a facade class called PathFindingGUI
        to run the program inside a while loop.

    */
    class TestingClass
    {
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
}