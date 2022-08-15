using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Abstraction
{
    public interface ICheckingService
    {
        void SaveChecking(Checking value);
        void DeleteChecking(int Id);
        void UpdateChecking(int Id, Checking value);
        Checking GetCheckingByName(string name);
        Checking GetValue(string name, Checking defaultValue);
        Checking GetCheckingById(int Id);
        List<Checking> GetCheckingList();
        List<Checking> GetCheckingByUserId(string userId);
    }
}
