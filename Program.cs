using ExtraVert;

List<Plant> plants = new List<Plant>()
{
    new Plant()
    {
        Id = 1,
        Species = "Rose",
        LightNeeds = 4,
        AskingPrice = 20.00M,
        City = "Lebanon",
        Zip = 37087,
        Sold = true
    },
    new Plant()
    {
        Id = 2,
        Species = "Dandelion",
        LightNeeds = 2,
        AskingPrice = 33.99M,
        City = "Nashville",
        Zip = 38389,
        Sold = false
    },
    new Plant()
    {
        Id = 3,
        Species = "Kevin",
        LightNeeds = 4,
        AskingPrice = 18.47M,
        City = "Texas",
        Zip = 47872,
        Sold = false
    },
    new Plant()
    {
        Id = 2,
        Species = "Rick",
        LightNeeds = 1,
        AskingPrice = 82.38M,
        City = "Dubai",
        Zip = 27983,
        Sold = false
    }
};

string greeting = $"Welcome to ExtraVert, your floral one-stop shop.";
Console.WriteLine(greeting);

string choice = null;

while (choice != "0")
{
    Console.WriteLine(@"
How can we help you today?
        0. Exit
        1. Display All Plants
        2. Post a Plant to be Adopted
        3. Adopt a Plant
        4. Delist a Plant");
    choice = Console.ReadLine();

    if (choice == "0")
    {
        Console.WriteLine("\nThanks for visiting! Come again\n");
    } 
    else if (choice == "1")
    {
        try
        {
            Console.WriteLine("\nAll plants:");
            ListPlants();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("There was an error.");
        }

    }
    else if (choice == "2")
    {
        try
        {
            PostPlant(plants);
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error:", ex.Message);
        }
    }
    else if (choice == "3")
    {
        try
        {
            AdoptPlant(plants);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("There was an error.");
        }
    }
    else if (choice == "4")
    {
        try
        {
            DelistPlant(plants);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("There was an error.");
        }
    }
}

void ListPlants()
{
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. A {plants[i].Species} in {plants[i].City} {(plants[i].Sold ? "was sold" : "is available")} for {plants[i].AskingPrice} dollars");
    }
}

void PostPlant(List<Plant> plants)
{
    Plant createdPlant = new Plant();
    Console.WriteLine("\nAdd a new plant\n");

    
    Console.WriteLine("Type the species:");
    string name = Console.ReadLine();
    createdPlant.Species = name;

    Console.WriteLine("Type how much light it needs (1-5):");
    string lightNeeded = Console.ReadLine();
    int lightResult = int.Parse(lightNeeded);
    int light = lightResult;
    createdPlant.LightNeeds = light;

    Console.WriteLine("Type the price:");
    string askingPrice = Console.ReadLine();
    decimal priceConversion = decimal.Parse(askingPrice);
    createdPlant.AskingPrice = priceConversion;

    Console.WriteLine("Enter the name of your city:");
    string city = Console.ReadLine();
    createdPlant.City = city;
    
    Console.WriteLine("Enter your zip code:");
    int zipCode = Convert.ToInt32(Console.ReadLine());
    createdPlant.Zip = zipCode;

    // Ensuring the plant doesn't get added as sold 
    createdPlant.Sold = false;

    plants.Add(createdPlant);
    Console.WriteLine($"\nSuccess! {createdPlant.Species} has been posted.\n");
}

void AdoptPlant(List<Plant> plants)
{
    if (plants.All(plant => plant.Sold)) // if all plants are sold, display none available
    {
        Console.WriteLine("There are no available plants yet. Come back later!");
        return;
    }
    else
    {
        Console.WriteLine("\nAvailable plants:");
        for (int i = 0; i < plants.Count; i++)
        {
            if (!plants[i].Sold)
            {
                Console.WriteLine($"{i + 1}. {plants[i].Species}");
            }  
            
        }

        Console.WriteLine("Choose which plant to adopt:");
        if (int.TryParse(Console.ReadLine(), out int choiceIndex)) // converts console response into an integer and turns that into a usable variable
        {
            if (!plants[choiceIndex - 1].Sold) // if the specific plant is NOT SOLD, when chosen, change sold to true
            {
                plants[choiceIndex - 1].Sold = true;
                Console.WriteLine($"Nice! You adopted {plants[choiceIndex - 1].Species}!");
            }
            else // if you can succeed ^ you can also fail v .. just in case
            {
                Console.WriteLine($"Invalid choice, {plants[choiceIndex - 1].Species} is not available.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input, try again");
        }
    }
}

void DelistPlant(List<Plant> plants)
{
    for (int i = 0;i < plants.Count;i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species}");
    }

    if (int.TryParse(Console.ReadLine(), out int choiceIndex))
    {
        plants.RemoveAt(choiceIndex - 1);
        Console.WriteLine($"Success!");
    }
    else
    {
        Console.WriteLine("There was an error.");
    }    
}