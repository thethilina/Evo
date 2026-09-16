using System;
using System.Collections.Generic;
using System.Timers;
using Evo.Universe;
using Timer = System.Timers.Timer;

class Program
{
    private static readonly List<WorldPositions> worldPositions = new List<WorldPositions>();
    private static readonly List<Creature> creatures = new List<Creature>();

    private static readonly List<Food> foods = new List<Food>();
    private static bool isFirstRender = true;

    static void Main(string[] args)
    {
        Console.CursorVisible = false;
      
        Console.Clear();
        Timer timer = new Timer(100);
        Timer creatureTimer = new Timer(500);
        timer.Elapsed += UpdateWorld;
        creatureTimer.Elapsed += UpdateCreatures;


        int worldwidth = 50;
        int worlheight = 20;



        for (int y = 0; y < worlheight; y++)
        {
            for (int x = 0; x < worldwidth; x++)
            {
                WorldPositions position = new WorldPositions(x, y);
                worldPositions.Add(position);
                Console.Write(".");
            }

            Console.WriteLine();
        }








        bool isRunning = true;

    

        timer.AutoReset = true;
        creatureTimer.AutoReset = true;

        timer.Start();
        creatureTimer.Start();

        while (isRunning)
        {
           

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo inputKey = Console.ReadKey(true);
                ConsoleKey key = inputKey.Key;

                if (key == ConsoleKey.C)
                {
                    Creature newCreature = Creature.CreateRandom(worldPositions[new Random().Next(worldPositions.Count)]);
                    creatures.Add(newCreature);
                }
                else if (key == ConsoleKey.R)
                {
                    creatures.Clear();
                    foreach (WorldPositions position in worldPositions)
                    {
                        position.OccupyingCreature = null;
                    }
                }
                else if (key == ConsoleKey.F)
                {
                    Food newFood = Food.CreateRandom(worldPositions[new Random().Next(worldPositions.Count)]);
                    foods.Add(newFood);
                }
                else if (key == ConsoleKey.P)
                {
                    foreach (Creature creature in creatures)
                    {
                        creature.displayCreatre();
                    }
                }
                
                if (key == ConsoleKey.Q)
                {
                    timer.Stop();
                    creatureTimer.Stop();
                    isRunning = false;
                }
            }
        }
    }



    public static void UpdateCreatures(object sender, ElapsedEventArgs e)
    {
        foreach (Creature creature in creatures)
        {
            if (creature.isAlive)
            {
                creature.AgeCreature();
                  creature.CreatureMove(worldPositions[0], worldPositions);
            }
        }
    }

    public static void UpdateWorld(object sender, ElapsedEventArgs e)
    {
        const int worldWidth = 50;
        const int worldHeight = 20;

       Console.SetCursorPosition(0, 0);

            for (int y = 0; y < worldHeight; y++)
            {
                for (int x = 0; x < worldWidth; x++)
                {
                    WorldPositions position = worldPositions[y * worldWidth + x];

                    if (position.OccupyingCreature != null)
                    {
                        if (position.OccupyingCreature.isAlive)
                        {
                            if (position.OccupyingCreature.Gender == gender.Male)
                            {
                                Console.Write("M");
                            }
                            else
                            {
                                Console.Write("F");
                            }
                        }
                        else
                        {
                            Console.Write("X");
                        }
                    }
                    else if (position.OccupyingFood != null)
                    {
                            Console.Write("#");
                    }
                  
                    else
                    {
                    Console.Write("..");
                    }
                }

                Console.WriteLine();
            }
        
    }
}
