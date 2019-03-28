using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchApp.Data
{
    public class Person
    {

        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public string PersonPictureFileName { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Interest> Interests { get; set; } = new List<Interest>();


    }
}
