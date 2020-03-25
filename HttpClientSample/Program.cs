using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace HttpClientSample
{


    class Values
    {
        public string data { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    class City
    {
        [JsonProperty("regionId")]
        public string regionId { get; set; }
        [JsonProperty("cityName")]
        public string cityName { get; set; }
        [JsonProperty("stateName")]
        public string stateName { get; set; }

        [JsonProperty("cityPopulation")]
        public string cityPopulation { get; set; }

        [JsonProperty("area")]
        public string area { get; set; }

        public static implicit operator City(JToken v)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static string CapText(Match m)
        {
            string x = m.ToString();
            if (char.IsDigit(x[0]))
            {
                return "\"" + x[0] + x.Substring(1, x.Length - 1) + "\"";
            }
            return x;
        }
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();

            //var result = await httpClient.GetAsync("http://localhost:5000/api/values");

            HttpContent content1 = null;

            var result2 = await httpClient.PostAsync("http://localhost:5000/api/City/GetCity", content1);

            var content = result2.Content.ReadAsStringAsync().Result;

            //var newItem = string.Concat(content.Split('[', ']', '{', '}'));


            Regex regex = new Regex(@"[-+]?([0-9]*\.[0-9]+|[0-9]+)");

            var s = regex.Replace(content, new MatchEvaluator(Program.CapText));


            List<City> cityList = JsonConvert.DeserializeObject<List<City>>(s);

            //var obj = cityList[0];

            Console.WriteLine(result2.StatusCode);

            //City realCity = (City)city;

            //Console.WriteLine(s);

            Console.WriteLine(cityList[0].cityName);
        }
    }
}
