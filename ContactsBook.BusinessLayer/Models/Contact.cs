using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContactsBook.BusinessLayer.Models
{
    public class Contact
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string EmailID { get; set; }
        public string NickName { get; set; }
    }
}
