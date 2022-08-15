using Petify.Abstraction;
using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Facade
{
    public class CheckingService : ICheckingService
    {
        private readonly PetifyLiveContext _db;
        public CheckingService(PetifyLiveContext db)
        {
            _db = db;
        }
        public void DeleteChecking(int Id)
        {
            throw new NotImplementedException();
        }

        public Checking GetCheckingById(int Id)
        {
            if (Id != 0)
            {
                var result = _db.Checkings.Where(x => x.Id == Id).FirstOrDefault();
                return result;
            }
            else
            {
                return null;
            }
        }

        public Checking GetCheckingByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Checking> GetCheckingByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public List<Checking> GetCheckingList()
        {
            return _db.Checkings.ToList();
        }

        public Checking GetValue(string name, Checking defaultValue)
        {
            throw new NotImplementedException();
        }

        public void SaveChecking(Checking value)
        {
            if (value != null)
            {
                _db.Checkings.Add(value);
                _db.SaveChanges();
            }
        }

        public void UpdateChecking(int Id, Checking value)
        {
            throw new NotImplementedException();
        }
    }
}
