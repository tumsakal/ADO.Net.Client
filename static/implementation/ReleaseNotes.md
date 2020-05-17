Version History
===============

### 1.0.7

* Renaming ReadObjectList and ReadObjectEnumerable on MultiResultReader
  * Renamed to ReadObjects
  * Renamed to ReadObjectsStream
* Renaming ReadObjectListAsync and ReadObjectEnumerableAsync on MultiResultReader
  * Renamed to ReadObjectsAsync
  * Renamed to ReadaObjectsStreamAsync
* MultiResultReader constructor now takes in an instance of IDataMapper
* SqlExecutor constructor now also takes in an instance of IDataMapper
* SqlExecutor GetScalarValue sync/async methods are now generic
* Updating to ADO.Net.Client.Core 1.0.8
* Adding .NET 4.5, 4.6.1, removing .NET 4.7.2 build

### 1.0.6

* Updating to ADO.Net.Client.Core 1.0.6 
* Removing IConnectionStringUtility methods from DbConnectionManager
* Adding new ConnectionStringBuilder class

### 1.0.5

* Updating SqlExecutor async methods to take in a bool that determines if a query should be prepared
  * .NET standard 2.1 and above.  
* Updating to ADO.Net.Client.Core 1.0.5

### 1.0.4

* Updating SqlExecutor sync methods to take in a bool that determines if a query should be prepared
* Updating to ADO.Net.Client.Core 1.0.4

### 1.0.3

### 1.0.2

* Initial Release