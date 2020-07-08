#region Using Statements
using ADO.Net.Client.Annotations;
#endregion

namespace ADO.Net.Client.Core.Tests.Models
{
    public class DirectionModel
    {
        #region Fields/Properties
        [Output]
        public object Output { get; set; }
        [ReturnValue]
        public object ReturnValue { get; set; }
        [Input]
        public object Input { get; set; }
        [InputOutput]
        public object InputOutput { get; set; }
        public object NoDirection { get; set; }
        #endregion
    }
}