using System.Timers;

namespace Evo.Universe;
using Timer = System.Timers.Timer;


public class World
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int worldHeight;
    public int worldWidth;
    public List<WorldPosition> worldpositions = new();
    public List<Creature> Creatures = new();
    public List<Foods> Foods = new();



public  World(int worldheight , int worldwidth)
    {
        Console.Clear();

        this.worldHeight = worldheight;
        this.worldWidth = worldwidth;
       Console.SetCursorPosition(0, 0);
        for (int y = 0; y < worldheight; y++)
        {
            for (int x = 0; x < worldwidth; x++)
            {
                WorldPosition worldpostion = new WorldPosition(x, y);
                worldpositions.Add(worldpostion);
                Console.Write(".");
            }

                Console.WriteLine();
        }

    }


    public void UpdateWorld()
    {

        Timer timer = new Timer(100);
        Timer Ctimer = new Timer(500);

        timer.Elapsed += this.ReRender;
        timer.Elapsed += this.PrintCreatures;
        Ctimer.Elapsed += this.UpdatesCreature;
        Ctimer.Elapsed += this.WalkTest;
        timer.AutoReset = true;
        Ctimer.AutoReset = true;
        Ctimer.Start();
        timer.Start();
    }

    public void ReRender(object sender, ElapsedEventArgs e)

    {
        Console.SetCursorPosition(0, 0);
        for (int y = 0; y < worldHeight; y++)
        {
            for (int x = 0; x < worldWidth; x++)
            {
         WorldPosition position = worldpositions[y * worldWidth + x];

                if (position.CurrentOccFood is  not null)
                {
                Console.Write("x");

                }
                else if (position.currentOccCreature is not null)
                {
                    if (!position.currentOccCreature.Alive)
                    {
                    Console.Write(".");
                    }else{
                    if (position.currentOccCreature.gender == Gender.Male)
                    {
                        Console.Write(position.currentOccCreature.age > 18 ? "T" : "t");
                    }
                    else
                    {
                        Console.Write(position.currentOccCreature.age > 18 ? "M" : "m");
                    }}

                }
                else
                {
                Console.Write(".");

                }
            }

                Console.WriteLine();
        }

    }




    //PrintCreatures
    public void PrintCreatures(object sender, ElapsedEventArgs e)
{

        if (Creatures.Count != 0)
        {
            Console.SetCursorPosition(0, worldHeight + 2);

            Console.WriteLine("___________ CREATURES ___________");

        Console.WriteLine();
        
        foreach (Creature creature in Creatures)
    {
        Console.WriteLine(
            $"{creature.FirstName} {creature.FamName} | " +
            $"Gender: {creature.gender} | " +
            $"Age: {creature.age} | " +
            $"Energy: {creature.Energy} | " +
            $"Alive: {creature.Alive}"
        );
        }
    Console.WriteLine();
            Console.WriteLine("_________________");
    }
        }

    public void UpdatesCreature(object sender, ElapsedEventArgs e)
    {
        foreach (var creature in Creatures)
        {
            creature.CreatureBehvae();
        }
    }

      public void WalkTest(object sender, ElapsedEventArgs e)
    {
        foreach (var creature in Creatures)
        {
            creature.walk(worldpositions[1], worldpositions);
        }
    } 


    ///Add Creature
    public void addRandomCreature()
        {
        WorldPosition choosedPosition = null;

        Random random = new Random();

        while (choosedPosition is null)
        {
            WorldPosition R = worldpositions[random.Next(0, worldpositions.Count)];

            if (R.currentOccCreature is null && R.CurrentOccFood is null)
            {
                choosedPosition = R;
                break;
            }
        }

        Creature creature = new Creature(choosedPosition);
        choosedPosition.currentOccCreature = creature;

        Creatures.Add(creature);


    }

    //Add Food
    public void addRandomFood()
    {
    WorldPosition choosedPosition = null;

        Random random = new Random();

        while (choosedPosition is null)
        {
            WorldPosition R = worldpositions[random.Next(0, worldpositions.Count)];

            if (R.currentOccCreature is null && R.CurrentOccFood is null)
            {
                choosedPosition = R;
                break;
            }
        }

        Foods food = new Foods(choosedPosition);
        choosedPosition.CurrentOccFood = food;

        Foods.Add(food);

    } 

}
