using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Text.Json;
using System.IO;
using System.Text;

namespace DGD208_Spring2025_IrmakKaytan;

public class Game
{
    private readonly Menu mainMenu;
    private bool isRunning;
    private List<Pet> adoptedPets;
    private List<Item> availableItems;
    private CancellationTokenSource statDecreaseCts;
    private const string SAVE_FILE = "savedata.json";

    private string GetStatBar(int value)
    {
        const int BAR_LENGTH = 10;
        int filledSegments = (int)Math.Round((double)value / 100 * BAR_LENGTH);
        StringBuilder bar = new StringBuilder();
        
        // Add filled segments with spaces between them
        for (int i = 0; i < BAR_LENGTH; i++)
        {
            if (i < filledSegments)
            {
                bar.Append("█ ");
            }
            else
            {
                bar.Append("░ ");
            }
        }
        
        return bar.ToString().TrimEnd();
    }

    private void WriteColoredLine(string text, ConsoleColor color)
    {
        var prev = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = prev;
    }

    public Game()
    {
        mainMenu = new Menu(new[]
        {
            "1. Adopt a Virus",
            "2. View Your Viruses",
            "3. Use System Resource",
            "4. Save Game",
            "5. Load Game",
            "6. About",
            "7. Exit"
        });
        adoptedPets = new List<Pet>();
        isRunning = true;
        InitializeItems();
        StartStatDecreaseTask();
    }

    private void InitializeItems()
    {
        availableItems = new List<Item>
        {
            new Item("RAM Stick", ItemType.RAM, 2000,
                "Memory supplements satiate the virus's appetite but deprive it of sleep."),
            new Item("CPU Core", ItemType.CPU, 2000,
                "It relaxes the processor, gives it a rest, but leaves it hungry and bored."),
            new Item("Hard Drive Space", ItemType.DiskSpace, 2000, 
                "With more storage to explore, the virus has the time of its life—so much that it forgets to eat and loses sleep over the excitement.")
        };
    }

