# Pre-reqs:
* Visual studio installed (Created on Visual Studio Enterprise 2017 edition)
* Running a (localdb)\MSSQLLocalDB instance on your machine
* Have .NET Core 2.1
* Have Nodejs installed

# Steps to run:
1. Open the command line inside the SearchApp\SearchApp folder
1. run "dotnet ef database update" to create tables in the master db on (localdb)\MSSQLLocalDB (tables will be seeded on initial run of SearchApp\SearchApp API)
1. Make sure visual studio has "Multiple Startup Projects" selected for the Startup Project, and click "Start"
    1. This will start IIS Express for both the SearchApp API and the SearchFrontEnd
    1. The SearchFrontEnd will automatically open up the search page in your default browser

### Things to do:
* SearchAppContext.cs has the path to the SQL server. move to config file
* include tests in repo
* add async where needed in API
