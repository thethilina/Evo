namespace Evo.Universe;
using Timer = System.Timers.Timer;
public enum Gender
{
    Male,
    Female
}
public class Creature



{
    public Guid Id;
    public string FirstName;
    public string FamName;
    public int age;
    public int Energy;
    public Creature Mother;
    public Creature Father;
    public List<Creature> Kids = new();
    public List<Creature> Mates = new();
    public Gender gender;

    public WorldPosition CurrentPosition;
    public bool Alive;


    //Create Random Creature
    public Creature(List<WorldPosition> worldpositions )
    {
        Random random = new Random();

         string[] mailNames = ["Uru", "Tiru", "Kara", "Vuru", "Maku", "Lulu", "Gato", "Pudu", "Fuju", "Atu", "Dunu", "Rata"];
         string[] femaleNames = ["Sakala", "Kalya", "Uma", "Rashi", "Bani", "Wiruya", "Saya", "Maya", "Aguka", "Raya"];

         string[] familyNames = ["Agakari", "Makari", "Gogerimahami", "Pruthuvipathy", "Sagani", "Adirukathi"];

        this.gender = (Gender)random.Next(0, 2);
        this.age = 0;
        this.Energy = random.Next(80, 100);
        this.Alive = true;
        while(this.CurrentPosition is  null){

            WorldPosition RandomPosition = worldpositions[random.Next(0, worldpositions.Count)];

            if (RandomPosition.currentOccCreature is  null && RandomPosition.CurrentOccFood is  null)
            {
                this.CurrentPosition = RandomPosition;
                break;
            }  
        }

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

        Timer timer = new Timer(1000);



        
    }


}
