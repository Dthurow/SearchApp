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

        private readonly ISearchAppContext _context;

        public SearchController(ISearchAppContext context)
        {
            _context = context;
        }

        // GET: api/Search
        [HttpGet]
        public JsonResult Get()
        {
            List<SearchItem> results = new List<SearchItem>();
            var people = _context.People.AsNoTracking().Include(p => p.Addresses).Include(p => p.Interests);

            people.ForEachAsync(p =>
            {
                List<SearchItemAddress> searchItemAddresses = getSearchAddresses(p.Addresses);
                List<string> interests = getInterests(p.Interests);
                results.Add(new SearchItem() { FirstName = p.FirstName, LastName = p.LastName, ProfilePicture = p.PersonPictureFileName, Addresses = searchItemAddresses, Age = p.Age, Interests = interests });
            }).Wait();

            return new JsonResult(new { result = results });
        }



        [HttpGet]
        [Route("getsubset/{searchString}")]
        public JsonResult GetSubset(string searchString)
        {
            List<SearchItem> results = new List<SearchItem>();

            if (!string.IsNullOrEmpty(searchString))
            {
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
            }

            System.Threading.Thread.Sleep(2000);
            return new JsonResult(new { result = results });
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
