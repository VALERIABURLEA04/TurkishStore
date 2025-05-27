using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.User;

namespace businessLogic.Interfaces
{
    public interface IContact
    {
        Task<IEnumerable<UContactData>> GetAllAsync();
        Task<UContactData> GetByIdAsync(int id);
        Task<UContactData> AddAsync(UContactData contactData);
        /* Task<UContactData> UpdateAsync(int id, UContactData contactData); */
        Task<bool> DeleteAsync(int id);
    }
}
