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
    public class BillingService : IBillingService
    {
        private readonly PetifyLiveContext _context;
        public BillingService(PetifyLiveContext context)
        {
            _context = context;
        }
        public void DeleteBilling(int Id)
        {
            var billingContains = _context.Billings.FirstOrDefault(x => x.Id == Id);
            _context.Entry(billingContains).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public Billing GetBillingName(string name)
        {
            throw new NotImplementedException();
        }

        public Billing GetBillingById(int Id)
        {
            if (Id != 0)
            {
                var result = _context.Billings.Where(x => x.Id == Id).FirstOrDefault();
                return result;
            }
            else
            {
                return null;
            }
        }

        public List<Billing> GetListBilling()
        {
            return _context.Billings.ToList();
        }

        public void SaveBilling(Billing value)
        {
            if (value != null)
            {
                _context.Billings.Add(value);
                _context.SaveChanges();
            }
        }

        public void UpdateBilling(int Id, Billing value)
        {
            if (Id != 0)
            {
                var billContains = _context.Billings.FirstOrDefault(p => p.Id == Id);

                billContains.Quantity = value.Quantity;
                billContains.PriceUnit = value.PriceUnit;
                billContains.Total = value.Total;
                billContains.Description = value.Description;
                billContains.Status = value.Status;
                billContains.Created = value.Created;
                billContains.CreatedBy = value.CreatedBy;

                _context.Entry(billContains).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}
