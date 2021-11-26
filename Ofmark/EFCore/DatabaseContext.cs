using Microsoft.EntityFrameworkCore;
using Ofmark.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ofmark.EFCore
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("User ID=postgres;Password=q1w2e3r4t5++;Server=localhost;Port=5432;Database=TestDB;Integrated Security=true;Pooling=true");
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<EmailAddresses> EmailAddresses { get; set; }
        public DbSet<PhoneNumbers> PhoneNumbers { get; set; }
        public DbSet<PublicAddresses> PublicAddresses { get; set; }
    }
}
