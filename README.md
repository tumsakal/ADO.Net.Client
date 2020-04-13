# ADO.Net.Client
These libraries are a very thing wrapper around a given [ADO.NET](https://msdn.microsoft.com/en-us/library/e80y5yhx.aspx) driver.
These libraries are capable of working with any ADO.Net compatible driver, meaining they are database agnostic.  There are three layers to this library, discussed below in the nuget packages section. 

## Why Use This Library?

### Database Agnostic

These libraries are not tied to any given database providers driver.  This means that you can focus on 
your SQL and not on the code that you would write.  This also encourages you to write your SQL
in a more standard fashion following the ANSI standard.

### Lightweight

Only the core of ADO.NET is implemented, not EF or Designer types.

### Async Support

This library implements true asynchronous I/O for database operations, without blocking
(or using `Task.Run` to run synchronous methods on a background thread). This greatly
improves the throughput of a web server that performs database operations.

#### IAsyncEnumerable Support

This library is capable of returning a typed object as an async enumerable on read operations.  The IAsyncEnumerable interface was introduced
in .NET Core 3.1 and C# 8.

### Licensing

These libraries are licensed under the [MIT License](LICENSE) and may be freely distributed with commercial software.

## Goals

The goals of this project are:

1. **.NET Standard support:** It must run on the full .NET Framework and all platforms supported by .NET Core.
2. **Async:** All operations must be truly asynchronous whenever possible.
4. **Lightweight:** Only the core of ADO.NET is implemented, not EF or Designer types.
5. **Managed:** Managed code only, no native code.
6. **Independent:** These libraries are not bound to any specific ADO.Net driver.

## NuGet Packages

[ADO.Net.Client.Core](https://www.nuget.org/packages/ADO.Net.Client.Core/)  
[ADO.Net.Client.Implementation](https://www.nuget.org/packages/ADO.Net.Client.Implementation/)  
[ADO.Net.Client](https://www.nuget.org/packages/ADO.Net.Client/)

## Contributing

If you'd like to contribute to ADO.Net.Client libraries, please read the [contributing guidelines](CONTRIBUTING).