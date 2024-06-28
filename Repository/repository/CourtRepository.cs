using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repository
{
    public class CourtRepository : BaseRepository<BadmintonCourt>
    {
        public CourtRepository(DBContext context) : base(context)
        {
        }
        public List<BadmintonCourt> getAll()
        {
            var huh = Context.BadmintonCourts.Include(c => c.VenueServiceTimes).Include(c => c.Location);
            return huh.ToList();
        }
        public List<VenueServiceTime> getAllV(int id)
        {
            return Context.VenueServiceTimes.Include(c => c.TimeSlot).Where(c => c.CourtId == id).ToList();
        }
        public bool IsTimeSlotBooked(int courtId, int timeSlotId, DateTime date)
        {
            var targetDate = DateOnly.FromDateTime(date);
            return Context.Bookings
                .Include(b => b.BookingSlots)
                .Any(b => b.CourtId == courtId &&
                          b.BookingSlots.Any(bs => bs.Vstid == timeSlotId && bs.BookDate == targetDate));
        }
        public List<Location> getAllLocation()
        {
            return Context.Locations.ToList();
        }
        public TimeSlot GetTimeSlotById(int id)
        {
            return Context.TimeSlots.FirstOrDefault(c => c.TimeSlotId == id);
        }

        public void AddBooking(Booking newBooking)
        {
            Context.Bookings.Add(newBooking);
            Context.SaveChanges();
        }
    }
}
