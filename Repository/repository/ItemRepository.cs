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
        private readonly DBContext _context;
        public ItemRepository(DBContext context) : base(context)
        {
            _context = context;
        }
        public List<Item> GetItems(int typeId)
        {
            return _context.Items.Where(c => c.ItemTypeId==typeId).ToList();
        }
        public List<ItemType> GetItemsByType()
        {
            return _context.ItemTypes.ToList();
        }
    }
}
