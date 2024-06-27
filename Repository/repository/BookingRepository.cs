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
        private readonly DBContext _context;
        public BookingRepository(DBContext context) : base(context)
        {
            _context = context;

        }
        public IEnumerable<Booking> GetBookingsByUserId(int userId)
        {
            return _context.Bookings
        .Include(b => b.Court) // Include the related Court data
        .Include(b => b.BookingItems) // Include the related BookingItems
        .ThenInclude(bi => bi.Item) // Include the related Item for each BookingItem
        .Include(b => b.BookingSlots) // Include the related BookingSlots
        .ThenInclude(bs => bs.Vst)
        .ThenInclude(bts => bts.TimeSlot)
        .Where(b => b.UserId == userId)
        .ToList();
        }
    }
}
