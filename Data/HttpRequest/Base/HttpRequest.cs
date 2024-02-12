using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CV18.Data.HttpRequest.Base
{
    internal class HttpRequest
    {
        public HttpRequest(string url)
        {
            adress = url;
        }

        public string adress { get; set; }

        public bool IsInternetConnectionAvailiable() => System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        public async Task<Stream> GetStream()
        {
            var client = new HttpClient();
            var answer = await client.GetAsync(adress, HttpCompletionOption.ResponseHeadersRead);
            return await answer.Content.ReadAsStreamAsync();
        }

        public IEnumerable<string> GetLines()
        {
            var lines = GetStream().Result;
            var dataReader = new StreamReader(lines);
            while (!dataReader.EndOfStream)
            {
                var line = dataReader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line;
            }
        }
    }
}
