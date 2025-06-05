using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DGD208_Spring2025_IrmakKaytan
{
    public class SaveData
    {
        public List<PetData> Pets { get; set; }
        public DateTime SaveTime { get; set; }

        public SaveData()
        {
            Pets = new List<PetData>();
            SaveTime = DateTime.Now;
        }
    }

    public class PetData
    {
        public string Name { get; set; }
        public PetType Type { get; set; }
        public int Hunger { get; set; }
        public int Sleep { get; set; }
        public int Fun { get; set; }

        public PetData() { }

        public PetData(Pet pet)
        {
            Name = pet.Name;
            Type = pet.Type;
            Hunger = pet.Hunger;
            Sleep = pet.Sleep;
            Fun = pet.Fun;
        }

        public Pet ToPet()
        {
            return new Pet(Name, Type)
            {
                Hunger = this.Hunger,
                Sleep = this.Sleep,
                Fun = this.Fun
            };
        }
    }
} 