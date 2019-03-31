using System;
using System.Collections.Generic;
using System.Text;
using SearchApp.Data;
using Microsoft.EntityFrameworkCore;

namespace SearchApp.Tests
{
    class TestSearchAppContext : DbContext, ISearchAppContext
    {
        public TestSearchAppContext(DbContextOptions<TestSearchAppContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }

        public TestSearchAppContext()
        {
            People = Set<Person>();
            Interests = Set<Interest>();
            Addresses = Set<Address>();
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
