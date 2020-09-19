using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Dynamics_365_REST_CRUD
{
    class Program
    {

        private static HttpClient httpClient = new HttpClient();

        private static string serviceUrl = "https://msz12.crm4.dynamics.com";

        private static string clientId = "";

        private static string secret = "";

        static void Main(string[] args)
        {

            AuthenticationContext authContext = new AuthenticationContext("https://login.microsoftonline.com/tenantid");
            ClientCredential credential = new ClientCredential(clientId, secret);

            AuthenticationResult result = authContext.AcquireTokenAsync(serviceUrl, credential).Result;

            string accessToken = result.AccessToken;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage respone = httpClient.GetAsync("https://msz12.api.crm4.dynamics.com/api/data/v9.1/accounts").Result;
            string data = respone.Content.ReadAsStringAsync().Result;
            Console.WriteLine(data);

        }
    }
}
