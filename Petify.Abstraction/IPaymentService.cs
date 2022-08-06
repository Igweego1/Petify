using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Abstraction
{
    public interface IPaymentService
    {
        void SavePayment(Payment value);
        void DeletePayment(int Id);
        void UpdatePayment(int Id, Payment value);
        Payment GetPaymentByName(string name);
        Payment GetValue(string name, Payment defaultValue);
        Payment GetPaymentById(int Id);
        List<Payment> GetPaymentList();
        List<Payment> GetPaymentByUserId(string userId);
    }
}
