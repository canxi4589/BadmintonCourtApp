using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repository
{
    public class ItemTypeRepository : BaseRepository<ItemType>
    {
        
        public ItemTypeRepository(DBContext context) : base(context)
        {
        }
        public List<ItemType> GetAllItemTypes()
        {
            if (Context.ItemTypes == null)
            {
                throw new InvalidOperationException("Context is not initialized.");
            }
            return Context.ItemTypes.ToList();
        }
        public List<ItemType> Get(int id)
        {
               return Context.ItemTypes.Where(i=>i.ItemTypeId==id).ToList();
        }
    }
}
