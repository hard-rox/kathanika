Prerequisites
-------------

-   .NET Core 3.1 or later
-   MongoDB installed and running
-   Git

Getting the Source Code
-----------------------

1.  Open a terminal or command prompt.
2.  Clone the repository:

bashCopy code

`git clone https://github.com/your_username/your_project.git`

Building the Solution
---------------------

1.  Open a terminal or command prompt.
2.  Navigate to the root of the project directory.
3.  Run the following command to restore dependencies:

Copy code

`dotnet restore`

1.  Run the following command to build the solution:

Copy code

`dotnet build`

Running the Application
-----------------------

1.  Open a terminal or command prompt.
2.  Navigate to the `src/Web` directory.
3.  Run the following command to start the application:

arduinoCopy code

`dotnet run`

1.  Open a web browser and navigate to `http://localhost:5000`.

Running the Tests
-----------------

1.  Open a terminal or command prompt.
2.  Navigate to the root of the project directory.
3.  Run the following command to run all tests:

bashCopy code

`dotnet test`

Running the GraphQL Server
--------------------------

1.  Open a terminal or command prompt.
2.  Navigate to the `src/Infrastructure.GraphQL` directory.
3.  Run the following command to start the GraphQL server:

arduinoCopy code

`dotnet run`

1.  Open a web browser and navigate to `http://localhost:5001/graphql` to test the GraphQL API.

Running the Background Worker
-----------------------------

1.  Open a terminal or command prompt.
2.  Navigate to the `src/Infrastructure.Workers` directory.
3.  Run the following command to start the worker:

arduinoCopy code

`dotnet run`

That's it! You should now have a basic understanding of how to get started with the project.