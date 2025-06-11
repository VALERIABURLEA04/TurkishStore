using businessLogic.Interfaces;
using eUseControl.Domain.Entities.Listings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace businessLogic.BLStruct
{
    public class ListingsBL : IListings
    {
        // Simple in-memory store to simulate database
        private readonly List<Item> _items = new List<Item>();

        private int _nextId = 1;

        public async Task<List<Item>> GetAllListingsAsync()
        {
            // Simulate async work
            return await Task.FromResult(_items.ToList());
        }

        public async Task<Item> GetListingByIdAsync(int id)
        {
            var item = _items.FirstOrDefault(i => i.UserId == id);
            return await Task.FromResult(item);
        }

        public async Task CreateListingAsync(CreateListingDto model)
        {
            var newItem = new Item
            {
                UserId = _nextId++,
                ClothId = _nextId,  // or some logic here
                ClothName = model.Title,
                Price = model.Price,
                Quantity = 1,          // Default quantity
                ImageUrl = model.ImageUrl,
                AddedDate = System.DateTime.Now
            };
            _items.Add(newItem);

            await Task.CompletedTask;
        }

        public async Task UpdateListingAsync(int id, CreateListingDto model)
        {
            var item = _items.FirstOrDefault(i => i.UserId == id);
            if (item != null)
            {
                item.ClothName = model.Title;
                item.Price = model.Price;
                item.ImageUrl = model.ImageUrl;
                // Quantity update not included here, but you can add if needed
            }

            await Task.CompletedTask;
        }

        public async Task DeleteListingAsync(int id)
        {
            _items.RemoveAll(i => i.UserId == id);
            await Task.CompletedTask;
        }
    }
}