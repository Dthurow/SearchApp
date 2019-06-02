using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SearchApp.Data
{
    public class SearchAppContext : DbContext, ISearchAppContext
    {

        public SearchAppContext(DbContextOptions<SearchAppContext> options) : base(options)
        {
        }
        

        public DbSet<Person> People { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }

    public interface ISearchAppContext
    {


        DbSet<Person> People { get; set; }
        DbSet<Interest> Interests { get; set; }
        DbSet<Address> Addresses { get; set; }

        int SaveChanges();
    }
}
