# Strava Segment Explorer

In it's current form, this application allows users to connect to their Strava account and explore their activities, and how they compare to milestone distances. The application is designed to have a clean and efficient architecture, using common practices such as MVC and Razor pages for UI, Separation of Concerns, Dependency Injection, Entity Framework, Identity for Authentication and Authorization, SQL Server for storing user information. 

## Application Structure

The solution is divided into three projects:

1. **StravaSegmentExplorerUI:** This project is responsible for the User Interface, built with ASP.NET MVC and Razor Pages. It contains all the Controllers, Views and related logic.

    - The user can register and log in via the UI.
    - Via the 'Profile' page, the user can connect to Strava through HTTP Get and Post requests, using OAuth2.0 for authentication. Once connected, a summary of your Strava profile is diaplayed. 
    - The UI offers filtering capabilities (through the use of Partial Views and HTTP Get Requests), to display insights into the users Strava Activity.

    ![image](https://github.com/MikeNolan678/StravaSegmentExplorer/assets/50291390/77501ba8-e512-4ab3-b849-e7a7a486773c)


2. **StravaSegmentExplorerDataAccess:** This project is responsible for all data access and manipulation logic, including SQL Server and API calls to Strava's API. It ustilises Dapper as an ORM for interacting with the database.

    - API calls are made to the Strava API (https://developers.strava.com/).
    - Activity information is retrieved and processed, before being returned to the relevant Controller in ASP.Net Core MVC.
    - Various information is passed back and forth to SQL Server, for user identification + authorisation with Strava API.
    - Session state and cookies are implemented to enable transfer of information around the ASP.Net MVC structure, and to the API layer.

    
4. **StravaSegmentExplorerDB:** This project includes all the SQL scripts related to the database structure.


## Technology Stack

- ASP.NET Core MVC
- Razor Pages
- Entity Framework Core
- Dapper ORM
- SQL Server
- Strava API (incl. OAuth2.0)
- Session state & Cookies
- HTML, CSS, Javascript, Bootstrap


<H2> Work in Progress! ⚠️</H2>

<h4>UI Improvements</h4>

<li>Develop a more dynamic UI</li>
<li>Display segment leaderboards</li>
<li>General UX improvements</li>

<h4> Data Access  improvements</h4>
<li>Retrieve additional data from Strava API</li>
<li>Stop storing sensitive user information in Database</li>
<li>Develop Strava API token refresh functionality</li>
