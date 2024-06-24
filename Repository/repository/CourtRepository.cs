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
        private readonly DbContext _context;
        public CourtRepository(DbContext context) : base(context)
        {
            _context = context;
        }

    }
}
