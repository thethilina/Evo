namespace Evo.Universe;
using System.Timers;
using Evo.AI;
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
    public GoapPlanner Planner = new GoapPlanner();
    public WorldPosition CurrentPosition;
    public bool Alive;
    public List<GoapAction> GetActions()
    {
        List<GoapAction> actions = new();

        GoapAction eat = new GoapAction("Eat", 1);

        eat.Preconditions["Hungry"] = true;
        eat.Effects["Hungry"] = false;

        actions.Add(eat);


        GoapAction mate = new GoapAction("Mate", 5);

        mate.Preconditions["Hungry"] = false;
        mate.Preconditions["Adult"] = true;
        mate.Preconditions["Male"] = true;

        mate.Effects["HasReproduced"] = true;

        actions.Add(mate);


        return actions;
    }
    public Dictionary<string, bool> GetGoal()
    {
        Dictionary<string, bool> goal = new();

        if (Energy < 50)
        {
            goal["Hungry"] = false;
        }
        else if (age >= 18 && gender == Gender.Male)
        {
            goal["HasReproduced"] = true;
        }

        return goal;
    }

    public Dictionary<string, bool> GetState()
    {
        Dictionary<string, bool> state = new();

        state["Hungry"] = Energy < 50;

        state["Adult"] = age >= 18;

        state["Male"] = gender == Gender.Male;

        state["Alive"] = Alive;

        return state;
    }


    //Create Random Creature
    public Creature(WorldPosition worldposition , Creature mother = null , Creature father = null)
    {

         string[] mailNames = ["Uru", "Tiru", "Kara", "Vuru", "Maku", "Lulu", "Gato", "Pudu", "Fuju", "Atu", "Dunu", "Rata"];
         string[] femaleNames = ["Sakala", "Kalya", "Uma", "Rashi", "Bani", "Wiruya", "Saya", "Maya", "Aguka", "Raya"];

         string[] familyNames = ["Agakari", "Makari", "Gogerimahami", "Pruthuvipathy", "Sagani", "Adirukathi"];

        this.gender = (Gender)random.Next(0, 2);
        this.age = 0;
        this.Energy = random.Next(80, 100);
        this.Alive = true;
       
        this.CurrentPosition = worldposition;


        if (mother is not null)
        {
            this.Mother = mother;
        }


        if (father is not null)
        {
            this.Father = father;

        }
        
        if (this.gender == Gender.Male)
        {
            this.FirstName = mailNames[random.Next(0,mailNames.Length)];
        }
        else
        {
            this.FirstName = femaleNames[random.Next(0, femaleNames.Length)];
        }

        if (father is not null)
        {
            this.FamName = father.FamName;
        }else{
        this.FamName = familyNames[random.Next(0,familyNames.Length)];
        }
    }


  public void CreatureBehvae(
    List<Foods> foods,
    List<WorldPosition> worldpositions,
    List<Creature> creatures)
{
    Age();
    BurnEnergy();

    if (!Alive)
        return;

    Think();

    GoapAction? action = Planner.MakePlan(
        GetState(),
        GetActions(),
        GetGoal()
    );

    if (action is not null)
    {
        ExecuteAction(
            action,
            foods,
            worldpositions,
            creatures
        );
    }
}

/// automatic actions ( cant avoid typa shits yk)
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
         if (this.age >= 500) {
            this.Alive = false;
            this.CurrentPosition.currentOccCreature = null;

        }else{
        this.age += 1;
        }
    }


//Actions real shits
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

//look for the closet food
    public Foods lookforFood( List <Foods> foods )
    {
        if (foods.Count == 0)
        {
            return null;
        }

        Foods  closetfood = null;
        int bestDistance = int.MaxValue;
        foreach (Foods food in foods)
        {

            if (closetfood is null)
            {
                closetfood = food; 
            }

            int distanceX = Math.Abs(food.position.X - CurrentPosition.X);
            int distanceY = Math.Abs(food.position.Y - CurrentPosition.Y);
            int distance = Math.Max(distanceX, distanceY);
               if (distance < bestDistance)
    {
        bestDistance = distance;
        closetfood = food;
    }
        }

    
        if(closetfood is not null){

            return closetfood;
        }

        return null;
    }


    public void eat(Foods food , List <Foods> foods)
    {

        int sumEnergy =  this.Energy + food.Energy;

        if (sumEnergy > 100)
        {
            this.Energy = 100;
        }

        else
        {

            this.Energy = sumEnergy;
        }

        food.position.CurrentOccFood = null;
        foods.Remove(food);

    }



    //Only for male ones and closet ideal mate
    public Creature lookforaMate(List<Creature> creatures)
    {
         if ( this.age < 18 ||this.gender == Gender.Female || !this.Alive )
        {
            return null;
        }

        Creature idealMate = null;
        int bestDistance = int.MaxValue;

        foreach (Creature creature in creatures)
        {

            if (creature.gender == Gender.Male || creature.age < 18 || !creature.Alive)
            {
                continue;
            }
    

            int distanceX = Math.Abs(creature.CurrentPosition.X - CurrentPosition.X);
            int distanceY = Math.Abs(creature.CurrentPosition.Y - CurrentPosition.Y);
            int distance = Math.Max(distanceX, distanceY);
            if (distance < bestDistance)
            {
                    bestDistance = distance;
                    idealMate = creature;
            }
            
        }

        if (idealMate is not null)
        {
            return idealMate;
        }

        return null;
    }


    //Only for male ones 

    public void Mate(Creature mate , List <WorldPosition> worldpositions , List <Creature> creatures)
    {
        //Find the postiton drop baby
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

        WorldPosition DropPosition = null;

        foreach (WorldPosition position in checkpositions)
        {
            if (position.currentOccCreature is null && position.CurrentOccFood is null)
            {
                DropPosition = position;
                break;
            }
        }

        if(DropPosition is not null){
            Creature baby = new Creature(DropPosition, mate, this);
            creatures.Add(baby);
            DropPosition.currentOccCreature = baby;
            this.Energy = this.Energy - 30;
            mate.Energy = this.Energy - 5;
            this.Kids.Add(baby);
            mate.Kids.Add(baby);
     
        }

    }


    public void Think()
{
    Dictionary<string, bool> state = GetState();

    List<GoapAction> actions = GetActions();

    Dictionary<string, bool> goal = GetGoal();

    GoapAction? action = Planner.MakePlan(
        state,
        actions,
        goal
    );

    if (action is null)
    {
        return;
    }

  
}

public void ExecuteAction(
    GoapAction action,
    List<Foods> foods,
    List<WorldPosition> worldpositions,
    List<Creature> creatures)
{
    if (action.Name == "Eat")
    {
        Foods? food = lookforFood(foods);

        if (food is null)
            return;

        if (CurrentPosition == food.position)
        {
            eat(food, foods);
        }
        else
        {
            walk(food.position, worldpositions);
        }
    }

    else if (action.Name == "Mate")
    {
        Creature? mate = lookforaMate(creatures);

        if (mate is null)
            return;

        int distanceX =
            Math.Abs(mate.CurrentPosition.X - CurrentPosition.X);

        int distanceY =
            Math.Abs(mate.CurrentPosition.Y - CurrentPosition.Y);

        int distance = Math.Max(distanceX, distanceY);

        if (distance <= 1)
        {
            Mate(mate, worldpositions, creatures);
        }
        else
        {
            walk(mate.CurrentPosition, worldpositions);
        }
    }
}

}



