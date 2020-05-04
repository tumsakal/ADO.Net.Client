Version History
===============

### 1.0.7

* Updating to ADO.Net.Client.Annotations 1.1.1
* Adding new [DbParameterFormatter](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/DbParameterFormatter.cs) class
* Updating IDbParameterFormatter API
  * MapDbTye now takes in an instance of PropertyInfo
  * MapDbValue now takes in an instance PropertyInfo and object
  * MapParameter **New**
    * Takes in an IDbDataParameter for the purpose of mapping the object to data type, value, and other settings
* Adding .NET 4.5 and .NET 4.6.1 builds

### 1.0.6

* Setting default parameter direction to input in DbObjectFactory.GetDbParameter
* New dependency ADO.Net.Client.Annotations
* IConnectionManager no longer chaining IConnectionStringUtility 
  
### 1.0.5

* Updating ISqlExecutor async methods to take in a bool that determines 
  if a query should be prepared
  * For .net standard 2.1+ and above

### 1.0.4

* Updating ISqlExecutor sync methods to take in a bool that determines if a query should be prepared

### 1.0.3
* Adding CanCreateDataAdapter and CanCreateCommandBuilder
  properties to IDbObjectFactory 
    * For .NET Standard 2.1+ and above