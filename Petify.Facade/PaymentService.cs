using Petify.Abstraction;
using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Facade
{
    public class PaymentService : IPaymentService
    {
        private readonly PetifyLiveContext _db;
        public PaymentService(PetifyLiveContext db)
        {
            _db = db;
        }
        public void DeletePayment(int Id)
        {
            throw new NotImplementedException();
        }

        public Payment GetPaymentById(int Id)
        {
            if (Id != 0)
            {
                var result = _db.Payments.Where(x => x.Id == Id).FirstOrDefault();
                return result;
            }
            else
            {
                return null;
            }
        }

        public Payment GetPaymentByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Payment> GetPaymentByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public List<Payment> GetPaymentList()
        {
            return _db.Payments.ToList();
        }

        public Payment GetValue(string name, Payment defaultValue)
        {
            throw new NotImplementedException();
        }

        public void SavePayment(Payment value)
        {
            if (value != null)
            {
                _db.Payments.Add(value);
                _db.SaveChanges();
            }
        }

        public void UpdatePayment(int Id, Payment value)
        {
            throw new NotImplementedException();
        }
    }
}
