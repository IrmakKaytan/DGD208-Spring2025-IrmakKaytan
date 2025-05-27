using System;

namespace DGD208_Spring2025_IrmakKaytan
{
    public class Pet
    {
        public string Name { get; private set; }
        public PetType Type { get; private set; }
        public int Hunger { get; private set; }
        public int Sleep { get; private set; }
        public int Fun { get; private set; }

        public Pet(string name, PetType type)
        {
            Name = name;
            Type = type;
            Hunger = 50;
            Sleep = 50;
            Fun = 50;
        }

        // Methods will be implemented in future iterations
        public void DecreaseStats()
        {
            // To be implemented
        }

        public void IncreaseHunger(int amount)
        {
            // To be implemented
        }

        public void IncreaseSleep(int amount)
        {
            // To be implemented
        }

        public void IncreaseFun(int amount)
        {
            // To be implemented
        }

        public bool IsAlive()
        {
            // To be implemented
            return true;
        }
    }
} 