    private void StartStatDecreaseTask()
    {
        statDecreaseCts = new CancellationTokenSource();
        Task.Run(async () =>
        {
            while (!statDecreaseCts.Token.IsCancellationRequested)
            {
                await Task.Delay(12000, statDecreaseCts.Token); // Wait for 12 seconds (5 times per minute)
                
                lock (adoptedPets) // Thread-safe access to the list
                {
                    for (int i = adoptedPets.Count - 1; i >= 0; i--)
                    {
                        var pet = adoptedPets[i];
                        pet.DecreaseStats();
                        
                        if (!pet.IsAlive())
                        {
                            Console.WriteLine($"\n{pet.Name} has died!");
                            Console.WriteLine($"Type: {pet.Type}");
                            Console.WriteLine($"Final Stats:");
                            Console.WriteLine($"   Hunger: {GetStatBar(pet.Hunger)} {pet.Hunger}%");
                            Console.WriteLine($"   Sleep:  {GetStatBar(pet.Sleep)} {pet.Sleep}%");
                            Console.WriteLine($"   Fun:    {GetStatBar(pet.Fun)} {pet.Fun}%");
                            Console.WriteLine("\nPress any key to continue...");
                            adoptedPets.RemoveAt(i);
                        }
                    }
                }
            }
        }, statDecreaseCts.Token);
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
                    SaveGame();
                    break;
                case 5:
                    LoadGame();
                    break;
                case 6:
                    ShowAbout();
                    break;
                case 7:
                    isRunning = false;
                    statDecreaseCts.Cancel(); // Stop the background task
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
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose a virus to adopt:");
            Console.WriteLine();
            Console.WriteLine("1. MemoryEater – A RAM-consuming virus");
            Console.WriteLine("   \"This glutton lives in your RAM and snacks on memory sticks like candy.");
            Console.WriteLine("   It gets hungry fast, but doesn't need much fun or sleep.\"");
            Console.WriteLine("   Strategy Tip: Feed it often or it'll vanish into the void.");
            Console.WriteLine();
            Console.WriteLine("2. FunMiner – A CPU-intensive virus");
            Console.WriteLine("   \"All it wants is entertainment! This virus hijacks your processor just to play silly games.");
            Console.WriteLine("   Constantly bored, a little bit hungry, and okay with staying up late.\"");
            Console.WriteLine("   Strategy Tip: Keep it amused or it'll get bored to death. Literally.");
            Console.WriteLine();
            Console.WriteLine("3. SleepCrawler – A background process virus");
            Console.WriteLine("   \"Runs quietly in the background... until it crashes from exhaustion.");
            Console.WriteLine("   Needs lots of sleep, isn't very hungry, and occasionally wants some fun.\"");
            Console.WriteLine("   Strategy Tip: Let it rest often or you'll watch it burn out like a forgotten laptop.");
            Console.WriteLine();
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
                break;
            }
            else if (choice == "4")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                Thread.Sleep(1000);
            }
        }
    }

    private void ShowViewPetsMenu()
    {
        Console.Clear();
        if (adoptedPets.Count == 0)
        {
            Console.WriteLine("You haven't adopted any viruses yet!");
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Your Viruses:");
        for (int i = 0; i < adoptedPets.Count; i++)
        {
            var pet = adoptedPets[i];
            Console.WriteLine($"\n{i + 1}. {pet.Name} ({pet.Type})");
            Console.WriteLine($"   Hunger: {GetStatBar(pet.Hunger)} {pet.Hunger}%");
            Console.WriteLine();
            Console.WriteLine($"   Sleep:  {GetStatBar(pet.Sleep)} {pet.Sleep}%");
            Console.WriteLine();
            Console.WriteLine($"   Fun:    {GetStatBar(pet.Fun)} {pet.Fun}%");
            Console.WriteLine();
        }
        Console.WriteLine("\nPress any key to return to the main menu...");
        Console.ReadKey();
    }

    private async void ShowUseItemMenu()
    {
        while (true)
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
                Console.WriteLine("Invalid choice. Please try again.");
                Thread.Sleep(1000);
                continue;
            }
            if (selectedPetIndex == adoptedPets.Count + 1)
            {
                break;
            }
            var selectedPet = adoptedPets[selectedPetIndex - 1];
            // Show item selection menu with descriptions
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{selectedPet.Name} ({selectedPet.Type})");
                Console.WriteLine("\nSelect an item to use:");
                for (int i = 0; i < availableItems.Count; i++)
                {
                    var item = availableItems[i];
                    Console.WriteLine($"{i + 1}. {item.Name}");
                    Console.WriteLine($"   {item.Description}");
                    Console.WriteLine();
                }
                Console.WriteLine($"{availableItems.Count + 1}. Back");
                var itemChoice = Console.ReadLine();
                if (!int.TryParse(itemChoice, out int selectedItemIndex) || 
                    selectedItemIndex < 1 || 
                    selectedItemIndex > availableItems.Count + 1)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    Thread.Sleep(1000);
                    continue;
                }
                if (selectedItemIndex == availableItems.Count + 1)
                {
                    break;
                }
                var selectedItem = availableItems[selectedItemIndex - 1];
                await selectedItem.Use(selectedPet);
                // Check if pet died after using the item
                if (!selectedPet.IsAlive())
                {
                    Console.WriteLine($"\n{selectedPet.Name} has died!");
                    Console.WriteLine($"Type: {selectedPet.Type}");
                    Console.WriteLine($"Final Stats:");
                    Console.WriteLine($"   Hunger: {GetStatBar(selectedPet.Hunger)} {selectedPet.Hunger}%");
                    Console.WriteLine();
                    Console.WriteLine($"   Sleep:  {GetStatBar(selectedPet.Sleep)} {selectedPet.Sleep}%");
                    Console.WriteLine();
                    Console.WriteLine($"   Fun:    {GetStatBar(selectedPet.Fun)} {selectedPet.Fun}%");
                    Console.WriteLine();
                    Console.WriteLine("\nPress any key to continue...");
                    adoptedPets.Remove(selectedPet);
                    Console.ReadKey();
                    break;
                }
                break;
            }
            break;
        }
    }

    private void ShowAbout()
    {
        Console.WriteLine("\nByteMonster: Digital Pet Viruses");
        Console.WriteLine("Created by: Irmak Kaytan");
        Console.WriteLine("Student Number: 2305041084");
    }

    private void SaveGame()
    {
        try
        {
            var saveData = new SaveData();
            foreach (var pet in adoptedPets)
            {
                saveData.Pets.Add(new PetData(pet));
            }

            string jsonString = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SAVE_FILE, jsonString);
            Console.WriteLine("\nGame saved successfully!");
            Console.WriteLine($"Save time: {saveData.SaveTime}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError saving game: {ex.Message}");
        }
    }

    private void LoadGame()
    {
        try
        {
            if (!File.Exists(SAVE_FILE))
            {
                Console.WriteLine("\nNo save file found!");
                return;
            }

            string jsonString = File.ReadAllText(SAVE_FILE);
            var saveData = JsonSerializer.Deserialize<SaveData>(jsonString);

            if (saveData == null || saveData.Pets.Count == 0)
            {
                Console.WriteLine("\nSave file is empty or corrupted!");
                return;
            }

            // Stop the current stat decrease task
            statDecreaseCts.Cancel();
            
            // Clear current pets and load saved ones
            adoptedPets.Clear();
            foreach (var petData in saveData.Pets)
            {
                adoptedPets.Add(petData.ToPet());
            }

            // Restart the stat decrease task
            StartStatDecreaseTask();

            Console.WriteLine("\nGame loaded successfully!");
            Console.WriteLine($"Save time: {saveData.SaveTime}");
            Console.WriteLine($"Loaded {adoptedPets.Count} viruses");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError loading game: {ex.Message}");
        }
    }
}