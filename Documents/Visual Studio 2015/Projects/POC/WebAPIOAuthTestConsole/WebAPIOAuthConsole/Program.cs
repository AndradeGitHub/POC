using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebAPIOAuthConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Invoke();
        }

        private static void Invoke()
        {
            try
            {
                var pairs = new List<KeyValuePair<string, string>>
                {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", "Israel"),
                new KeyValuePair<string, string>("password", "12345")
                };

                var content = new FormUrlEncodedContent(pairs);

                var client = new HttpClient { BaseAddress = new Uri("http://localhost:55547") };

                // call sync
                //var response = await client.PostAsync("/issuer/token", content).;
                var response = client.PostAsync("/issuer/token", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var tokenBody = response.Content.ReadAsStringAsync();
                    dynamic parsedTokenBody = JsonConvert.DeserializeObject(tokenBody.Result);

                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55547/api/Test/GetData"))
                    {
                        requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue(
                                parsedTokenBody.token_type.ToString(),
                                parsedTokenBody.access_token.ToString());

                        using (var responseMessage = client.SendAsync(requestMessage))
                        {
                            //var responseBody = responseMessage.Result.Content.ReadAsStringAsync();
                            var responseBody = responseMessage.Result;

                            Console.WriteLine(responseBody);
                        }
                    }
                }

                //using (var client = new HttpClient())
                //{
                //    using (var tokenResponse = await client.PostAsync("http://localhost:55547/issuer/token", CreateContent()))
                //    {
                //        var tokenBody = await tokenResponse.Content.ReadAsStringAsync();
                //        dynamic parsedTokenBody = JsonConvert.DeserializeObject(tokenBody);

                //        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:55547/api/Test/GetData"))
                //        {
                //            requestMessage.Headers.Authorization =
                //                new AuthenticationHeaderValue(
                //                    parsedTokenBody.token_type.ToString(),
                //                    parsedTokenBody.access_token.ToString());

                //            using (var responseMessage = await client.SendAsync(requestMessage))
                //            {
                //                var responseBody = await responseMessage.Content.ReadAsStringAsync();

                //                Console.WriteLine(responseBody);
                //            }
                //        }
                //    }
                //}
            }
            catch(Exception ex)
            {
                throw (ex);
            }

        }

        //private static FormUrlEncodedContent CreateContent()
        //{
        //    //return new FormUrlEncodedContent(new[]
        //    //{
        //    //    new KeyValuePair<string, string>("grant_type", "password"),
        //    //    new KeyValuePair<string, string>("username", "Israel"),
        //    //    new KeyValuePair<string, string>("password", "12345")
        //    //});

        //    var body = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>("grant_type", "password"),
        //        new KeyValuePair<string, string>("username", "Israel"),
        //        new KeyValuePair<string, string>("password", "12345")                
        //    };

        //    var content = new FormUrlEncodedContent(body);

        //    return content;
        //    //var result = httpClient.PostAsync(uri, content).Result;
        //}
    }
}
