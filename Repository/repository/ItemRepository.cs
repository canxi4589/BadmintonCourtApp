using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repository
{

    /// <summary>
    ///  This is the repository for managing Course Items related.
    /// </summary>
    public class ItemRepository : BaseRepository<Item>
    {
        public ItemRepository(DBContext context) : base(context)
        {
        }
        public List<Item> GetAllItems()
        {
            return Context.Items.ToList();
        }
        
        public List<Item> GetItemsByType(int typeId)
        {
            return Context.Items.Where(c => c.ItemTypeId == typeId).ToList();
        }

        public List<Item> GetItemsByName(string prefix)
        {
            return Context.Items
                .Where(c => c.Name.Contains(prefix, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<ItemType> GetAllItemTypes() 
        {
            return Context.ItemTypes.ToList();
        }

        public ItemType? GetItemType(int Id)
        {
            return Context.ItemTypes.Find(Id);
        }

    }
}
