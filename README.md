# ToDoApp

Simple example API with Entity Framework.

The API will automatically open a browser with Swagger so it can be tested. 

For the operations a MS SQL Server database is used. The connection string is set in the appsettings.json (for debug mode appsettings.Development.json).

Unit tests have been implemented in a seperate project (ToDoApp.Test) - the connection string for the test database is set in the appsettings.json file of this project. 