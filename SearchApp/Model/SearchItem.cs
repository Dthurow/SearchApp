using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchApp.Model
{
    public class SearchItem
    {

        public string FirstName;
        public string LastName;
        public int Age;
        public string ProfilePicture;
        public List<string> Interests;
        public List<SearchItemAddress> Addresses;
    }

    public class SearchItemAddress
    {
        public string StreetAddress { get; set; }
        public string SecondaryAddressLine { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
