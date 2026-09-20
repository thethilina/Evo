using System;
using Evo.Universe;

class Program
{


    static void Main(string[] args)
    {

        Console.Clear();
        Console.CursorVisible = false;
        World world = new World(20, 100);
        world.UpdateWorld();

        bool isrunning = true;

        while (isrunning)
        {


            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo inputKey = Console.ReadKey(true);
                ConsoleKey key = inputKey.Key;

                if (key == ConsoleKey.C)
                {
                    world.addRandomCreature();
                }   
                else if (key == ConsoleKey.F)
                {
                    world.addRandomFood();
                }
                else if (key == ConsoleKey.Q)
                {
                    isrunning = false;
                }
             
            }
            
        }    








}

}
