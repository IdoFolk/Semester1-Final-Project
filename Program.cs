using ConsoleDungeonCrawler.Printer;
using System.Runtime.InteropServices;

namespace ConsoleDungeonCrawler
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.SetWindowPosition(0,0);
            Console.SetBufferSize(200, 41);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            while (true)
            {
                Menu.MainMenu();
            }
        }
    }

}