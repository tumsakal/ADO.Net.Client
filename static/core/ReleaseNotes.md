Version History
===============

### 1.1.4

* Adding AddParameterRange overload to [IDbParameterUtility](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/IDbParameterUtility.cs) that takes in a parameter array of objects
* Adding Append overload to [IQueryBuilder](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/IQueryBuilder.cs) that takes in a SQL string in a parameter array of objects
* Adding append overload to [IQueryBuilder](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/IQueryBuilder.cs) that takes in a parameter name and value

### 1.1.3

* IMultiResultReaderAsync now implements IAsyncDisposable for .NET Standard 2.1 and above
### 1.1.2

* GetScalarValue sync/async now returns T instead of object on following objects
  * ISqlExector
  * IDbProvider
  * DbProvider
  * IAsynchronousClient
  * ISynchronousClient

### 1.1.1

* Adding GetMultiResultReader async/sync on ISqlExectourSync/Async

### 1.1.0

* ISqlExecutorAsync .NET standard 2.1+ build executetransactednonquery/nonquery now take in a cancellation token
* DbObjectFactory now has constructors that don't require an instance of IDbParameterFormatter


### 1.0.9

* IDbParamaeterUtility now has Contains function that takes in an instance of DbParameter
* Updating to ADO.Net.Client.Annotations 1.1.2
* IDbObjectFactory new GetDbParameters function that takes in a param array of object
  and returns an IEnumerable of DbParameter
* IAsynchronousClient and ISynchronousClient GetScalarValue is now generic
* Utilities class adding new extension method to check if type is IEnumerable
* Utilites class making IsNullableGenericType an extension method
* Utilities class adding new extensions method on PropertyInfo[] to get a property by name with string value of the name

### 1.0.8

* Renaming ReadObjectList and ReadObjectEnumerable on IMultiResultReaderSync
  * Renamed to ReadObjects
  * Renamed to ReadObjectsStream
* Renaming ReadObjectListAsync and ReadObjectEnumerableAsync on IMultiResultReaderAsync
  * Renamed to ReadObjectsAsync
  * Renamed to ReadaObjectsStreamAsync
* Renaming GetDataObjectList and GetDataObjectEnumerable on ISqlExecutorSync
  * Renamed to GetDataObects
  * Renamed to GetDataObjectsStream
* Renaming GetDataObjectListAsync and GetDataObjectEnumerableAsync on ISqlExecutorAsync
  * Renamed to GetDataObjectsAsync
  * Renamed to GetDataObjectsStreamAsync
* Removing all async/sync functions from [Utilities](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/Utilities.cs), those functions are now in [IDataMapper](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/IDataMapper.cs)
* [ISqlExecutor](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/ISqlExecutor.cs) GetScalarValue sync and async functions are now generic
* **New** Class [DataMapper](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/DataMapper.cs)
* **New** Interface [IDataMapper](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/IDataMapper.cs)
* All instances of [DbObjectFactory](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/DbObjectFactory.cs) constructors now take in an instance of [IDbParamaterFormatter](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/IDbParameterFormatter.cs)
* Removing dependency on [Microsoft.Extensions.DependencyModel](https://www.nuget.org/packages/Microsoft.Extensions.DependencyModel)
* Correctiing all references to #if NET472
* Updating to [Microsoft.Bcl.AsyncInterfaces](https://www.nuget.org/packages/Microsoft.Bcl.AsyncInterfaces/) 1.1.1
* Making DbObjectFactory.GetProviderFactory a static function
  * Removing API from [IDbObjectFactory](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/IDbObjectFactory.cs)

### 1.0.7

* Updating to ADO.Net.Client.Annotations 1.1.1
* Adding **new** [DbParameterFormatter](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/DbParameterFormatter.cs) class
* Updating [IDbParameterFormatter](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/IDbParameterFormatter.cs) API
  * MapDbTye now takes in an instance of PropertyInfo
  * MapDbValue now takes in an instance PropertyInfo and object
  * **New** MapParameter
    * Takes in an IDbDataParameter for the purpose of mapping the object to data type, value, and other settings
* Adding .NET 4.5 and .NET 4.6.1 builds

### 1.0.6

* Setting default parameter direction to input in DbObjectFactory.GetDbParameter
* New dependency [ADO.Net.Client.Annotations](https://www.nuget.org/packages/ADO.Net.Client.Annotations/)
* IConnectionManager no longer chaining IConnectionStringUtility 
  
### 1.0.5

* Updating [ISqlExecutor](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/ISqlExecutorAsync.cs) async methods to take in a bool that determines 
  if a query should be prepared
  * For .net standard 2.1+ and above

### 1.0.4

* Updating [ISqlExecutor](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/ISqlExecutorSync.cs) sync methods to take in a bool that determines if a query should be prepared

### 1.0.3
* Adding CanCreateDataAdapter and CanCreateCommandBuilder
  properties to [IDbObjectFactory](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/IDbObjectFactory.cs) 
    * For .NET Standard 2.1+ and above

### 1.0.0

* Initial Release