# Pre-reqs:
* Visual studio installed (Created on Visual Studio Enterprise 2017 edition)
* Running a (localdb)\MSSQLLocalDB instance on your machine
* Have .NET Core 2.1
* Have Nodejs installed

# Steps to run:
This will start IIS Express for both the SearchApp API and the SearchFrontEnd
The SearchFrontEnd will automatically open up the search page in your default browser

1. Open SearchApp solution in visual studio and build with ctrl+b
    1. this will get the NuGet packages, including the MicrosoftEntityFrameworkCore, which you will use to auto-generate the SQL database from the classes in the Data folder
1. Open the command line inside the SearchApp\SearchApp folder
1. run "dotnet ef database update" to create tables in the master db on (localdb)\MSSQLLocalDB (tables will be seeded on initial run of SearchApp\SearchApp API)
1. Make sure visual studio has "Multiple Startup Projects" selected for the Startup Project, and click "Start"
    1. If this option is not available in the dropdown, right click the solution name -> properties-> Start up Project and select "multiple startup projects" Start the SearchApp and SearchFrontEnd. Recommended to start SEarchFrontEnd without debugging

### Troubleshooting tips
1. search seems to hang
    1. there's a built in delay time of 2 seconds, but if it's longer than that, click F12 to check the console log on the browser. If it comes up with a CORS error, than most likely the API is returning back 500 errors (.NET Core's CORS HTTP header gets stripped off of 500 errors). Try calling the API directly

## Future enhancements
1. move sql connection string into the config file and pull from there instead of hardcoding it
1. include the ability for end users to add new person records to search
1. add ability to use wildcard in search
1. change ajax call in search.vue to use passed in URL from config file instead of hardcoding it

