using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.ProductEntities;
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
            using (var context = new DataContext())
            {
                List<Product> products = context.Products.ToList();

                if (products == null && !products.Any())
                    return new List<Product>();

                return products;
            }
        }

        public AdminData ValidateAdminLogin(string username, string password)
        {
            using (var db = new DataContext())
            {
                System.Diagnostics.Debug.WriteLine($"[DEBUG] DB Connection String: {db.Database.Connection.ConnectionString}");
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Input username: '{username}'");
                var admin = db.AdminData.FirstOrDefault(a => a.Username == username);
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Retrieved admin: {(admin == null ? "null" : "not null")}");

                var allAdmins = db.AdminData.ToList();
                foreach (var a in allAdmins)
                {
                    System.Diagnostics.Debug.WriteLine($"[DEBUG] Found in DB: '{a.Username}'");
                }


                if (admin != null)
                {
                    System.Diagnostics.Debug.WriteLine("[DEBUG] Calling VerifyHashedPassword...");
                    if (VerifyHashedPassword(admin.PasswordHash, password))
                    {
                        System.Diagnostics.Debug.WriteLine("[DEBUG] Password match!");
                        return admin;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[DEBUG] Password does not match.");
                    }
                }

                return null;
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