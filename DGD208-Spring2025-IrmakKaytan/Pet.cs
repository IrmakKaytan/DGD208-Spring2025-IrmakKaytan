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

        public void DecreaseStats()
        {
            Hunger = Math.Max(0, Hunger - 1);
            Sleep = Math.Max(0, Sleep - 1);
            Fun = Math.Max(0, Fun - 1);
        }

        public void IncreaseHunger(int amount)
        {
            Hunger = Math.Min(100, Hunger + amount);
        }

        public void IncreaseSleep(int amount)
        {
            Sleep = Math.Min(100, Sleep + amount);
        }

        public void IncreaseFun(int amount)
        {
            Fun = Math.Min(100, Fun + amount);
        }

        public bool IsAlive()
        {
            return Hunger > 0 && Sleep > 0 && Fun > 0;
        }
    }
} 