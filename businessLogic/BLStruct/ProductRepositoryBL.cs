using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces.Repositories;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Product;

namespace businessLogic.BLStruct
{
    public class ProductRepositoryBL : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepositoryBL()
        {
            _context = new DataContext();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

    }
}