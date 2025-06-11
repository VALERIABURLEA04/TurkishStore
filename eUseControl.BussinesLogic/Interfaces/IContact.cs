using businessLogic.Dtos.ContactDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace businessLogic.Interfaces
{
    public interface IContact
    {
        Task<IEnumerable<ContactDto>> GetAllAsync();

        Task<ContactDto> GetByIdAsync(int id);

        Task<ContactDto> AddAsync(ContactDto contactData);

        /* Task<UContactData> UpdateAsync(int id, UContactData contactData); */

        Task<bool> DeleteAsync(int id);
    }
}