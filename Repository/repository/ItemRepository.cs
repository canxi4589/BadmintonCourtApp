using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repository
{
    public class ItemRepository : BaseRepository<Item>
    {
        public ItemRepository(DBContext context) : base(context)
        {
        }
        public List<Item> GetItems(int typeId)
        {
            return Context.Items.Where(c => c.ItemTypeId==typeId).ToList();
        }
        public List<ItemType> GetItemsByType()
        {
            return Context.ItemTypes.ToList();
        }
    }
}
