using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchApp.Data;
using SearchApp.Model;
using System.Collections.Generic;
using System.Linq;

namespace SearchApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private readonly SearchAppContext _context;

        public SearchController(SearchAppContext context)
        {
            _context = context;
        }

        // GET: api/Search
        [HttpGet]
        public ActionResult<IEnumerable<SearchItem>> Get()
        {
            List<SearchItem> results = new List<SearchItem>();
            Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Person, ICollection<Interest>> people = _context.People.AsNoTracking().Include(p => p.Addresses).Include(p => p.Interests);

            people.ForEachAsync(p =>
            {
                List<SearchItemAddress> searchItemAddresses = getSearchAddresses(p.Addresses);
                List<string> interests = getInterests(p.Interests);
                results.Add(new SearchItem() { FirstName = p.FirstName, LastName = p.LastName, ProfilePicture = p.PersonPictureFileName, Addresses = searchItemAddresses, Age = p.Age, Interests = interests });
            }).Wait();

            return results;
        }



        [HttpGet]
        [Route("getsubset/{searchString}")]
        public ActionResult<IEnumerable<SearchItem>> GetSubset(string searchString)
        {
            List<SearchItem> results = new List<SearchItem>();

            var query = from person in _context.People.AsNoTracking().Include(p => p.Addresses).Include(p => p.Interests)
                        where person.FirstName.ToLower().Contains(searchString.ToLower()) || person.LastName.ToLower().Contains(searchString.ToLower())
                        select person;

            if (query != null && query.Count() > 0)
            {
                foreach(var p in query)
                {
                    results.Add(new SearchItem() { FirstName = p.FirstName, LastName = p.LastName, ProfilePicture = p.PersonPictureFileName, Addresses = getSearchAddresses(p.Addresses), Age = p.Age, Interests = getInterests(p.Interests) });
                    
                }
            }

            return results;
        }


        #region private helper methods
        private List<SearchItemAddress> getSearchAddresses(ICollection<Address> addresses)
        {
            return addresses.Select<Address, SearchItemAddress>(a => new SearchItemAddress() { StreetAddress = a.StreetAddress, SecondaryAddressLine = a.SecondaryAddressLine, City = a.City, Country = a.Country, PostalCode = a.PostalCode, StateOrProvince = a.StateOrProvince }).ToList();
        }
        private List<string> getInterests(ICollection<Interest> Interests)
        {
            return Interests.Select(i => i.InterestName).ToList();
        }
        #endregion

    }
}
