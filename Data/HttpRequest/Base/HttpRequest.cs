using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CV18.Data.HttpRequest.Base
{
    internal class BaseHttpRequest
    {

        public BaseHttpRequest(string url)
        {
            adress = url;
        }

        public string adress;

        public bool IsInternetConnectionAvailiable() => System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        public async Task<Stream> GetStream()
        {
            HttpClient CV18Client = new HttpClient();
            var serverAnswer = await CV18Client.GetAsync(adress, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(true);
            return await serverAnswer.Content.ReadAsStreamAsync().ConfigureAwait(true);
        }

        public virtual async IAsyncEnumerable<string> GetLines()
        {
            using var dataStream = await GetStream().ConfigureAwait(true);
            using var dataReader = new StreamReader(dataStream);
            while (!dataReader.EndOfStream)
            {
                var line = dataReader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line;
            }
        }
    }
}
