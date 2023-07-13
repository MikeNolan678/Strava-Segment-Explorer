# Strava Segment Explorer

Strava Segment Explorer is an application that allows users to connect their Strava account and explore their activities, segments and more. The application is designed with a clean and efficient architecture, using best practices such as Dependency Injection, Entity Framework, Identity for Authentication and Authorization, and MVC pattern for presentation logic. 

## Application Structure

The solution is divided into three projects:

1. **StravaSegmentExplorerUI:** This project is responsible for the User Interface, built with ASP.NET MVC. It contains all the Controllers, Views and related logic.

2. **StravaSegmentExplorerDataAccess:** This project is responsible for all data access logic. It uses Dapper as an ORM for interacting with the database.

3. **StravaSegmentExplorerDB:** This project includes all the SQL scripts related to the database structure.

## Technology Stack

- ASP.NET Core MVC
- Entity Framework Core & Dapper ORM
- SQL Server
- Strava API (inc. OAuth2.0)

<H2> Work in Progress! ⚠️</H2>

<h4>UI Improvements</h4>

<li>Develop a more dynamic UI</li>
<li>Display segment leaderboards</li>
<li>General UX improvements</li>

<h4> Data Access  improvements</h4>
<li>Retrieve additional data from Strava API</li>
<li>Stop storing sensitive user information in Database</li>
<li>Develop Strava API token refresh functionality</li>
