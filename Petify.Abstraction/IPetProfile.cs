using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Abstraction
{
    public interface IPetProfile
    {
        void SavePetProfile(Pet value);
        void DeletePetProfile(int Id);
        void UpdatePetProfile(int Id, Pet value);
        Pet GetPetProfileByName(string name);
        Pet GetValue(string name, Pet defaultValue);
        Pet GetPetProfileById(int Id);
        List<Pet> GetPetProfile();
        List<Pet> GetPetProfileByUserId(string userId);

    }
}
