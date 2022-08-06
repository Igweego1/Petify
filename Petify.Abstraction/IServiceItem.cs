using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Abstraction
{
    public interface IServiceItem
    {
        void SaveService(Service value);
        void DeleteService(int Id);
        void UpdateService(int Id, Service value);
        Service GetServiceByName(string name);
        Service GetValue(string name, Service defaultValue);
        Service GetServiceById(int Id);
        List<Service> GetListServices();
        List<Service> GetServiceListByUserId(string userId);
    }
}
