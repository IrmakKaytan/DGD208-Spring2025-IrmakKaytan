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

        // Timers for each stat
        private int hungerTimer;
        private int sleepTimer;
        private int funTimer;

        public Pet(string name, PetType type)
        {
            Name = name;
            Type = type;
            Hunger = 50;
            Sleep = 50;
            Fun = 50;
            ResetTimers();
        }

        private void ResetTimers()
        {
            switch (Type)
            {
                case PetType.MemoryEater:
                    hungerTimer = 1;  // 1 minute
                    sleepTimer = 3;   // 3 minutes
                    funTimer = 3;     // 3 minutes
                    break;
                case PetType.FunMiner:
                    hungerTimer = 2;  // 2 minutes
                    sleepTimer = 3;   // 3 minutes
                    funTimer = 1;     // 1 minute
                    break;
                case PetType.SleepCrawler:
                    hungerTimer = 3;  // 3 minutes
                    sleepTimer = 1;   // 1 minute
                    funTimer = 2;     // 2 minutes
                    break;
            }
        }

        public void DecreaseStats()
        {
            // Decrease timers
            hungerTimer--;
            sleepTimer--;
            funTimer--;

            // Decrease stats based on timers
            if (hungerTimer <= 0)
            {
                Hunger = Math.Max(0, Hunger - 1);
                ResetHungerTimer();
            }

            if (sleepTimer <= 0)
            {
                Sleep = Math.Max(0, Sleep - 1);
                ResetSleepTimer();
            }

            if (funTimer <= 0)
            {
                Fun = Math.Max(0, Fun - 1);
                ResetFunTimer();
            }
        }

        private void ResetHungerTimer()
        {
            switch (Type)
            {
                case PetType.MemoryEater:
                    hungerTimer = 1;  // 1 minute
                    break;
                case PetType.FunMiner:
                    hungerTimer = 2;  // 2 minutes
                    break;
                case PetType.SleepCrawler:
                    hungerTimer = 3;  // 3 minutes
                    break;
            }
        }

        private void ResetSleepTimer()
        {
            switch (Type)
            {
                case PetType.MemoryEater:
                    sleepTimer = 3;   // 3 minutes
                    break;
                case PetType.FunMiner:
                    sleepTimer = 3;   // 3 minutes
                    break;
                case PetType.SleepCrawler:
                    sleepTimer = 1;   // 1 minute
                    break;
            }
        }

        private void ResetFunTimer()
        {
            switch (Type)
            {
                case PetType.MemoryEater:
                    funTimer = 3;     // 3 minutes
                    break;
                case PetType.FunMiner:
                    funTimer = 1;     // 1 minute
                    break;
                case PetType.SleepCrawler:
                    funTimer = 2;     // 2 minutes
                    break;
            }
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

        public string GetDescription()
        {
            return Type switch
            {
                PetType.MemoryEater => "A RAM boost feeds the virus's appetite but leaves it sleepless.",
                PetType.FunMiner => "A CPU core entertains the virus, but drains its energy fast.",
                PetType.SleepCrawler => "As a background process, it gets tired easily and craves rest.",
                _ => string.Empty
            };
        }
    }
} 