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
using ADO.Net.Client.Tests.Common.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
#endregion;

namespace ADO.Net.Client.Implementation.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MultiResultReaderTests
    {
        #region Tests
        [Test]
        [Category("MultiResultReader Async Tests")]
        public async Task WhenReadObject_IsCalled_ShouldCall_ReaderReadAsync()
        {
            int delay = _faker.Random.Int(0, 1000);

            //Wrap in a using statement to dispose of resources automatically
            using (CancellationTokenSource source = new CancellationTokenSource(delay))
            {
                DateTime dateBirth = _faker.Person.DateOfBirth;
                string firstName = _faker.Person.FirstName;
                string lastName = _faker.Person.LastName;
                PersonModel expectedModel = new PersonModel()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateBirth
                };
                _mockMapper.Setup(x => x.MapRecord<PersonModel>(_mockReader.Object)).Returns(expectedModel);

                MultiResultReader multiReader = new MultiResultReader(_mockReader.Object, _mockMapper.Object);
                PersonModel returnedModel = await multiReader.ReadObjectAsync<PersonModel>(source.Token);

                Assert.IsNotNull(returnedModel);
                Assert.IsTrue(returnedModel.DateOfBirth == expectedModel.DateOfBirth);
                Assert.IsTrue(returnedModel.FirstName == expectedModel.FirstName);
                Assert.IsTrue(returnedModel.LastName == expectedModel.LastName);

                //Verify the readers read method was called
                _mockMapper.Verify(x => x.MapRecord<PersonModel>(_mockReader.Object), Times.Once);
                _mockReader.Verify(x => x.ReadAsync(source.Token), Times.Once);
            }
        }
        #endregion
    }
}