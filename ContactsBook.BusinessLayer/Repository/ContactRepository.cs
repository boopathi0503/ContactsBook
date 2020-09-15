using ContactsBook.BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactsBook.BusinessLayer.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDBContext _context;
        private DbSet<Contact> contacts;

        public ContactRepository()
        {
            _context = new ContactDBContext();
            contacts = _context.Set<Contact>();
        }
        public void AddContact(Contact contact)
        {
            if (contact != null)
            {
                contacts.Add(contact);
                _context.SaveChanges();
            }
            else
                throw new ArgumentNullException(nameof(contact));
        }

        public void DeleteContact(int? contactId)
        {
            if (contactId != null)
            {
                Contact contact = contacts.SingleOrDefault(s => s.ID == contactId);
                contacts.Remove(contact);
                _context.SaveChanges();
            }
            else
                throw new ArgumentNullException("contactId");
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            //return _context.Contacts.ToList();
            return contacts.AsEnumerable();
        }

        public Contact GetContactByID(int contactId)
        {
            //return _context.Contacts.Find(contactId);
            return contacts.SingleOrDefault(s => s.ID == contactId);
        }

        public void UpdateContact(Contact contact)
        {
            if (contact != null)
            {
                contacts.Attach(contact);
                _context.Entry(contact).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
                throw new ArgumentNullException(nameof(contact));

        }
    }
}
