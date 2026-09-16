namespace Evo.Universe
{
    public enum gender
    {
        Male,
        Female
    }

    public class Creature
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public int Age { get; set; }
        public int Health { get; set; }
        public int Energy { get; set; }
        public int Strength { get; set; }
        public gender Gender { get; set; }

        public int wasteToRelease { get; set; }

        public WorldPositions currentPosition { get; set; }

        public bool isAlive { get; set; } = true;


        // Constructor
        public Creature(
            string name,
            int age,
            int health,
            int energy,
            int strength,
            gender gender,
            WorldPositions currentPosition)
        {
            Name = name;
            Age = age;
            Health = health;
            Energy = energy;
            Strength = strength;
            Gender = gender;
            this.currentPosition = currentPosition;
        }


  
public void CreatureMove(WorldPositions positiontoMove, List<WorldPositions> worldpositions)
{
    int x = currentPosition.X;
    int y = currentPosition.Y;

    List<WorldPositions> checkpositions = new List<WorldPositions>();

    for (int offsetX = -1; offsetX <= 1; offsetX++)
    {
        for (int offsetY = -1; offsetY <= 1; offsetY++)
        {
            if (offsetX == 0 && offsetY == 0)
                continue;

            WorldPositions? found = worldpositions.Find(
                p => p.X == x + offsetX &&
                     p.Y == y + offsetY
            );

            if (found != null)
            {
                checkpositions.Add(found);
            }
        }
    }


    int directionX = Math.Sign(positiontoMove.X - currentPosition.X);
    int directionY = Math.Sign(positiontoMove.Y - currentPosition.Y);


    WorldPositions? idealPosition = null;

    int bestScore = -1;


    foreach (WorldPositions position in checkpositions)
    {
        if (position == positiontoMove)
        {
            idealPosition = position;
            break;
        }


        if (position.OccupyingCreature is null &&
            position.OccupyingFood is null)
        {
            int moveX = Math.Sign(position.X - currentPosition.X);
            int moveY = Math.Sign(position.Y - currentPosition.Y);

            int score = 0;


            if (moveX == directionX)
            {
                score++;
            }


            if (moveY == directionY)
            {
                score++;
            }


            if (score > bestScore)
            {
                bestScore = score;
                idealPosition = position;
            }
        }
    }


    if (idealPosition != null)
    {
        currentPosition.OccupyingCreature = null;

        currentPosition = idealPosition;

        currentPosition.OccupyingCreature = this;
    }
}
        public static Creature CreateRandom(WorldPositions worldPositions)
        {
            string[] maleFirstNames =
            {
                "John",
                "Michael",
                "David",
                "James",
                "Robert"
            };

            string[] femaleFirstNames =
            {
                "Mary",
                "Jennifer",
                "Linda",
                "Elizabeth",
                "Susan"
            };

            string[] lastNames =
            {
                "Smith",
                "Johnson",
                "Williams",
                "Jones",
                "Brown"
            };


            // Random gender
            gender randomGender =
                (gender)Random.Shared.Next(0, 2);


            // Random first name
            string firstName;

            if (randomGender == gender.Male)
            {
                firstName =
                    maleFirstNames[
                        Random.Shared.Next(maleFirstNames.Length)
                    ];
            }
            else
            {
                firstName =
                    femaleFirstNames[
                        Random.Shared.Next(femaleFirstNames.Length)
                    ];
            }


            // Random last name
            string lastName =
                lastNames[
                    Random.Shared.Next(lastNames.Length)
                ];


            // Full name
            string name = $"{firstName} {lastName}";


            // Random stats
            int age = 0;

            int health =
               100;

            int energy =
                Random.Shared.Next(50, 101);

            int strength =
                Random.Shared.Next(1, 21);

            int wasteToRelease =
               0;




            Creature creature = new Creature(
                name,
                age,
                health,
                energy,
                strength,
                randomGender,
                worldPositions
            );

            worldPositions.OccupyingCreature = creature;
            creature.wasteToRelease = wasteToRelease;
            creature.isAlive = true;


            return creature;
        }


        public void AgeCreature()
        {
            Age++;
            Health -= 1;
            Energy -= 1;

            if (Health <= 0 || Energy <= 0)
            {
                isAlive = false;
                currentPosition.OccupyingCreature = null;
            }
        }
        public void displayCreatre()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine($"Energy: {Energy}");
            Console.WriteLine($"Strength: {Strength}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine(
                $"Current Position: ({currentPosition.X}, {currentPosition.Y})"
            );
            Console.WriteLine($"Is Alive: {isAlive}");
            Console.WriteLine($"Waste To Release: {wasteToRelease}");
        }
    }
}