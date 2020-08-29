# CentralLogging
This project demonstrates a centralized logging context which is used to track that relevent log messages for the lifetime of a request. The context is avilable across threads and can be editted at any point in the code.

## Get Started
Please follow the below steps to setup the solution on your machine.  

### Prerequisites
* Visual Studio 
* Chrome
* Postman

### Installing
* Pull the code from github into your ide
* Restore Nuget Packages
* Build Solution

### Running
* Start the API by using the CentralLogging propertities profile. This should display a welcome page
* Using Postman send a request to the **count** endpoint

```
POST https://localhost:5001/website/count
Body {"website":"https://www.google.co.za"}
```

## Todo
* Filter records based on log level

## Authors
* Sashen Govender
