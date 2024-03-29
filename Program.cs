﻿using ExtraVert;
using Microsoft.VisualBasic;

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
        Sold = true,
        AvailableUntil = new DateTime(2024, 1, 7)
    },
    new Plant()
    {
        Id = 2,
        Species = "Dandelion",
        LightNeeds = 2,
        AskingPrice = 33.99M,
        City = "Nashville",
        Zip = 38389,
        Sold = true,
        AvailableUntil = new DateTime(2024, 1, 8)
    },
    new Plant()
    {
        Id = 3,
        Species = "Kevin",
        LightNeeds = 4,
        AskingPrice = 18.47M,
        City = "Texas",
        Zip = 47872,
        Sold = false,
        AvailableUntil = new DateTime(2023, 10, 22)
    },
    new Plant()
    {
        Id = 2,
        Species = "Rick",
        LightNeeds = 1,
        AskingPrice = 82.38M,
        City = "Dubai",
        Zip = 27983,
        Sold = false,
        AvailableUntil = DateTime.Now
    }
};

Random random = new Random();

string greeting = $"Welcome to ExtraVert, your floral one-stop shop.";
Console.WriteLine(greeting);


string choice = null;

while (choice != "0")
{
    Console.WriteLine(@"
How can we help you today?
        0. Exit
        1. Display All Plants
        2. Post a Plant
        3. Adopt a Plant
        4. Delist a Plant
        5. Plant of the Day
        6. Search by Light Needed
        7. View App Stats");
    choice = Console.ReadLine();

    if (choice == "0")
    {
        Console.WriteLine("\nThanks for visiting! Come again\n");
    } 
    else if (choice == "1")
    {
        try
        {
            Console.Clear();
            Console.WriteLine("\nAll plants:");
            ListPlants();
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error:", ex.Message);
        }

    }
    else if (choice == "2")
    {
        try
        {
            Console.Clear();
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
            Console.Clear();
            AdoptPlant(plants);
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error:", ex.Message);
        }
    }
    else if (choice == "4")
    {
        try
        {
            Console.Clear();
            DelistPlant(plants);
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error:", ex.Message);
        }
    }
    else if (choice == "5")
    {
        try
        {
            Console.Clear();
            DailyPlant();
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error:", ex.Message);
        }
    }
    else if (choice == "6")
    {
        try
        {
            Console.Clear();
            SearchByLightNeeds(plants);
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error:", ex.Message);
        }
    }
    else if (choice == "7")
    {
        try
        {
            Console.Clear();
            ViewStats();
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: ", ex.Message);
        }
    }
}

void ListPlants()
{
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. A {plants[i].Species} in {plants[i].City} {(plants[i].Sold ? "was sold" : $"is available until {plants[i].AvailableUntil.Date}")} for {plants[i].AskingPrice} dollars");
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

    Console.WriteLine("Enter when your listing will expire: MM/DD/YYYY");
    try
    {
        var userInput = Console.ReadLine();
        DateTime date = ConvertToDate(userInput);
        createdPlant.AvailableUntil = date.Date;
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Invalid format, try again: ", ex.Message);
    }

    // Ensuring the plant doesn't get added as sold 
    createdPlant.Sold = false;

    plants.Add(createdPlant);
    Console.WriteLine($"\nSuccess! {createdPlant.Species} has been posted.\n");
}

void AdoptPlant(List<Plant> plants)
{
//    var availability = plants.Where(plant => plant.AvailableUntil <= DateTime.Now);

    if (plants.All(plant => plant.Sold)) // if all plants are sold & not available, display none available
    {
        Console.WriteLine("There are no available plants yet. Come back later!");
        return;
    }
    else
    {
        Console.WriteLine("\nAvailable plants:");
        for (int i = 0; i < plants.Count; i++)
        {
            if (!plants[i].Sold && plants[i].AvailableUntil <= DateTime.Now) 
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
            else // if you can succeed ^ you can also fail .. just in case
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
    Console.WriteLine("Choose a plant to de-list:");

    for (int i = 0;i < plants.Count;i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species}");
    }

    if (int.TryParse(Console.ReadLine(), out int choiceIndex))
    {
        plants.RemoveAt(choiceIndex - 1);
        Console.WriteLine("Successfully de-listed.");
    }
    else
    {
        Console.WriteLine("There was an error.");
    }    

}

void DailyPlant()
{
    if (plants.All(plant => plant.Sold))
    {
        Console.WriteLine("No plants available! Check back soon");
    }
    else
    {
        int randomPlantIndex;

        do
        {
            randomPlantIndex = random.Next(0, plants.Count - 1);
        } while (plants[randomPlantIndex].Sold);

        var species = plants[randomPlantIndex].Species;
        var location = plants[randomPlantIndex].City;
        var lightNeeds = plants[randomPlantIndex].LightNeeds;
        var price = plants[randomPlantIndex].AskingPrice;

        Console.WriteLine(@$"Plant of the Day: {species}!
        Located in {location}, with a required-light score of {lightNeeds},
        the {species} is ExtraVert's Plant of the Day!
        Buy now for {price}!");
    }


}

void SearchByLightNeeds(List<Plant> plants)
{
    Console.WriteLine("Enter a maximum light-needs number (1 - 5):");
    try
    {
        if (int.TryParse(Console.ReadLine(), out int choiceIndex))
        {
            foreach (Plant plant in plants)
            {
                if (plant.LightNeeds <= choiceIndex)
                {
                    Console.WriteLine($"{plant.Species} has a light-needs number of {plant.LightNeeds}");
                }
            }
        }
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Please only enter number between 1-5");
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine("Please stay between numbers 1-5");
    }
    catch(Exception ex)
    {
        Console.WriteLine("There was an error:", ex.Message);
    }
}

static DateTime ConvertToDate(string input) // function to parse the string response into a valid datetime format
{
    string[] format = { "MM/dd/yyyy" };
    DateTime parsedDateTime = DateTime.ParseExact(input, format, null, System.Globalization.DateTimeStyles.None);
    return parsedDateTime;
}

void ViewStats()
{
    // lowest priced plant
    decimal lowestPrice = plants.Min(plants => plants.AskingPrice);
    foreach (Plant plant in plants)
    {
        if (plant.AskingPrice == lowestPrice)
        {
            Console.WriteLine($"The lowest priced plant we have is the {plant.Species}, with an asking price of {plant.AskingPrice}");
        }
    }

    // number of plants available
    Console.WriteLine("\nOur available plants:");
    var plantsAvailable = plants.Any(plant => !plant.Sold);
    foreach (Plant plant in plants)
    {
        if (plantsAvailable)
        {
            Console.WriteLine($"{plant.Species}");
        }
    }    

    // name of plant with highest light needs
    Console.WriteLine("\nName of plants with most high light needs:");
    var highestLightNeeds = plants.Max(plant => plant.LightNeeds);
    foreach (Plant p in plants)
    {
        if (p.LightNeeds == highestLightNeeds)
        {
            Console.WriteLine($"{p.Species}, with a score of {p.LightNeeds}");
        }
    }

    // average light needs
    Console.WriteLine("\nAverage light needs:");
    double averageLight = plants.Average(plant => plant.LightNeeds);
    Console.WriteLine($"{averageLight:F}");

    // percentage of plants adopted
    int totalPlants = plants.Count();
    int adoptedPlants = plants.Count(p => p.Sold);
    double adoptionPercentage = (double)adoptedPlants / totalPlants * 100;
    Console.WriteLine("\nPercentage of Adopted Plants:");
    Console.WriteLine($"{adoptionPercentage}%!");

}

string PlantDetails(Plant plant)
{
    string plantString = @$"
    Species: {plant.Species}
    Light Needs: {plant.LightNeeds}
    Price: {plant.AskingPrice}
    City: {plant.City}
    Zip: {plant.Zip}
    Availablity: {(plant.Sold ? "Sold" : "Available!")}
    Expiration: {plant.AvailableUntil.ToShortDateString}";

    return plantString;
}