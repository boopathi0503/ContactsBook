using ContactsBook.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsBook.BusinessLayer.Repository
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAllContacts();
        Contact GetContactByID(int contactId);
        void AddContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(int? contactId);
    }
}
