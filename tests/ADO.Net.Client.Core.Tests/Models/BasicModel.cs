#region Using Statements
using ADO.Net.Client.Annotations;
using System;
#endregion

namespace ADO.Net.Client.Core.Tests.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BasicModel
    {
        public TimeSpan Time { get; set; }
        public DateTime NormalDateTime { get; set; }
        [DateTime2]
        public DateTime DateTime2 { get; set; }
        public string NormalString { get; set; }
        [ANSIString]
        public string AnsiString { get; set; }
    }
}
