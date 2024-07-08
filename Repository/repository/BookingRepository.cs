using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repository
{
    public class BookingRepository : BaseRepository<Booking>
    {
        public BookingRepository(DBContext context) : base(context)
        {

        }
        public IEnumerable<Booking> GetBookingsByUserId(int userId)
        {
            return Context.Bookings.Include(b => b.Court) // Include the related Court data
                .Where(b => b.UserId == userId)
                .ToList();
        }

        public IEnumerable<Booking> GetAllBookinInfoLiterally()
        {
            // This shit just so bad.
            return Context.Bookings
                .Include(x => x.Court)
                .Include(x => x.BookingSlots)
                .ThenInclude(y => y.Vst)
                .ThenInclude(z => z.TimeSlot)
                .Include(a => a.User)
                .ToList();
        }
    }
}
