using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Listings
{
    public class Cart
    {
        private List<Item> _items = new List<Item>();

        public IEnumerable<Item> Items => _items;

        public void AddItem(Item item)
        {
            var existingItem = _items.FirstOrDefault(i => i.ClothId == item.ClothId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                item.AddedDate = DateTime.Now;
                _items.Add(item);
            }
        }

        public void RemoveItem(int watchId)
        {
            _items.RemoveAll(i => i.ClothId == watchId);
        }

        public void UpdateQuantity(int watchId, int quantity)
        {
            var item = _items.FirstOrDefault(i => i.ClothId == watchId);
            if (item != null)
            {
                item.Quantity = quantity;
            }
        }

        public decimal GetTotal()
        {
            return _items.Sum(i => i.Price * i.Quantity);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
