using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace ApiClient
{
    /// <summary>
    /// Test client to call the payment gateway api using a bearer token. Make sure that Api and IdentityServer are
    /// up and running before run this console application.
    /// You can also get the token from the console output.
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var tokenResponse = await GetToken();

                await CallApiWithoutToken();

                await CallApi(tokenResponse);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task<TokenResponse> GetToken()
        {
            PrintTitle("Fetching a bearer token from Identity Server");

            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:4000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return null;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "merchant",
                ClientSecret = "secret",
                Scope = "payment-gateway"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }

            Console.WriteLine(tokenResponse.Json);

            return tokenResponse;
        }

        private static async Task CallApiWithoutToken()
        {
            PrintTitle("Calling the api without the token");
            
            // call api
            var client = new HttpClient();

            var response = await client.GetAsync("https://localhost:5001/payments/merchants/1");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }
        
        private static async Task CallApi(TokenResponse tokenResponse)
        {
            PrintTitle("With the token, try fetching a list of payments");
            
            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("https://localhost:5001/payments/1");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }

        private static void PrintTitle(string title)
        {
            Console.WriteLine(new string('-', 120));
            Console.WriteLine($"*** {title} ***");
            Console.WriteLine(new string('-', 120));
        }
    }
}