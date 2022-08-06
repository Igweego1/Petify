using Petify.Abstraction;
using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Facade
{
    public class GroomingService : IGroomingService
    {
        private readonly PetifyLiveContext _db;

        public GroomingService(PetifyLiveContext db)
        {
            _db = db;
                
        }
        public void DeleteGrooming(int Id)
        {
            throw new NotImplementedException();
        }

        public Grooming GetGroomingById(int Id)
        {
            if (Id != 0)
            {
                var result = _db.Groomings.Where(x => x.Id == Id).FirstOrDefault();
                return result;
            }
            else
            {
                return null;
            }
        }

        public Grooming GetGroomingByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Grooming> GetGroomingByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public List<Grooming> GetGroomingList()
        {
            return _db.Groomings.ToList();
        }

        public Grooming GetValue(string name, Grooming defaultValue)
        {
            throw new NotImplementedException();
        }

        public void SaveGrooming(Grooming value)
        {
            if(value != null)
            {
                _db.Groomings.Add(value);
                _db.SaveChanges();
            }
        }

        public void UpdateGrooming(int Id, Grooming value)
        {
            throw new NotImplementedException();
        }
    }
}
