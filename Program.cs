using ConsoleDungeonCrawler.Printer;
using System.Runtime.InteropServices;

namespace ConsoleDungeonCrawler
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowPosition(0,0);
            //Menu.MainMenu();
            Game.Start();
        }
    }
}


