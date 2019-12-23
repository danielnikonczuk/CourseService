# Software Engineering Task - NORTHPASS

## INSTRUCTIONS

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

## RUNNING INTEGRATION TESTS

For purpose of this task I prepared a special docker-compose file to run integration tests independently of a IDE or OS.
It spins up two containers for the service (web and db), and the third one that imports the tests and run them using .NET SDK.

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
