#region Licenses
/*Copyright(C) 2011-2011 Topten Software(contact @toptensoftware.com)
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this product except in compliance with the License.
You may obtain a copy of the License at

<http://www.apache.org/licenses/LICENSE-2.0>

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.*/
#endregion
#region Using Statements
#endregion

namespace ADO.Net.Client.Annotations
{
    /// <summary>
    ///     Wrap strings in an instance of this class to force use of DbType.AnsiString
    /// </summary>
    public class AnsiString
    {
        #region Fields/Properties
        /// <summary>
        ///     The string value
        /// </summary>
        public string Value { get; }
        #endregion
        #region Constructors
        /// <summary>
        ///    Initializes a new instance of <see cref="AnsiString"/>
        /// </summary>
        /// <param name="str">The C# string to be converted to ANSI before being passed to the Database</param>
        public AnsiString(string str) => Value = str;
        #endregion
    }
}