using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repository
{
    public class TimeSlotRepository : BaseRepository<TimeSlot>
    {
        public TimeSlotRepository(DBContext context) : base(context)
        {
        }
    }
}
