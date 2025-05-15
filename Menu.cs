namespace ByteMonster.Menus;

public class Menu
{
    private readonly string[] options;

    public static Menu CreateMainMenu()
    {
        return new Menu(new[]
        {
            "1. Adopt a new ByteMonster",
            "2. View your ByteMonsters",
            "3. Use items on ByteMonster",
            "4. About",
            "5. Exit"
        });
    }

    public static Menu CreateAdoptionMenu()
    {
        return new Menu(new[]
        {
            "1. MemoryEater - Loves RAM resources",
            "2. FunMiner - CPU mining enthusiast",
            "3. SleepCrawler - Disk space dreamer",
            "4. Back"
        });
    }

    public static Menu CreatePetSelectionMenu(IEnumerable<string> petNames)
    {
        var petOptions = petNames.Select((name, i) => $"{i + 1}. {name}").ToList();
        petOptions.Add($"{petOptions.Count + 1}. Back");
        return new Menu(petOptions.ToArray());
    }

    public static Menu CreateItemSelectionMenu(IEnumerable<string> itemDescriptions)
    {
        var itemOptions = itemDescriptions.ToList();
        itemOptions.Add($"{itemOptions.Count + 1}. Back");
        return new Menu(itemOptions.ToArray());
    }

    public Menu(string[] options)
    {
        this.options = options;
    }

    public int Show(string? prompt = null)
    {
        while (true)
        {
            if (!string.IsNullOrEmpty(prompt))
            {
                Console.WriteLine($"\n{prompt}");
            }
            else
            {
                Console.WriteLine("\nPlease select an option:");
            }

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(options[i]);
            }

            Console.Write("\nYour choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice > 0 &&
                choice <= options.Length)
            {
                return choice;
            }

            Console.WriteLine("\nInvalid choice. Please try again.");
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}