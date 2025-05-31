using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DGD208_Spring2025_IrmakKaytan;

public class Game
{
    private readonly Menu mainMenu;
    private bool isRunning;
    private List<Pet> adoptedPets;
    private List<Item> availableItems;

    public Game()
    {
        mainMenu = new Menu(new[]
        {
            "1. Adopt a Virus",
            "2. View Your Viruses",
            "3. Use System Resource",
            "4. About",
            "5. Exit"
        });
        adoptedPets = new List<Pet>();
        isRunning = true;
        InitializeItems();
    }

    private void InitializeItems()
    {
        availableItems = new List<Item>
        {
            new Item("RAM Stick", ItemType.RAM, 20, 2000),
            new Item("CPU Core", ItemType.CPU, 20, 2000),
            new Item("Hard Drive Space", ItemType.DiskSpace, 20, 2000)
        };
    }

    public void Run()
    {
        Console.WriteLine("Welcome to ByteMonster: Digital Pet Viruses!");
        Console.WriteLine("Your computer has been infected with friendly viruses!");

        while (isRunning)
        {
            Console.Clear();
            var choice = mainMenu.Show();

            switch (choice)
            {
                case 1:
                    ShowAdoptionMenu();
                    break;
                case 2:
                    ShowViewPetsMenu();
                    break;
                case 3:
                    ShowUseItemMenu();
                    break;
                case 4:
                    ShowAbout();
                    break;
                case 5:
                    isRunning = false;
                    break;
            }

            if (isRunning)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }

    private void ShowAdoptionMenu()
    {
        Console.Clear();
        Console.WriteLine("Choose a virus to adopt:");
        Console.WriteLine("1. MemoryEater - A RAM-consuming virus");
        Console.WriteLine("2. FunMiner - A CPU-intensive virus");
        Console.WriteLine("3. SleepCrawler - A background process virus");
        Console.WriteLine("4. Back to main menu");

        var choice = Console.ReadLine();
        if (int.TryParse(choice, out int virusChoice) && virusChoice >= 1 && virusChoice <= 3)
        {
            Console.Write("Enter a name for your virus: ");
            string name = Console.ReadLine();
            
            PetType type = (PetType)(virusChoice - 1);
            Pet newPet = new Pet(name, type);
            adoptedPets.Add(newPet);
            
            Console.WriteLine($"\nYou've adopted {name}, a {type} virus!");
        }
    }

    private void ShowViewPetsMenu()
    {
        Console.Clear();
        if (adoptedPets.Count == 0)
        {
            Console.WriteLine("You haven't adopted any viruses yet!");
            return;
        }

        Console.WriteLine("Your Viruses:");
        for (int i = 0; i < adoptedPets.Count; i++)
        {
            var pet = adoptedPets[i];
            Console.WriteLine($"\n{i + 1}. {pet.Name} ({pet.Type})");
            Console.WriteLine($"   Hunger: {pet.Hunger}");
            Console.WriteLine($"   Sleep: {pet.Sleep}");
            Console.WriteLine($"   Fun: {pet.Fun}");
        }
    }

    private async void ShowUseItemMenu()
    {
        Console.Clear();
        if (adoptedPets.Count == 0)
        {
            Console.WriteLine("You haven't adopted any viruses yet!");
            return;
        }

        // Show pet selection menu
        Console.WriteLine("Select a virus to use an item on:");
        for (int i = 0; i < adoptedPets.Count; i++)
        {
            var pet = adoptedPets[i];
            Console.WriteLine($"{i + 1}. {pet.Name} ({pet.Type})");
        }
        Console.WriteLine($"{adoptedPets.Count + 1}. Back to main menu");

        var petChoice = Console.ReadLine();
        if (!int.TryParse(petChoice, out int selectedPetIndex) || 
            selectedPetIndex < 1 || 
            selectedPetIndex > adoptedPets.Count + 1)
        {
            return;
        }

        if (selectedPetIndex == adoptedPets.Count + 1)
        {
            return;
        }

        var selectedPet = adoptedPets[selectedPetIndex - 1];

        // Show item selection menu
        Console.WriteLine("\nSelect an item to use:");
        for (int i = 0; i < availableItems.Count; i++)
        {
            var item = availableItems[i];
            Console.WriteLine($"{i + 1}. {item.Name}");
        }
        Console.WriteLine($"{availableItems.Count + 1}. Back");

        var itemChoice = Console.ReadLine();
        if (!int.TryParse(itemChoice, out int selectedItemIndex) || 
            selectedItemIndex < 1 || 
            selectedItemIndex > availableItems.Count + 1)
        {
            return;
        }

        if (selectedItemIndex == availableItems.Count + 1)
        {
            return;
        }

        var selectedItem = availableItems[selectedItemIndex - 1];
        await selectedItem.Use(selectedPet);

        // Check if pet died after using the item
        if (!selectedPet.IsAlive())
        {
            Console.WriteLine($"\n{selectedPet.Name} has died!");
            adoptedPets.Remove(selectedPet);
        }
    }

    private void ShowAbout()
    {
        Console.WriteLine("\nByteMonster: Digital Pet Viruses");
        Console.WriteLine("Created by: Irmak Kaytan");
        Console.WriteLine("Student Number: 2305041084");
    }

}