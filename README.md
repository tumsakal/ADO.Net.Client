# ADO.Net.Client
These libraries are a very thing wrapper around a given [ADO.NET](https://msdn.microsoft.com/en-us/library/e80y5yhx.aspx) driver.
These libraries are capable of working with any ADO.Net compatible driver, meaining they are database agnostic.  There are three layers to this library, discussed below in the nuget packages section. 

## Why Use This Library?


### Database Agnostic

These libraries are not tied to any given database providers driver.  This means that you can focus on 
your SQL and not on the code that you would write.  This also encourages you to write your SQL
in a more standard fashion following the ANSI standard.

### Async Support

This library implements true asynchronous I/O for database operations, without blocking
(or using `Task.Run` to run synchronous methods on a background thread). This greatly
improves the throughput of a web server that performs database operations.

#### IAsyncEnumerable Support

This library is capable of returning a typed object as an async enumerable.  The IAsyncEnumerable interface was introduced
in .NET Core 3.1 and C# 8.

### Licensing

These libraries are licensed under the [MIT License](LICENSE) and may be freely distributed with commercial software.

## NuGet Packages

[ADO.Net.Client.Core](https://www.nuget.org/packages/ADO.Net.Client.Core/)  
[ADO.Net.Client.Implementation](https://www.nuget.org/packages/ADO.Net.Client.Implementation/)  
[ADO.Net.Client](https://www.nuget.org/packages/ADO.Net.Client/)

## Contributing

If you'd like to contribute to ADO.Net.Client libraries, please read the [contributing guidelines](CONTRIBUTING).