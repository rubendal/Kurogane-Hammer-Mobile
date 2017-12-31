using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Kurogane_Hammer
{
    public static class APIConnection
    {
        private static HttpClient _client = new HttpClient();
        private static readonly string API_SERVER = "http://beta-api-kuroganehammer.azurewebsites.net";

        public static string MakeURL(string route)
        {
            if (route[0] != '/')
                route = "/" + route;

            return $"{API_SERVER}{route}";
        }

        public static async Task<string> Request(string url)
        {
            using (var response = await _client.GetAsync(MakeURL(url)))
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return json;
                }
            }

            return null;
        }
    }
}
