# Strava Segment Explorer

In it's current form, this application allows users to connect to their Strava account and explore their activities, and how they compare to milestone distances. The application is designed to have a clean and efficient architecture, using common practices such as MVC and Razor pages for UI, Separation of Concerns, Dependency Injection, Entity Framework, Identity for Authentication and Authorization, SQL Server for storing user information. 

## Application Structure

The solution is divided into three projects:

1. **StravaSegmentExplorerUI:** This project is responsible for the User Interface, built with ASP.NET MVC and Razor Pages. It contains all the Controllers, Views and related logic.

    - The user can register and log in via the UI.
    - Via the 'Profile' page, the user can connect to Strava. Once connected, a summary of your Strava profile is diaplayed. 
    - The UI offers filtering capabilities (through the use of Partial Views and HTTP Get Requests), to display insights into the users Strava Activity.

2. **StravaSegmentExplorerDataAccess:** This project is responsible for all data access logic, including SQL Server and API calls to Strava's API. It ustilises Dapper as an ORM for interacting with the database.

3. **StravaSegmentExplorerDB:** This project includes all the SQL scripts related to the database structure.




## Technology Stack

- ASP.NET Core MVC
- Razor Pages
- Entity Framework Core
- Dapper ORM
- SQL Server
- Strava API (incl. OAuth2.0)


<H2> Work in Progress! ⚠️</H2>

<h4>UI Improvements</h4>

<li>Develop a more dynamic UI</li>
<li>Display segment leaderboards</li>
<li>General UX improvements</li>

<h4> Data Access  improvements</h4>
<li>Retrieve additional data from Strava API</li>
<li>Stop storing sensitive user information in Database</li>
<li>Develop Strava API token refresh functionality</li>
