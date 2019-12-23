# Course Service

## Instructions

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

1. To run integration tests container just run from the mail folder:
```
docker-compose -f docker-compose-integration.yml up --build --abort-on-container-exit
```
2. Watch the output logs. Expected result is: 
```
integration_1  | Test Run Successful.
integration_1  | Total tests: 14
integration_1  |      Passed: 14
integration_1  |  Total time: 6.8945 Seconds
```
