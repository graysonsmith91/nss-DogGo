﻿using DogGo.Models;
using System.Collections.Generic;

namespace DogGo.Interfaces
{
    public interface IOwnerRepository
    {
        List<Owner> GetAllOwners();
        Owner GetOwnerById(int id);
        Owner GetOwnerByEmail(string email);
        void AddOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(int id);
    }
}
