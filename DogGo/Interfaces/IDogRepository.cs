﻿using DogGo.Models;
using System.Collections.Generic;

namespace DogGo.Interfaces
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
        List<Dog> GetDogsByOwnerId(int ownerId);
        Dog GetDogById(int id);
        void AddDog(Dog dog);
        void UpdateDog(Dog dog);
        void DeleteDog(int id);
    }
}
