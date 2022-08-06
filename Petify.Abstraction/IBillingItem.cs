using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Abstraction
{
    public interface IBillingItem
    {
        void SaveBillingPriceUnit (Billing value);
        void DeleteBillingPriceUnit (int Id);
        void UpdateBillingPriceUnit(int Id, Billing value);
        Billing GetBillingName(string name);
        Billing GetBillingPriceunitById(int Id);
        List<Billing> GetListBillingPriceUnit();
    }
}
