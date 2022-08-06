using Petify.Abstraction;
using Petify.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Facade
{
    public class BookingService : IBookingService
    {
        private readonly PetifyLiveContext _db;

        public BookingService(PetifyLiveContext db)
        {
            _db = db;
        }
        public void DeleteBooking(int Id)
        {
            throw new NotImplementedException();
        }

        public Booking GetBookingById(int Id)
        {
            if (Id != 0)
            {
                var result = _db.Bookings.Where(x => x.Id == Id).FirstOrDefault();
                return result;
            }
            else
            {
                return null;
            }
        }

        public Booking GetBookingByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetBookingByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetBookingList()
        {
            return _db.Bookings.ToList();
        }

        public Booking GetValue(string name, Booking defaultValue)
        {
            throw new NotImplementedException();
        }

        public void SaveBooking(Booking value)
        {
            if (value != null)
            {
                _db.Bookings.Add(value);
                _db.SaveChanges();
            }
        }

        public void UpdateBooking(int Id, Booking value)
        {
            throw new NotImplementedException();
        }
    }
}
