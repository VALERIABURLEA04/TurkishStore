using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.User;

namespace businessLogic.BLStruct
{
    public class ContactBL : IContact
    {
        public async Task<IEnumerable<UContactData>> GetAllAsync()
        {
            using (var context = new DataContext())
            {
                return await context.ContactData.ToListAsync();
            }
        }

        public async Task<UContactData> GetByIdAsync(int id)
        {
            using (var context = new DataContext())
            {
                return await context.ContactData.FindAsync(id);
            }
        }

        public async Task<UContactData> AddAsync(UContactData contactData)
        {
            using (var context = new DataContext())
            {
                context.ContactData.Add(contactData);
                await context.SaveChangesAsync();
                return contactData;
            }
        }

        /*
        public async Task<UContactData> UpdateAsync(int id, UContactData contactData)
        {
            using (var context = new DataContext())
            {
                var existing = await context.ContactData.FindAsync(id);
                if (existing == null) return null;

                existing.Name = contactData.Name;
                existing.Email = contactData.Email;
                existing.Subject = contactData.Subject;
                existing.Message = contactData.Message;

                await context.SaveChangesAsync();
                return existing;
            }
        }
        */

        public async Task<bool> DeleteAsync(int id)
        {
            using (var context = new DataContext())
            {
                var contact = await context.ContactData.FindAsync(id);
                if (contact == null) return false;

                context.ContactData.Remove(contact);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}

