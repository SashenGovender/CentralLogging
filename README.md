# CentralLogging
It would be awesome to have the full context of a message when an error occurs such as the input data, processing at different stages and output. However, it is difficult to created a central processing context because processing will likely occur at different parts of the code

This project demonstrates a centralized logging context which is used to track that relevant log messages for the lifetime of a request. The context is available across threads and can be edited at any point in the code.

The project relies heavily of the use of [AsyncLocal](https://docs.microsoft.com/en-us/dotnet/api/system.threading.asynclocal-1?view=netcore-3.1) to solve the issues:
* Each request need to have its own context list of messages

# Projects
* CentralLog - the library which creates and manages a list of all the logs
* GeneralOperationsAPI
* GeneralOperationsAPI.Tests
* LoggingDemo - Two hosted services: One demonstrating the logging issue and the other solving it

# Get Started
Please follow the below steps to setup the solution on your machine.  

## Prerequisites
* Visual Studio 
* Chrome
* Postman

## Installation
* Pull the code from github into your ide
* Restore Nuget Packages
* Build Solution

## Running
* Start the API by using the CentralLogging properties profile. This should display a welcome page (https://localhost:5001/html/home.html)
* Using Postman send a request to the **count** endpoint

```
POST https://localhost:5001/website/count
Body {"website":"https://www.google.co.za"}
```

## Usage
* Add the **CentralLog** dependency to your project
* Add LogContext to your IOC via **AddLogContextProivder** passing through the Configuration
```
private static void ConfigureServices(HostBuilderContext builder, IServiceCollection serviceCollection)
{
  serviceCollection.AddLogContextProivder(builder.Configuration);
}
```
* Add the ILogContext interface to your class and inject via the constructor
```
public class CalculationServiceLogContext : IHostedService
{
  private readonly ILogger<CalculationServiceLogContext> _logger;
  private readonly ILogContext _logContext;

  public CalculationServiceLogContext(ILogger<CalculationServiceLogContext> logger, ILogContext logContext)
  {
    _logger = logger;
    _logContext = logContext;
  }
}
```
* Now you can add a LogContext
```
LogContext.Context.AddLog(LogLevel.Information, "A valuable log message");
```

# Todo
* Improve log context formatting

# Authors
* Sashen Govender
