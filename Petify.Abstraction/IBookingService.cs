using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Abstraction
{
    public interface IBookingService
    {
        void SaveBooking(Booking value);
        void DeleteBooking(int Id);
        void UpdateBooking(int Id, Booking value);
        Booking GetBookingByName(string name);
        Booking GetValue(string name, Booking defaultValue);
        Booking GetBookingById(int Id);
        List<Booking> GetBookingList();
        List<Booking> GetBookingByUserId(string userId);
    }
}
