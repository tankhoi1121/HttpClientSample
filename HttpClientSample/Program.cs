using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Json;
using System.Collections.Generic;

namespace HttpClientSample
{


    class Values
    {
        public string data { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();

            var result = await httpClient.GetAsync("http://localhost:5000/api/values");

            var content = await result.Content.ReadAsStringAsync();

            var dict = new Dictionary<string, string>();
            dict["text"] = content;

            Console.WriteLine(dict["text"]);
        }
    }
}
