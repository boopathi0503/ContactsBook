using ContactsBook.BusinessLayer.Models;
using ContactsBook.BusinessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //using(var context = new StudentDBContext())
            //{
            //    var std = new Student()
            //    {
            //        ID = 1,
            //        Name = "Ram"
            //    };
            //    context.Students.Add(std);
            //    context.SaveChanges();
            //}
            IContactRepository contactRepository = new ContactRepository();
            contactRepository.AddContact(
                new Contact()
                {
                    Name = "Ram Krishnan",
                    EmailID = "abc@xyz.com",
                    PhoneNumber = "1234567890",
                    NickName = "Ram"
                }
                );
            contactRepository.AddContact(
                new Contact()
                {
                    Name = "Sivakumar",
                    EmailID = "xyz@xyz.com",
                    PhoneNumber = "9987654321",
                    NickName = "Siva"
                }
                );
            contactRepository.AddContact(
                new Contact()
                {
                    Name = "Preetha",
                    EmailID = "pree@xyz.com",
                    PhoneNumber = "1111111111",
                    NickName = "Preetha"
                }
                );
            List<Contact> contacts = contactRepository.GetAllContacts().ToList();
            Contact contact = contactRepository.GetContactByID(2);
            contact.PhoneNumber = "555555555";
            contactRepository.UpdateContact(contact);
            contact = contactRepository.GetContactByID(2);
            contactRepository.DeleteContact(2);
            Console.ReadLine();
        }
    }
}
