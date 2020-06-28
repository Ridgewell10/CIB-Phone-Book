
using Infustructure.DTO;
using Infustructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IContactRepository : IRepositoryBase<PhoneBook>
    {

        Task<IEnumerable<Books>> GetAllContactsAsync();
        Task<PhoneBook> GetContactByIdAsync(int contactId);
        void AddContact(PhoneBook contact);
        bool UpdateContact(PhoneBook contact);
        void DeleteContact(PhoneBook contact);

    }
}
