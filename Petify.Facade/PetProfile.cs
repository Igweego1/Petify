using Microsoft.EntityFrameworkCore;
using Petify.Abstraction;
using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Facade
{
    public class PetProfile : IPetProfile
    {
        private readonly PetifyLiveContext _db;
        public PetProfile(PetifyLiveContext db)
        {
            _db = db;
        }


        public void DeletePetProfile(int Id)
        {
            var petDetails = _db.Pets.FirstOrDefault(p => p.Id == Id);
            _db.Entry(petDetails).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public List<Pet> GetPetProfile()
        {
            return _db.Pets.ToList();
        }

        public Pet GetPetProfileById(int Id)
        {
            if (Id != 0)
            {
                var result = _db.Pets.Where(p => p.Id == Id).FirstOrDefault();
                return result;
            }
            else
            {
                return null;
            }
        }

        public List<Pet> GetPetProfileByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Pet GetPetProfileByName(string name)
        {
            throw new NotImplementedException();
        }

        public Pet GetValue(string name, Pet defaultValue)
        {
            throw new NotImplementedException();
        }

        public void SavePetProfile(Pet value)
        {
            if (value != null)
            {
                _db.Pets.Add(value);
                _db.SaveChanges();
            }
        }

        public void UpdatePetProfile(int Id, Pet value)
        {
            if (Id != 0)
            {
                var petDetails = _db.Pets.FirstOrDefault(p => p.Id == Id);

                petDetails.PetName = value.PetName;
                petDetails.PetGenderId = value.PetGenderId;
                petDetails.AllergyId = value.AllergyId;
                petDetails.PetTypeId = value.PetTypeId;
                petDetails.DateOfBirth = value.DateOfBirth;
                petDetails.DateOfBirth = value.DateOfBirth;
                petDetails.Description = value.Description;
                petDetails.PetAddress = value.PetAddress;
                petDetails.PetCity = value.PetCity;
                petDetails.UserId = value.UserId;
                petDetails.Image = value.Image;
                petDetails.CreatedBy = value.CreatedBy;
                _db.Entry(petDetails).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }
    }
}
