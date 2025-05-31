using System;
using System.Threading.Tasks;

namespace DGD208_Spring2025_IrmakKaytan
{
    public class Item
    {
        public string Name { get; private set; }
        public ItemType Type { get; private set; }
        public int EffectAmount { get; private set; }
        public int UseDuration { get; private set; } // Duration in milliseconds

        public Item(string name, ItemType type, int effectAmount, int useDuration)
        {
            Name = name;
            Type = type;
            EffectAmount = effectAmount;
            UseDuration = useDuration;
        }

        public async Task Use(Pet pet)
        {
            Console.WriteLine($"\nUsing {Name} on {pet.Name}...");
            
            // Show progress
            for (int i = 0; i < 10; i++)
            {
                Console.Write(".");
                await Task.Delay(UseDuration / 10);
            }
            Console.WriteLine();

            // Apply effect based on item type
            switch (Type)
            {
                case ItemType.RAM:
                    pet.IncreaseHunger(EffectAmount);
                    Console.WriteLine($"{pet.Name}'s hunger increased by {EffectAmount}!");
                    break;
                case ItemType.CPU:
                    pet.IncreaseFun(EffectAmount);
                    Console.WriteLine($"{pet.Name}'s fun increased by {EffectAmount}!");
                    break;
                case ItemType.DiskSpace:
                    pet.IncreaseSleep(EffectAmount);
                    Console.WriteLine($"{pet.Name}'s sleep increased by {EffectAmount}!");
                    break;
            }
        }
    }
} 