using System.Linq;

namespace SearchApp.Data
{
    public static class SearchAppDbInitializer
    {
        public static void Initialize(ISearchAppContext context)
        {
            // Look for any students.
            if (context.People.Any())
            {
                return;   // DB has been seeded
            }

            addPeople(context);
            context.SaveChanges();

            addAddresses(context);
            context.SaveChanges();

            addInterests(context);
            context.SaveChanges();


        }


        public static void addPeople(ISearchAppContext context)
        {
            Person[] people = new Person[]
           {
                new Person{FirstName="Carson",LastName="Alexander",Age=10, PersonPictureFileName="alexander.jpg"},
                new Person{FirstName="Meredith",LastName="Alonso",Age=20, PersonPictureFileName="alonso.jpg"},
                new Person{FirstName="Arturo",LastName="Anand",Age=80, PersonPictureFileName="anand.jpg"},
                new Person{FirstName="Gytis",LastName="Barzdukas",Age=18, PersonPictureFileName="barzdukas.jpg"},
                new Person{FirstName="Yan",LastName="Li",Age=50, PersonPictureFileName="li.jpg"},
                new Person{FirstName="Peggy",LastName="Justice",Age=45, PersonPictureFileName="justice.jpg"},
                new Person{FirstName="Laura",LastName="Norman",Age=13, PersonPictureFileName="norman.jpg"},
                new Person{FirstName="Nino",LastName="Olivetto",Age=26, PersonPictureFileName="olivetto.jpg"}
           };
            foreach (Person p in people)
            {
                context.People.Add(p);
            }
        }

        public static void addAddresses(ISearchAppContext context)
        {
            Person[] people = context.People.ToArray();
            Address[] addresses = new Address[]
            {
                new Address{StreetAddress="123 something lane", City="somewhere",StateOrProvince="NV",Country="USA", PostalCode = "1234", PersonId = people[0].PersonId},
                new Address{StreetAddress="567 something lane",SecondaryAddressLine="PO BOX 10", City="somewhere",StateOrProvince="NV",PostalCode = "34534",Country="USA", PersonId = people[1].PersonId},
                new Address{StreetAddress="4356 something lane", City="boring",StateOrProvince="OR",Country="USA",PostalCode = "678", PersonId = people[2].PersonId},
                new Address{StreetAddress="7896 something lane", City="a city",StateOrProvince="WA",Country="USA",PostalCode = "1234", PersonId = people[3].PersonId},
                new Address{StreetAddress="123 something street", City="somewhere",StateOrProvince="NV",Country="USA", PostalCode = "1234",PersonId = people[4].PersonId},
                new Address{StreetAddress="567 something street",SecondaryAddressLine="PO BOX 10", City="somewhere",StateOrProvince="NV",PostalCode = "1234",Country="USA", PersonId = people[5].PersonId},
                new Address{StreetAddress="4356 something street", City="boring",StateOrProvince="OR",Country="USA", PostalCode = "1234",PersonId = people[6].PersonId},
                new Address{StreetAddress="7896 something street", City="a city",StateOrProvince="WA",Country="USA", PostalCode = "1234",PersonId = people[7].PersonId}
            };

            foreach (Address a in addresses)
            {
                context.Addresses.Add(a);
            }
        }

        public static void addInterests(ISearchAppContext context)
        {
            Person[] people = context.People.ToArray();
            Interest[] interests = new Interest[]
            {
               new Interest{InterestName="archery", PersonId = people[0].PersonId},
               new Interest{InterestName="reading", PersonId = people[0].PersonId},
               new Interest{InterestName="geocaching", PersonId = people[1].PersonId},
               new Interest{InterestName="climbing", PersonId = people[2].PersonId},
               new Interest{InterestName="kayaking", PersonId = people[3].PersonId},
               new Interest{InterestName="mountain biking", PersonId = people[4].PersonId},
               new Interest{InterestName="videogames", PersonId = people[5].PersonId},
               new Interest{InterestName="programming", PersonId = people[6].PersonId},
               new Interest{InterestName="martial arts", PersonId = people[7].PersonId},
               new Interest{InterestName="basketball", PersonId = people[7].PersonId},
            };

            foreach (Interest i in interests)
            {
                context.Interests.Add(i);
            }
        }

    }
}
