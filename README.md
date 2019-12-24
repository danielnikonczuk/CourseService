# Course Service

## Short description

It is a WebAPI service developed in .NET Core 3.1 cross-platform framework with language of choice - C#. For DB connectivity I used Entity Framework ORM with Code-first approach to automatically create/recreate table schemas based on entities in the code. When service is spun up and DB doesn't exist, it will create it and seed it with initial data (ref. OnModelCreating method in [this file](https://github.com/danielnikonczuk/CourseService/blob/master/CourseService/Models/CourseServiceDbContext.cs)). It is also very useful when entity changes. One can change entity in the code, and Entity Framework handles data migrations by preparing proper SQL migration scripts.

The rest is pretty standard code for API service built in .NET Core with latest revisioned approaches to API development.
It doesn't have everything you could have in your microservice e.g. authentication, authorization or versioning but I believe it's enough for the purpose of this task.

## How to run it

1. To run service along with the DB just run from main folder:
```
docker-compose build 
```
followed by 
```
docker-compose up -d
```
2. Now you can play with it and you should find it under http://localhost:8080/.
3. To get to know the service endpoints please go to http//localhost:8080/swagger/.

## Running Integration Tests

For purpose of this task I prepared a special docker-compose file to run integration tests independently of a IDE or OS.

It spins up two containers for the service (web and db), and the third one that imports the tests and run them using .NET SDK against the web container. Integration tests share DB context as if they would use this service in parallel with other users. However all tests are written to be logically independent so they could resemble real scenarios as close as they can. At the beginning and end of whole suite run we clear up all DB tables. 

I resigned from unit tests for the sake of integrations tests. They have full functionality coverage and in my opinion it would be cumbersome to write unit tests to cover same code again just with mock-ups.

1. To run integration tests container just run from the main folder:
```
docker-compose -f docker-compose-integration.yml up --build --abort-on-container-exit
```
2. Watch the output logs. Expected result should contain: 
```
integration_1  | Test Run Successful.
integration_1  | Total tests: 14
integration_1  |      Passed: 14
```
