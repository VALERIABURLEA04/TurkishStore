using System;

namespace eUseControl.Domain.Entities.ListingEntities
{
    public class Listing
    {
        public int Listings_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
    }
}