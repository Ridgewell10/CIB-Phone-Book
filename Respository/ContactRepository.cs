using Contracts;
using Infustructure.DTO;
using Infustructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Respository
{
    public class ContactRepository : RepositoryBase<PhoneBook>, IContactRepository
    {
        private readonly CIBDbContext _db;
        public ContactRepository(CIBDbContext repositoryContext)
            : base(repositoryContext)
        {
            _db = repositoryContext;
        }


        public void AddContact(PhoneBook contact)
        {
            contact.Id = contact.Id;
            Create(contact);
            Save();
        }

        public void DeleteContact(PhoneBook contact)
        {
            Delete(contact);
            Save();
        }

        public async Task<IEnumerable<Books>> GetAllContactsAsync()
        {
            
            List<Books> books = new List<Books>();
            try
            {
                var listbooks = await _db.PhoneBook.ToListAsync();
                foreach (var item in listbooks)
                {
                    var book = new Books()
                    {
                        ID = item.Id,
                        BookName = item.Name,
                    };
                    var entry = _db.Entry.Where(o => o.PhoneBookId == item.Id).ToList();
                    
                    foreach (var dbEntry in entry)
                    {
                        book.EntryName = dbEntry.Name;
                        book.EntryNumber = dbEntry.PhoneNumber;
                        books.Add(book);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return books;

        }

        public async Task<PhoneBook> GetContactByIdAsync(int contactId)
        {
            return await Task.Run(() => FindByCondition(contact => contact.Id.Equals(contactId))
            .DefaultIfEmpty(new PhoneBook())
            .FirstOrDefault());
        }

        public bool UpdateContact(PhoneBook contact)
        {
            var lastId = FindAll().Max(a => a.Id);
            Update(contact);
            Save();

            if (FindAll().Select(a => a.Id).FirstOrDefault() == lastId + 1)
                return true;
            else
                return false;
        }
    }
}
