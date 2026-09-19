namespace Evo.Universe;

using System.Timers;
using Timer = System.Timers.Timer;
public enum Gender
{
    Male,
    Female
}
public class Creature



{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName;
    public string FamName;
    public int age;
    public int Energy;
    public Creature? Mother;
    public Creature? Father;
    public List<Creature> Kids = new();
    public List<Creature> Mates = new();
    public Gender gender;
    private static Random random = new Random();

    public WorldPosition CurrentPosition;
    public bool Alive;


    //Create Random Creature
    public Creature(WorldPosition worldposition )
    {

         string[] mailNames = ["Uru", "Tiru", "Kara", "Vuru", "Maku", "Lulu", "Gato", "Pudu", "Fuju", "Atu", "Dunu", "Rata"];
         string[] femaleNames = ["Sakala", "Kalya", "Uma", "Rashi", "Bani", "Wiruya", "Saya", "Maya", "Aguka", "Raya"];

         string[] familyNames = ["Agakari", "Makari", "Gogerimahami", "Pruthuvipathy", "Sagani", "Adirukathi"];

        this.gender = (Gender)random.Next(0, 2);
        this.age = 0;
        this.Energy = random.Next(80, 100);
        this.Alive = true;
       
        this.CurrentPosition = worldposition;
       

        if (this.gender == Gender.Male)
        {
            this.FirstName = mailNames[random.Next(0,mailNames.Length)];
        }
        else
        {
            this.FirstName = femaleNames[random.Next(0, femaleNames.Length)];
        }

        this.FamName = familyNames[random.Next(0,familyNames.Length)];

    }


    public void CreatureBehvae()
    {

        Age();
        BurnEnergy();

    }


    public void BurnEnergy()
    { if (this.Energy <= 0) {
            this.Alive = false;
            this.CurrentPosition.currentOccCreature = null;

        }else{
        this.Energy -= 1;
        }

    }
    //Creature Aging
    public void Age()
    {
         if (this.age >= 100) {
            this.Alive = false;
            this.CurrentPosition.currentOccCreature = null;

        }else{
        this.age += 1;
        }
    }



    //walk
    public void walk(WorldPosition positionToGo , List <WorldPosition> worldpositions)

    {

        int currentX = this.CurrentPosition.X;
        int currentY = this.CurrentPosition.Y;

        List<WorldPosition> checkpositions = new();

        for (int offsetX = -1; offsetX <= 1; offsetX++)
        {
            for (int offsetY = -1; offsetY <= 1; offsetY++)
            {
                if (offsetX == 0 && offsetY == 0)
                    continue;

            WorldPosition? found = worldpositions.Find(
                p => p.X == currentX + offsetX &&
                     p.Y == currentY + offsetY
            );

         if (found != null)
            {
                checkpositions.Add(found);
            }

            }

            


        }

       WorldPosition? idealPosition = null;
int bestDistance = int.MaxValue;

foreach (WorldPosition position in checkpositions)
{
    if (position == positionToGo)
    {
        idealPosition = position;
        break;
    }

    if (position.currentOccCreature is not null ||
        position.CurrentOccFood is not null)
    {
        continue;
    }

    int distanceX = Math.Abs(positionToGo.X - position.X);
    int distanceY = Math.Abs(positionToGo.Y - position.Y);

    int distance = Math.Max(distanceX, distanceY);

    if (distance < bestDistance)
    {
        bestDistance = distance;
        idealPosition = position;
    }
}

      


        

        if (idealPosition is not null)
        {
            this.CurrentPosition.currentOccCreature= null;
            this.CurrentPosition = idealPosition;
            idealPosition.currentOccCreature = this;
        }
    }





}
