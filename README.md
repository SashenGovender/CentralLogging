# CentralLogging
It would be awesome to have the full context of a message when an error occurs such as the input data, processing at different stages and output. However, it is difficult to created a central processing context because processing will likely occur at different parts of the code

This project demonstrates a centralized logging context which is used to track that relevant log messages for the lifetime of a request. The context is available across threads and can be edited at any point in the code.

The project relies heavily of the use of [AsyncLocal](https://docs.microsoft.com/en-us/dotnet/api/system.threading.asynclocal-1?view=netcore-3.1) and ConcurrentQueue to solve the issues:
* Each thread accessing the shared list to update it in a sfae manner
* Each request need to have its own context list of messages

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
* Start the API by using the CentralLogging properties profile. This should display a welcome page (https://localhost:5001/html/home.html)
* Using Postman send a request to the **count** endpoint

```
POST https://localhost:5001/website/count
Body {"website":"https://www.google.co.za"}
```

## Todo
* Filter records based on log level

## Authors
* Sashen Govender
