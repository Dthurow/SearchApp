

Pre-reqs:
Visual studio installed (Created on Visual Studio Enterprise 2017 edition)
Running a (localdb)\MSSQLLocalDB instance on your machine
Have .NET Core 2.1
Have Nodejs installed

Steps to run:
Open the command line inside the SearchApp\SearchApp folder
run "dotnet ef database update" to create tables in the master db on (localdb)\MSSQLLocalDB (tables will be seeded on initial run of SearchApp\SearchApp API)
Make sure visual studio has <Multiple Startup Projects> selected for the Startup Project, and click "Start"
    This will start IIS Express for both the SearchApp API and the SearchFrontEnd
    The SearchFrontEnd will automatically open up the search page in your default browser