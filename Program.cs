using ConsoleDungeonCrawler.Printer;

namespace ConsoleDungeonCrawler
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(120, 40);
            //Menu.MainMenu();
            Game.Start();
        }
    }
}

