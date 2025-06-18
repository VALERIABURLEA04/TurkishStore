using eUseControl.Domain.Entities.UserEntities;
using eUSeControl.BusinessLogic.Dtos.ContactDtos;
using eUSeControl.DataAccess.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace eUSeControl.BusinessLogic.Services
{
    public class ContactService : IContactService
    {
        private static ContactService _instance;
        private static readonly object _lock = new object();

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

        public static ContactService GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new ContactService();
                }
            }

            return _instance;
        }
    }
}