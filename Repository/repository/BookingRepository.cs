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
            return _context.Bookings.Include(b => b.Court) // Include the related Court data
                .Where(b => b.UserId == userId)
                .ToList();
        }
    }
}
