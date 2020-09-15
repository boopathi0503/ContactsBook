using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsBook.BusinessLayer.Models
{
    public class ContactDBContext : DbContext
    {
        //static LoggerFactory object
        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseInMemoryDatabase(databaseName: "ContactBooksDB")
                .UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging();
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
