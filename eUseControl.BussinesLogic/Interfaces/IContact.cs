using System.Collections.Generic;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.Contact;
using eUseControl.Domain.Entities.Contact.eUseControl.Domain.Entities.Contact;

namespace businessLogic.Interfaces
{
    public interface IContact
    {
        Task<IEnumerable<ContactMessageEntity>> GetAllAsync();
        Task<ContactMessageEntity> GetByIdAsync(int id);
        Task<bool> AddAsync(ContactMessageEntity contactData);

        // Task<ContactMessageEntity> UpdateAsync(int id, ContactMessageEntity contactData);
        Task<bool> DeleteAsync(int id);
    }
}
