using eUseControl.Domain.Entities.Listings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace businessLogic.Interfaces
{
    public interface IListings
    {
        Task<List<Item>> GetAllListingsAsync();               // Get all listings (cart items)

        Task<Item> GetListingByIdAsync(int id);               // Get a single listing by ID

        Task CreateListingAsync(CreateListingDto model); // Create a new listing from the input model

        Task UpdateListingAsync(int id, CreateListingDto model); // Update an existing listing by ID

        Task DeleteListingAsync(int id);                       // Delete listing by ID
    }
}