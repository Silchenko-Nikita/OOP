using System;


namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Game gm = new Game();
            gm.RunGame();
            gm.Dispose();

            Console.ReadKey();
        }
    }
}