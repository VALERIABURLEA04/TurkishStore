using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace eUseControlBussinessLogic.Core
{
    public class AdminApi
    {
        public List<Product> GetProductsList()
        {
            using (var context = new EUseControlDbContext())
            {
                List<Product> products = context.Products.ToList();

                if (products == null && !products.Any())
                    return new List<Product>();

                return products;
            }
        }

        private bool VerifyHashedPassword(string storedHash, string inputPassword)
        {
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Stored Hash: '{storedHash}'");
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Input Password: '{inputPassword}'");

            using (SHA256 sha256 = SHA256.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(inputPassword);
                var hashBytes = sha256.ComputeHash(inputBytes);
                var inputHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                System.Diagnostics.Debug.WriteLine($"[DEBUG] Computed Hash: '{inputHash}'");

                var normalizedStoredHash = storedHash.Trim().ToLower().Replace("-", "");
                return normalizedStoredHash == inputHash;
            }
        }
    }
}