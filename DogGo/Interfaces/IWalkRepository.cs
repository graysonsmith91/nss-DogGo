using DogGo.Models;
using System.Collections.Generic;

namespace DogGo.Interfaces
{
    public interface IWalkRepository
    {
        List<Walk> GetAllWalks();
        List<Walk> GetWalksByWalkerId(int walkerId);
    }
}