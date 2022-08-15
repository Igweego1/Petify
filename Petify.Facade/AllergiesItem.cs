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
    public class AllergiesItem : IAllergiesItem
    {
        private readonly PetifyLiveContext _context;
        public AllergiesItem(PetifyLiveContext context)
        {
            _context = context;
        }
        public void DeleteAllergy(int Id)
        {
            var allergyContains = _context.Allergies.FirstOrDefault(x => x.Id == Id);
            _context.Entry(allergyContains).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public Allergy GetAllergyById(int Id)
        {
            if (Id != 0)
            {
                var result = _context.Allergies.Where(x => x.Id == Id).FirstOrDefault();
                return result;
            }
            else
            {
                return null;
            }
        }

        public Allergy GetAllergyByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Allergy> GetAllergyListByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public List<Allergy> GetListAllergy()
        {
            return _context.Allergies.ToList();
        }

        public Allergy GetValue(string name, Allergy defaultValue)
        {
            throw new NotImplementedException();
        }

        public void SaveAllergy(Allergy value)
        {
            if (value != null)
            {
                _context.Allergies.Add(value);
                _context.SaveChanges();
            }
        }

        public void UpdateAllergy(int Id, Allergy value)
        {
            if (Id != 0)
            {
                var allergyContains = _context.Allergies.FirstOrDefault(p => p.Id == Id);

                allergyContains.AllergyName = value.AllergyName;

                _context.Entry(allergyContains).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}
