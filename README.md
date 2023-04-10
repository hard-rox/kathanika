Kathanika: Library Management System
=========================

This project is a library management system that allows librarians to manage their book collections and patrons to borrow and return books.

Features
--------

-   Add, update, and delete books
-   Add, update, and delete patrons
-   Borrow and return books
-   Search books by title, author, and category
-   Generate reports on book availability and borrower history

Getting Started
---------------

See the [Getting Started](https://github.com/hard-rox/kathanika/docs/GETTING_STARTED.md) guide for instructions on how to build and run the project.

Architecture
------------

This project follows a clean architecture pattern with domain-driven design (DDD) principles. The code is organized into three main layers:

-   Domain layer: Contains the core business logic and domain entities, value objects, and repositories.
-   Application layer: Contains the application services, which coordinate the interactions between the domain layer and the infrastructure layer.
-   Infrastructure layer: Contains the implementation details of the application, including persistence, GraphQL, and workers.

Technologies
------------

-   .NET 7
-   MongoDB
-   GraphQL
-   xUnit

Contributing
------------

Contributions are welcome! See the [Contributing Guidelines](https://github.com/hard-rox/kathanika/docs/CONTRIBUTING.md) for details.

License
-------

This project is licensed under the [MIT License](https://github.com/hard-rox/kathanika/LICENSE).