using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SearchApp.Data
{
    
    public class Interest
    {
        
        public int Id { get; set; }
        public string InterestName { get; set; }
        public int PersonId { get; set; }

        
        public Person Person { get; set; }
    }
}
