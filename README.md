# Client Management API

This is a simple client management API built with ASP.NET Core. It allows you to perform CRUD operations on clients and utilizes an in-memory cache for data storage.

## Project Structure

The project structure is as follows:

```bash
My_First_Api_.Net/
├── Controllers/
│   └── ClientController.cs
└── Models/
    └── Client.cs
```

- **Controllers**: Contains the `ClientController`, which handles client-related HTTP requests.
- **Models**: Contains the `Client` class, defining the structure of client data.

## Controller Description

The `ClientController` provides the following actions:

- **ListAllClients**: Retrieves a list of all clients.
- **CreateClient**: Creates a new client.
- **GetClientById**: Retrieves a client by its ID.
- **UpdateClient**: Updates an existing client.
- **DeleteClient**: Deletes a client by its ID.

## Client Class

The `Client` class defines the structure of client data. It includes the following properties:

- **Id**: Unique identifier of the client.
- **FirstName**: Client's first name.
- **LastName**: Client's last name.
- **Age**: Client's age.
- **Email**: Client's email address.
- **Phone**: Client's phone number.

## Requirements and Dependencies

This API requires ASP.NET Core and Microsoft.Extensions.Caching.Memory for caching management.

## Usage

To use this API, run the project and make HTTP requests to the defined routes in the `ClientController`.

## Contributions

Contributions are welcome! If you'd like to improve this API or report issues, feel free to open an issue or submit a pull request on GitHub.

## License

This project is licensed under the [MIT License](LICENSE).
