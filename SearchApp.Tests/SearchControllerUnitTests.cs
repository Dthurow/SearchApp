using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchApp.Controllers;
using SearchApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchApp.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchApp.Tests
{
    [TestClass]
    public class SearchControllerUnitTests
    {

        [TestMethod]
        public void GetMethod()
        {
            //setup && seed data
            var options = new DbContextOptionsBuilder<TestSearchAppContext>()
              .UseInMemoryDatabase(databaseName: "GetMethod")
              .Options;

            using (TestSearchAppContext context = new TestSearchAppContext(options))
            {
                SearchAppDbInitializer.Initialize(context);
                SearchController controller = new SearchController(context);

                //test
                Assert.IsNotNull(controller.Get());
            }
        }

        [TestMethod]
        public void GetSubsetMethod()
        {
            //setup && seed data
            var options = new DbContextOptionsBuilder<TestSearchAppContext>()
              .UseInMemoryDatabase(databaseName: "GetSubsetMethod")
              .Options;

            using (TestSearchAppContext context = new TestSearchAppContext(options))
            {
                SearchAppDbInitializer.Initialize(context);
                SearchController controller = new SearchController(context);

                //test
                Assert.IsNotNull(controller.GetSubset("a"));
            }
        }

        [TestMethod]
        public void GetSubsetMethod_BadInput1()
        {
            //setup && seed data
            var options = new DbContextOptionsBuilder<TestSearchAppContext>()
             .UseInMemoryDatabase(databaseName: "GetSubsetMethod_BadInput1")
             .Options;

            using (TestSearchAppContext context = new TestSearchAppContext(options))
            {
                SearchAppDbInitializer.Initialize(context);
                SearchController controller = new SearchController(context);
                List<SearchItem> searchResults = controller.ResultsFromSearch("*");
                 
                //test
                Assert.IsTrue(searchResults.Count() == 0);
                

            }
        }

        [TestMethod]
        public void GetSubsetMethod_BadInput2()
        {
            //setup && seed data
            var options = new DbContextOptionsBuilder<TestSearchAppContext>()
             .UseInMemoryDatabase(databaseName: "GetSubsetMethod_BadInput2")
             .Options;

            using (TestSearchAppContext context = new TestSearchAppContext(options))
            {
                SearchAppDbInitializer.Initialize(context);
                SearchController controller = new SearchController(context);
                List<SearchItem> searchResults = controller.ResultsFromSearch("<asdf;");

                //test
                Assert.IsTrue(searchResults.Count() == 0);


            }
        }


        [TestMethod]
        public void ResultsFromSearchMethod()
        {
            //setup && seed data
            var options = new DbContextOptionsBuilder<TestSearchAppContext>()
             .UseInMemoryDatabase(databaseName: "ResultsFromSearchMethod")
             .Options;

            using (TestSearchAppContext context = new TestSearchAppContext(options))
            {
                SearchAppDbInitializer.Initialize(context);
                SearchController controller = new SearchController(context);

                //test
                List<SearchItem> results = controller.ResultsFromSearch("a");

                //based on searchDB intializer, know what data should have come back
                Assert.AreEqual(results.Count(), 6);
                Assert.IsTrue(results.Any(x => x.FirstName == "Carson"));
                Assert.IsFalse(results.Any(x => x.FirstName == "Peggy"));

            }
        }


    }
}
