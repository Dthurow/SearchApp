using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchApp.Data;
using SearchApp.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

       /// <summary>
       /// gets all person records in database
       /// transforms them into search results
       /// not something to keep if the database actually gets large
       /// </summary>
       /// <returns></returns>
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


        /// <summary>
        /// Given a string, returns list of SearchItems
        /// that include all people who contain that search string in their
        /// last or first name
        /// Only support alphabetical searches, no unicode or wildcards
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns>list of searchItems, null if no found matches or invalid searchString</returns>
        [HttpGet]
        [Route("getsubset/{searchString}")]
        public JsonResult GetSubset(string searchString)
        {
            List<SearchItem> results = new List<SearchItem>();
           
            results = ResultsFromSearch(searchString);
            System.Threading.Thread.Sleep(2000);
            return new JsonResult(new { result = results });
        }

        public List<SearchItem> ResultsFromSearch(string searchString)
        {
            List<SearchItem> results = new List<SearchItem>();

        //validate input
        if (!string.IsNullOrEmpty(searchString) && Regex.IsMatch(searchString, @"[a-zA-Z]+"))
            {
                var query = from person in _context.People.AsNoTracking().Include(p => p.Addresses).Include(p => p.Interests)
                            where person.FirstName.ToLower().Contains(searchString.ToLower()) || person.LastName.ToLower().Contains(searchString.ToLower())
                            select person;

                if (query != null && query.Count() > 0)
                {
                    foreach (var p in query)
                    {
                        results.Add(new SearchItem() { FirstName = p.FirstName, LastName = p.LastName, ProfilePicture = p.PersonPictureFileName, Addresses = getSearchAddresses(p.Addresses), Age = p.Age, Interests = getInterests(p.Interests) });

                    }
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
