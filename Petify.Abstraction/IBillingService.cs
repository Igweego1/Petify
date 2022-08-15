using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Abstraction
{
    public interface IBillingService
    {
        void SaveBilling(Billing value);
        void DeleteBilling(int Id);
        void UpdateBilling(int Id, Billing value);
        Billing GetBillingName(string name);
        Billing GetBillingById(int Id);
        List<Billing> GetListBilling();
    }
}
