using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLogic.Interfaces;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Contact.eUseControl.Domain.Entities.Contact;
using eUseControl.Domain.Entities.User;

namespace businessLogic.BLStruct
{
    public class ContactBL : IContact
    {
        public async Task<IEnumerable<ContactMessageEntity>> GetAllAsync()
        {
            using (var context = new DataContext())
            {
                return await context.ContactData
                    .Select(c => new ContactMessageEntity
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Email = c.Email,
                        Subject = c.Subject,
                        Message = c.Message
                    })
                    .ToListAsync();
            }
        }

        public async Task<ContactMessageEntity> GetByIdAsync(int id)
        {
            using (var context = new DataContext())
            {
                var c = await context.ContactData.FindAsync(id);
                if (c == null) return null;

                return new ContactMessageEntity
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Subject = c.Subject,
                    Message = c.Message
                };
            }
        }

        public async Task<bool> AddAsync(ContactMessageEntity entity)
        {
            using (var context = new DataContext())
            {
                var contactData = new UContactData
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Email = entity.Email,
                    Subject = entity.Subject,
                    Message = entity.Message
                };

                context.ContactData.Add(contactData);
                await context.SaveChangesAsync();
                return true;
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