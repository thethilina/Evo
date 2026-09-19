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

        Timer timer = new Timer(500);
        timer.Elapsed += this.Age;

        timer.AutoReset = true;
        timer.Start();

    }



    //Creature Aging
    public void Age(object sender, ElapsedEventArgs e)
    {
        this.age = +1;

        if (this.age > 100) {
            this.Alive = false;
        } 
    }

}
