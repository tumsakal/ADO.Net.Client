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
using ADO.Net.Client.Core;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
#endregion

namespace ADO.Net.Client.Implementation.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class QueryBuilderTests 
    {
        #region Fields/Properties
        #endregion
        #region Constructors
        public QueryBuilderTests()
        {
           
        }
        #endregion
        #region Setup/Teardown
        [SetUp]
        public void Setup()
        {
        }
        #endregion
        #region Tests        
        [Test]
        public void RejectsDuplicateParameterName()
        {
            QueryBuilder builder = new QueryBuilder();
            MySqlParameter parameter = new MySqlParameter() { ParameterName = "@Param1" };

            builder.AddParameter(parameter);

            Assert.Throws<ArgumentException>(() => builder.AddParameter(new MySqlParameter() { ParameterName = "@Param1" }));
        }
        /// <summary>
        /// Determines whether this instance [can append string].
        /// </summary>
        [Test]
        public void CanAppendString()
        {
            QueryBuilder builder = new QueryBuilder();
            string valueToAppend = "Value To Append";

            builder.Append(valueToAppend);

            Assert.That(!string.IsNullOrWhiteSpace(builder.QueryText));
            Assert.That(valueToAppend == builder.QueryText);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanAppendStringAndParameter()
        {
            string valueToAppend = "Value To Append";
            QueryBuilder builder = new QueryBuilder();
            MySqlParameter parameter = new MySqlParameter() { ParameterName = "Param1" };

            builder.Append(valueToAppend, parameter);

            Assert.IsNotNull(builder.Parameters);
            Assert.That(!string.IsNullOrWhiteSpace(builder.QueryText));
            Assert.That(valueToAppend == builder.QueryText);
            Assert.That(builder.Parameters.Count() == 1);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanAppendStringAndParameters()
        {
            string valueToAppend = "Value To Append";
            QueryBuilder builder = new QueryBuilder();
            List<DbParameter> parameters = new List<DbParameter>() 
            { 
                new MySqlParameter() { ParameterName = "Param1" },
                new MySqlParameter() { ParameterName = "Param2" },
                new MySqlParameter() { ParameterName = "Param3" }
            };

            builder.Append(valueToAppend, parameters);

            Assert.IsNotNull(builder.Parameters);
            Assert.That(!string.IsNullOrWhiteSpace(builder.QueryText));
            Assert.That(valueToAppend == builder.QueryText);
            Assert.That(builder.Parameters.Count() == 3);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanClearParameters()
        {
            QueryBuilder builder = new QueryBuilder();
            List<DbParameter> parameters = new List<DbParameter>()
            {
                new MySqlParameter() { ParameterName = "Param1" },
                new MySqlParameter() { ParameterName = "Param2" },
                new MySqlParameter() { ParameterName = "Param3" }
            };

            builder.AddParameterRange(parameters);

            Assert.IsNotNull(builder.Parameters);
            Assert.That(builder.Parameters.Count() == 3);

            builder.ClearParameters();

            Assert.IsNotNull(builder.Parameters);
            Assert.That(builder.Parameters.Count() == 0);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanFindParameterByName()
        {
            QueryBuilder builder = new QueryBuilder();
            MySqlParameter parameter = new MySqlParameter() { ParameterName = "@Param1", Value = 1};

            builder.AddParameter(parameter);

            DbParameter param = builder.GetParameter(parameter.ParameterName);

            Assert.IsNotNull(param);
            Assert.AreEqual(typeof(MySqlParameter), param.GetType());
            Assert.That(builder.Parameters.Count() == 1);
            Assert.That(param.ParameterName == parameter.ParameterName);
            Assert.That(param.Value == parameter.Value);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanRemoveParameterByName()
        {
            QueryBuilder builder = new QueryBuilder();
            MySqlParameter parameter = new MySqlParameter() { ParameterName = "@Param1", Value = 1 };

            builder.AddParameter(parameter);

            builder.RemoveParameter(parameter.ParameterName);

            Assert.IsNotNull(builder.Parameters);
            Assert.That(builder.Contains(parameter.ParameterName) == false);
            Assert.That(builder.Parameters.Count() == 0);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanReplaceParameterByName()
        {
            QueryBuilder builder = new QueryBuilder();
            MySqlParameter parameter = new MySqlParameter() { ParameterName = "@Param1", Value = 1 };
            MySqlParameter newParam = new MySqlParameter() { ParameterName="@Param2", Value = "SomeValue" };
            
            builder.AddParameter(parameter);

            builder.ReplaceParameter(parameter.ParameterName, newParam);

            Assert.IsNotNull(builder.Parameters);
            Assert.That(builder.Parameters.Count() == 1);
            Assert.AreNotEqual(parameter, newParam);
            Assert.AreNotEqual(parameter.Value, newParam.Value);
            Assert.AreNotEqual(parameter.ParameterName, newParam.ParameterName);
        }
        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CanSetParameterValueByName()
        {
            QueryBuilder builder = new QueryBuilder();
            MySqlParameter parameter = new MySqlParameter() { ParameterName = "@Param1", Value = 1 };

            builder.AddParameter(parameter);
            builder.SetParamaterValue(parameter.ParameterName, 333);
            DbParameter param = builder.GetParameter(parameter.ParameterName);

            Assert.IsNotNull(builder.Parameters);
            Assert.That(builder.Parameters.Count() == 1);
            Assert.That(param.ParameterName == parameter.ParameterName);
            Assert.That(333 == (int)param.Value);
        }
        #endregion
    }
}