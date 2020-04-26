#region Licenses
/*MIT License
Copyright(c) 2020
Robert Garrison

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/
#endregion
#region Using Statements
using System.Data;
using System.Data.Common;
using System.Reflection;
#endregion

namespace ADO.Net.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IDbParameterFormatter" />
    public class DbParameterFormatter : IDbParameterFormatter
    {
        #region Utility Methods                
        /// <summary>
        /// Maps the type of the database.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DbType MapDbType(PropertyInfo info)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Maps the parameter value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="info"></param>
        /// <returns></returns>
        public object MapParameterValue(object value, PropertyInfo info)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Maps an instance of a <see cref="DbParameter"/>
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="parameterValue">The value of the parameter</param>
        /// <param name="info">The information.</param>
        /// <param name="parameterNamePrefix">The parameter name prefix symbol</param>
        /// <returns></returns>
        public void MapDbParameter(DbParameter parameter, object parameterValue, PropertyInfo info, string parameterNamePrefix = "@")
        {
            parameter.ParameterName = string.Concat(parameterNamePrefix, info.Name);
            parameter.Value = MapParameterValue(parameterValue, info);
            parameter.DbType = MapDbType(info);
        }
        #endregion
    }
}
