using System;
using System.Threading.Tasks;

namespace DGD208_Spring2025_IrmakKaytan
{
    public class Item
    {
        public string Name { get; private set; }
        public ItemType Type { get; private set; }
        public int UseDuration { get; private set; } // Duration in milliseconds
        public string Description { get; private set; }

        public Item(string name, ItemType type, int useDuration, string description)
        {
            Name = name;
            Type = type;
            UseDuration = useDuration;
            Description = description;
        }

        public async Task Use(Pet pet)
        {
            Console.WriteLine($"\nUsing {Name} on {pet.Name}...");
            Console.WriteLine(Description);
            
            // Show progress
            for (int i = 0; i < 10; i++)
            {
                Console.Write(".");
                await Task.Delay(UseDuration / 10);
            }
            Console.WriteLine();

            // Apply effects based on item type
            switch (Type)
            {
                case ItemType.RAM:
                    pet.IncreaseHunger(30);
                    pet.IncreaseSleep(-10);
                    pet.IncreaseFun(0);
                    Console.WriteLine($"{pet.Name}'s stats changed:");
                    Console.WriteLine($"Hunger: +30");
                    Console.WriteLine($"Sleep: -10");
                    Console.WriteLine($"Fun: +0");
                    break;
                case ItemType.CPU:
                    pet.IncreaseHunger(-10);
                    pet.IncreaseSleep(30);
                    pet.IncreaseFun(-10);
                    Console.WriteLine($"{pet.Name}'s stats changed:");
                    Console.WriteLine($"Hunger: -10");
                    Console.WriteLine($"Sleep: +30");
                    Console.WriteLine($"Fun: -10");
                    break;
                case ItemType.DiskSpace:
                    pet.IncreaseHunger(15);
                    pet.IncreaseSleep(10);
                    pet.IncreaseFun(5);
                    Console.WriteLine($"{pet.Name}'s stats changed:");
                    Console.WriteLine($"Hunger: +15");
                    Console.WriteLine($"Sleep: +10");
                    Console.WriteLine($"Fun: +5");
                    break;
            }
        }
    }
} 