
# BookstoreXmlApi

This is a simple ASP.NET Core Web API project that manages a bookstore using an XML file as its data source.

## Features

- Read all books from an XML file
- Get a book by ISBN
- Add new books
- Update existing books
- Delete books
- Generate an HTML report of all books

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later
- Visual Studio 2022 or VS Code (recommended)

### Setup

1. Clone the repository or download the source code.

2. Ensure the XML data file is located at `Data/bookstore.xml`. This file contains the books data in XML format.

3. Build the project:

```bash
dotnet build
```

4. Run the project:

```bash
dotnet run
```

The API will start, and by default will listen on `https://localhost:5256` and `http://localhost:<port>`.

### Running Tests

The project includes unit tests for the `BookService`. To run tests, use:

```bash
dotnet test
```

Make sure the test XML file `Data/test-bookstore.xml` is present. The tests will copy the main XML file for isolation.

## Project Structure

- **Models**: Contains the `Book` model class representing book data.
- **Services**: Contains `BookService` which handles all operations on the bookstore XML.
- **Configuration**: Contains settings classes like `XmlSettings` for the XML file path.
- **Tests**: Contains unit tests for the service.

