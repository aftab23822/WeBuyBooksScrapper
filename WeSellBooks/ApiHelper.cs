using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeSellBooks
{
    internal class ApiHelper
    {
        private string BearerToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJoYXNDb21wbGV0ZWRGaXJzdFRyYWRlIjpmYWxzZSwicHJvbXB0QmlvbWV0cmljcyI6dHJ1ZSwicHJvQWNjb3VudEVsaWdpYmxlIjpmYWxzZSwiaWQiOiI5OTZkN2IzYTBiYjYzNTA3NmJjMzUwMTJiZmZlNWFkY2ZhMTQ3MjczIiwianRpIjoiOTk2ZDdiM2EwYmI2MzUwNzZiYzM1MDEyYmZmZTVhZGNmYTE0NzI3MyIsImlzcyI6IiIsImF1ZCI6IjEiLCJzdWIiOiI3ODA0NjUiLCJleHAiOjE3MDU1MjMwNTcsImlhdCI6MTcwNTUwMTQ1NywidG9rZW5fdHlwZSI6ImJlYXJlciIsInNjb3BlIjoiY3VzdG9tZXIifQ.eVE9gB5llep-QNta7ZomybgPULKIUj83En1qQk_LO-3n_NfmVIiZsUjuCuDrF0qwfzYXaBlR2cvo7jhDVKwTVDcj01s-6m67mPbSg95kUkeaG8tvRUqDop0n06TLebzoBMvdQXV_KZL7b6bQ1C3juuMLGrF-BECTwPEfy9XOUZMIrCJpGa6L46BxnbgP58m1bexpEXQw4Wte77KxVaqpPkuP-OLgk0xwwkiz5tNEiyGoNKbl4pBkXlI7l43gn8bwhjrv9ZGwqOqLEwnjLqgVMcGX9Lj7CoFE-wB2HGaOx15TaC-7qMHBGVU90ISaMZv17gKo-JF6w-I_xRnH4_s28Q";

        public async Task<bool> EraseBasket()
        {
            return await DeleteFromBasket();
        }

        public async Task<BasketItemResponse> GetDetailsForIsbn(string isbn)
        {
            try
            {
                var basketItemResponse = await TryApiCall(isbn);

                return basketItemResponse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public async Task<BasketItemResponse> TryApiCall(string isbn)
        {
            Thread.Sleep(5000);
            var options = new RestClientOptions("https://api2.revivalbooks.co.uk")
            {
                MaxTimeout = -1,
                // Bypass SSL certificate validation
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                FollowRedirects = true
            };
            var client = new RestClient(options);
            var request = new RestRequest("/basket/item", Method.Post);

            request.AddHeader("X-Revival-Site", "1");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {BearerToken}");
            var body = @"{""barcode"": """ + isbn + @"""}";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var token = LoginAndGetToken();
                return await TryApiCall(isbn);
            }
            //string sampleBasketResponsefilePath = @"C:\Users\Aftab Ahmed\source\repos\WeSellBooks\WeSellBooks\sample basket response.json";
            //var obj = JsonConvert.DeserializeObject<BasketItemResponse>(File.ReadAllText(sampleBasketResponsefilePath));
            //return obj;
            return JsonConvert.DeserializeObject<BasketItemResponse>(response.Content);
        }

        public bool LoginAndGetToken()
        {
            var options = new RestClientOptions("https://api2.revivalbooks.co.uk")
            {
                MaxTimeout = -1,
                // Bypass SSL certificate validation
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                FollowRedirects = true
            };
            var client = new RestClient(options);
            var loginRequest = new RestRequest("auth/customer-login", Method.Post);
            loginRequest.AddHeader("X-Revival-Site", "1");
            loginRequest.AddHeader("Content-Type", "application/json");
            loginRequest.AddHeader("Authorization", "Basic MTpGVlp3YmJXQUVOTUpLTkxS");
            var loginBody = @"{
                    ""grant_type"": ""password"",
                    ""username"": ""hafenof391@wentcity.com"",
                    ""password"": ""hafenof391@wentcity.com""
                }";
            loginRequest.AddStringBody(loginBody, DataFormat.Json);
            RestResponse loginResponse = client.Execute(loginRequest);
            var responseObj = JsonConvert.DeserializeObject<ApiResponse>(loginResponse.Content);
            string accessToken = responseObj?.access_token;
            BearerToken = string.IsNullOrEmpty(accessToken) ? BearerToken : accessToken;
            return !string.IsNullOrEmpty(accessToken);
        }


        public async Task<bool> DeleteFromBasket()
        {
            var options = new RestClientOptions("https://api2.revivalbooks.co.uk")
            {
                MaxTimeout = -1,
                // Bypass SSL certificate validation
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                FollowRedirects = true
            };
            var client = new RestClient(options);
            var request = new RestRequest($"/basket", Method.Get);

            request.AddHeader("X-Revival-Site", "1");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {BearerToken}");
            RestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var token = LoginAndGetToken();
                return await DeleteFromBasket();
            }
            var backet = JsonConvert.DeserializeObject<basket>(response.Content);
            foreach (var item in backet?.items)
            {
                await DeleteFromBasket(item.id?.ToString());
            }
            return response.StatusCode == HttpStatusCode.OK;
        }


        public async Task<bool> DeleteFromBasket(string id)
        {
            Thread.Sleep(TimeSpan.FromSeconds(3));
            var options = new RestClientOptions("https://api2.revivalbooks.co.uk")
            {
                MaxTimeout = -1,
                // Bypass SSL certificate validation
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                FollowRedirects = true
            };
            var client = new RestClient(options);
            var request = new RestRequest($"/basket/item{id}", Method.Delete);
            request.AddHeader("X-Revival-Site", "1");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {BearerToken}");
            RestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var token = LoginAndGetToken();
                return await DeleteFromBasket(id);
            }
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
