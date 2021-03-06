﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SearchApp.Data
{
    public class SearchAppContext : DbContext, ISearchAppContext
    {

        public DbSet<Person> People { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True");
        }
    }

    public interface ISearchAppContext
    {
        DbSet<Person> People { get; set; }
        DbSet<Interest> Interests { get; set; }
        DbSet<Address> Addresses { get; set; }

        int SaveChanges();
    }
}
