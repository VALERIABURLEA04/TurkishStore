using System.Collections.Generic;
using System.Web;

namespace eUSeControl.BusinessLogic.eUSeControl.BusinessLogic.Dtos.ProducteUSeControl.BusinessLogic.Dtos
{
    public class UpsertProductDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public string Category { get; set; }
        public int Stock { get; set; }
        public decimal? Weight { get; set; }
        public string Dimensions { get; set; }
        public string Materials { get; set; }
        public List<string> Sizes { get; set; } = new List<string>();
        public List<string> Colors { get; set; } = new List<string>();
        public List<HttpPostedFileBase> Images { get; set; } = new List<HttpPostedFileBase>();
    }
}