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
    public class ServiceItem : IServiceItem
    {
        private readonly PetifyLiveContext _context;
        public ServiceItem(PetifyLiveContext context)
        {
            _context = context;
        }
        public void DeleteService(int Id)
        {
            var serviceContains = _context.Services.FirstOrDefault(p => p.Id == Id);
            _context.Entry(serviceContains).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public List<Service> GetListServices()
        {
           return _context.Services.ToList();
        }

        public Service GetServiceById(int Id)
        {
            if(Id != 0)
            {
                var result = _context.Services.Where(x => x.Id == Id).FirstOrDefault();
                return result;
            }
            else
            {
                return null;
            }
        }

        public Service GetServiceByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Service> GetServiceListByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Service GetValue(string name, Service defaultValue)
        {
            throw new NotImplementedException();
        }

        public void SaveService(Service value)
        {
            if(value != null)
            {
                _context.Services.Add(value);
                _context.SaveChanges();
            }
        }

        public void UpdateService(int Id, Service value)
        {
            if(Id != 0)
            {
                var serviceContains = _context.Services.FirstOrDefault(p => p.Id == Id);

                serviceContains.ServiceName = value.ServiceName;

                _context.Entry(serviceContains).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}
