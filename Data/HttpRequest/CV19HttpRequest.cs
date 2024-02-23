using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CV18.Data.HttpRequest.Base;

namespace CV18.Data.HttpRequest
{
    internal class CV19HttpRequest : BaseHttpRequest
    {
        public CV19HttpRequest(string url) : base(url)
        {

        }
        public override IEnumerable<string> GetLines()
        {
            var lines = GetStream().Result;
            var dataReader = new StreamReader(lines);
            while (!dataReader.EndOfStream)
            {
                var line = dataReader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line.Replace("Korea,", "Korea-");
            }
        }
    }
}
