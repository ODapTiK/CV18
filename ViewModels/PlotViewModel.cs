﻿using CV18.Data.HttpRequest;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV18.ViewModels
{
    internal class PlotViewModel
    {
        private string countryName;
        private string provinceName;
        private const string httpAdress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        private CV19HttpRequest request = new CV19HttpRequest(httpAdress);
        public PlotViewModel(string countryName) { 
            this.countryName = countryName;
            this.provinceName = "";
        }
        public PlotViewModel(string countryName, string provinceName) { 
            this.provinceName = provinceName;
            this.countryName = countryName;
        }
        private async Task<DateTime[]> GetDateTimes()
        {
            IAsyncEnumerable<string> sequence = request.GetLines();
            List<string> data = await sequence.ToListAsync();
            DateTime[] dates = data.First().Split(',').Skip(4).Select(x => DateTime.Parse(x, CultureInfo.InvariantCulture)).ToArray();
            return dates;
        }
        private async IAsyncEnumerable<(string country, string province, int[] countsOfInfected)> GetData()
        {
            var lines = request.GetLines().Skip(1).Select(x => x.Split(','));
            await foreach (var line in lines)
            {
                var province = line[0].Trim();
                var country = line[1].Trim(' ', '"');
                var countsOfInfected = line.Skip(4).Select(int.Parse).ToArray();
                yield return (country, province, countsOfInfected);
            }
        }
        public async Task<DataPoint[]> CreatePlot()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                IAsyncEnumerable<(string country, string province, int[] countsOfInfected)> sequence = GetData(); 
                List<(string country, string province, int[] countsOfInfected)> dataList = await sequence.ToListAsync();
                var country = dataList.First(x => x.country.Equals("Belarus", StringComparison.OrdinalIgnoreCase));
                var data = new List<DataPoint>();
                var a = GetDateTimes().Result.Count();
                for (int i = 0; i < a; i++)
                {
                    data.Add(new DataPoint(i, country.countsOfInfected[i]));
                }
                return data.ToArray();
            }
            else { 
                Console.WriteLine("Нет подключения к интернету");
                return null;
            }
        }
    }
}
