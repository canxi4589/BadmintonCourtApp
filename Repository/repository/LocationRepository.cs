using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repository
{
    public class LocationRepository : BaseRepository<Location>
    {
        public LocationRepository(DBContext context) : base(context)
        {
        }
        public List<Location> getAll()
        {
            return Context.Locations.ToList();
        }

        public List<Location> GetLocationsByID(int id)
        {
            return Context.Locations.Where(c => c.LocationId == id).ToList();
        }
    }
}
