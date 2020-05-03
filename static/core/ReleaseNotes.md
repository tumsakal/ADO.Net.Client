Version History
===============

### 1.0.7

* Updating to ADO.Net.Client.Annotations 1.1.1
* Adding .NET 4.5 and .NET 4.6.1 builds

### 1.0.6

* Setting default parameter direction to input in DbObjectFactory.GetDbParameter
* New dependency ADO.Net.Client.Annotations
* IConnectionManager no longer chaining IConnectionStringUtility 
  
### 1.0.5

* Updating ISqlExecutor async methods to take in a bool that determines 
  if a query should be prepared
  * For .net standard 2.1 and above.