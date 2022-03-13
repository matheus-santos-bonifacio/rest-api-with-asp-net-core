# rest-api-with-asp-net-core
Restful API with Asp.Net Core, Entity Framework with Postgresql, and Swagger to documentation, beside authentication with Jwt (Json Web Token) and design patterns like Repository pattern, IOC (Inversion of control), and DI (Dependency injection).

## Technologies
- Dotnet 6.0
- Postgresql 14.2
- Docker 20.10
- Docker compose
- Jwt
- Swagger
- Entity Framework
- Asp.Net Core 6.0
- Git

## Design patterns
- DI `Dependency injection`
- IoC `Inversion of Control`
- Repository Pattern

## Start
Clone the repository and go to project root and entry in web project
```
$ git clone https://github.com/matheus-santos-bonifacio/rest-api-with-asp-net-core
$ cd rest-api-with-asp-net-core/web
```
If you don't have the database, initialize the docker container
```
$ docker-compose --profile database up -d
```
But if you already have a postgres database in your machine you can use the below configuration
- Database: net_backend_architecture
- Username: aspnet
- Password: aspnet

But in both cases you must create database with the migration
- With docker cli (you must have entity framework tool, if you don't have you can install with `dotnet tool install --global dotnet-ef`)
```
$ docker ef database update
```
- With visual studio
```
Update-Database
```
After all that you can initialize the project and see documentation in `https://localhost:7045`
- With docker cli
```
$ dotnet run
```
- With visual studio
```
Debug > Start Without Debugging
```
