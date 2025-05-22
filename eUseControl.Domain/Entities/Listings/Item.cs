using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Listings
{
    public class Item
    {
        public int UserId { get; set; }
        public int ClothId { get; set; }
        public string ClothName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
