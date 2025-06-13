using businessLogic.Dtos.ContactDtos;
using businessLogic.Interfaces;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.UserEntities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace businessLogic.BLStruct
{
    public class ContactBL : IContact
    {
        public async Task<IEnumerable<ContactDto>> GetAllAsync()
        {
            using (var context = new EUseControlDbContext())
            {
                return await context.Contacts
                    .Select(x => new ContactDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        Message = x.Message,
                        Subject = x.Subject,
                    })
                    .ToListAsync();
            }
        }

        public async Task<ContactDto> GetByIdAsync(int id)
        {
            using (var context = new EUseControlDbContext())
            {
                return await context.Contacts
                    .Select(x => new ContactDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        Message = x.Message,
                        Subject = x.Subject,
                    })
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<ContactDto> AddAsync(ContactDto model)
        {
            using (var context = new EUseControlDbContext())
            {
                Contact contact = new Contact
                {
                    Name = model.Name,
                    Email = model.Email,
                    Message = model.Message,
                    Subject = model.Subject
                };

                context.Contacts.Add(contact);
                await context.SaveChangesAsync();

                return model;
            }
        }

        /*
        public async Task<UContactData> UpdateAsync(int id, UContactData contactData)
        {
            using (var context = new EUseControlDbContext())
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
            using (var context = new EUseControlDbContext())
            {
                var contact = await context.Contacts.FindAsync(id);
                if (contact == null) return false;

                context.Contacts.Remove(contact);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}