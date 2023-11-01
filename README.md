
# WHO-NAPHS

1. Open the NAPHS back-end app by uing code 
```bash
 cd NAPHS-WHO
 ```
And to open the solution in VS Code we need to add writhe the below command

```bash
code .
``` 
Now we can see the project structure.

2. In the project structure we will have following projects
    
    1. **WHO.NAPHS.API**: This project consists of all the end points for this application

    2. **WHO.NAPHS.BusinessLogic**: This project consists the service interfaces, Service Helper and view models.

    3. **WHO.NAPHS.Core**: This project consists the common methods that can be used in other projects like Response Middleware, Enums, API Response wrapper.

    4. **WHO.NAPHS.Database**: All the database schemas like tables, Stored procedures and SQL functions will be stored here.

    5. **WHO.NAPHS.Infrastructure**: This project consists the Database models and service impementaions and Database context for this application.


**Commands to Run Project**
1. First restore the nuget packages by using command
``` bash
dotnet restore
``` 

2. Build the whole application
```bash
dotnet Build
```

3. Run the application using this command
```bash
dotnet run --project WHO.NAPHS.API/WHO.NAPHS.API.csproj --environment local
```

4. To give one project ref to another project you need to move to the project directory when we want to add the reference and then apply the below command
```bash
dotnet add reference ../WHO.NAPHS.Core/WHO.NAPHS.Core.csproj
```

5. To publish the database go to the Database project from the side navigation from the Visual Code and right click on 
```bash
WHO.NAPHS.Database
```
and publish that database. if user is not created then create user firts and then add connection string 
```bash
(localdb)\\mssqllocaldb
```
and then select the database from the list. If database is not there then create new database with name 
```bash
NAPHS
```
and enter in the soltion window. The database will be published.

**To Run code in dubugger mode**

To run the code in dubugger mode we need to click on "Run and Debug" option from the side navigation of the Visual Code and then add "C#" as the project type and select the "WHO.NAPHS.API" and click the enter.

**Install ssl certificate**

Now, To run project on https
   Go to this link and install ssl certificate.
    [Install SSL certificate for angular project](https://medium.com/@rubenvermeulen/running-angular-cli-over-https-with-a-trusted-certificate-4a0d5f92747a#:~:text=Step%201%3A%20Generate%20a%20certificate)








