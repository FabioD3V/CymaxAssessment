using CymaxAssessment.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CymaxAssessment.Client
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static string source, destination = "";
        private static int[] cartonDimensions;
        private static XmlSerializer xmlSerializer;

        static async Task Main(string[] args)
        {
            // For testing and evaluating purpose.
            // The values can be changed as you will.
            source = "Custom Source";
            destination = "Custom Destination";
            cartonDimensions = new int[] { 1, 3 };

            var quoteTasks = new[]
            {
                GetOffer(1),
                GetOffer(2),
                GetOffer(3)
            };

            // Await all offers to be retrieved
            var responses = await Task.WhenAll(quoteTasks);

            // Find the best offer among all responses
            var bestOffer = responses.SelectMany(r => r.Values).Min();

            // A separated variable to hold the best offer data
            // I could make use of the responses variable as well
            // However I decided to keep responses as it is so if you want to check all values by yourself  :)
            var result = responses.Where(x => x.Values.Contains(bestOffer));

            // Fetching all possible "best offers" in case we would have two or more APIs asking for the same price
            foreach (var item in result)
            {
                Console.WriteLine("The best offer was found on the API: {0}", item.Keys.FirstOrDefault());
                Console.WriteLine("Offering the price of: {0}", item.Values.FirstOrDefault());
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Gets the offer for each desired API.
        /// The parameter is just an int representing the API number as found in the assessment,
        /// in a real world scenario it would be companies name, url, api link, etc, depending how
        /// we would want to structure this app.
        /// </summary>
        /// <param name="apiNumber">Desired API number</param>
        /// <returns></returns>
        public static async Task<Dictionary<string, double>> GetOffer(int apiNumber)
        {
            var token = await GetAccessToken(apiNumber);
            var content = await GetContent(apiNumber);
            var quote = ProcessRequest(apiNumber, token, content);

            return quote.Result;
        }

        /// <summary>
        /// Gets the request content to be passed in according to each API.
        /// </summary>
        /// <param name="apiNumber">Desired API number</param>
        /// <returns></returns>
        private static async Task<string> GetContent(int apiNumber)
        {
            string content = "";

            switch (apiNumber)
            {
                case 1:
                    var requestModelAPI1 = new RequestModelAPI1
                    {
                        ContactAddress = source,
                        WarehouseAddress = destination,
                        PackageDimensions = cartonDimensions
                    };
                    content = JsonConvert.SerializeObject(requestModelAPI1);
                    break;

                case 2:
                    var requestModelAPI2 = new RequestModelAPI2
                    {
                        Consignee = source,
                        Consignor = destination,
                        Cartons = cartonDimensions
                    };
                    content = JsonConvert.SerializeObject(requestModelAPI2);
                    break;

                case 3:
                    var pkgs = new Packages();
                    pkgs.Package = new List<int>();
                    pkgs.Package.Add(1);
                    pkgs.Package.Add(2);

                    var requestModelAPI3 = new RequestModelAPI3
                    {
                        Source = source,
                        Destination = destination,
                        Packages = pkgs
                    };

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (XmlTextWriter tw = new XmlTextWriter(ms, Encoding.UTF8))
                        {
                            xmlSerializer = new XmlSerializer(requestModelAPI3.GetType());
                            tw.Formatting = System.Xml.Formatting.Indented;
                            xmlSerializer.Serialize(tw, requestModelAPI3);
                            content = Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }
                    break;

                default:
                    break;
            }

            return content;
        }

        /// <summary>
        /// Process the request call to the API accordingly.
        /// </summary>
        /// <param name="apiNumber"></param>
        /// <param name="token"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private static async Task<Dictionary<string, double>> ProcessRequest(int apiNumber, string token, string content)
        {
            var quote = new Dictionary<string, double>();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var uri = new Uri("https://localhost:44363/api/Carton/ClientRequestFromCompanyAPI" + apiNumber);
            
            var stringContent = apiNumber == 3 ? new StringContent(content, Encoding.UTF8, "application/xml") : new StringContent(content, Encoding.UTF8, "application/json");
            
            // *** Connects to the API to retrieve the required response ***
            var response = await client.PostAsync(uri, stringContent);

            // Here throwing a generic error in case of non success status code. 
            // However it would be better with error handler is implemented.
            if (!response.IsSuccessStatusCode) throw new Exception("Error: " + response.StatusCode.ToString());

            var responseContent = response.Content.ReadAsStringAsync().Result;

            // Special case for the API #3, where it uses xml instead of json
            // This kind of code use to be in a service or helper class in real world scenarios
            // But for the sake of simplicity and time I'm doing this here.
            if (apiNumber == 3)
            {
                using (var stringReader = new StringReader(responseContent))
                using (var xmlReader = XmlReader.Create(stringReader))
                {
                    if (true)
                    {
                        xmlReader.ReadToDescendant("quote");
                        var xmlSerializer = new XmlSerializer(typeof(string), new XmlRootAttribute("quote"));
                        var value = (string)xmlSerializer.Deserialize(xmlReader.ReadSubtree());
                        quote.Add(uri.ToString(), double.Parse(value));
                    }
                }
            }
            else
            {
                quote.Add(uri.ToString(), double.Parse(responseContent));
            }

            return quote;
        }

        /// <summary>
        /// Get's the access token from the API's accordingly to their crendentials
        /// </summary>
        /// <param name="apiNumber"></param>
        /// <returns></returns>
        public static async Task<string> GetAccessToken(int apiNumber)
        {
            // For the sake of time and simplicity, just kept it hard coded.
            // It could be done better, for example using Config variables (appsettings, etc).
            var authUri = new Uri("https://localhost:44363/api/Carton/Authenticate");
            var content = new StringContent(GetCredentials(apiNumber), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(authUri, content);
            var accessToken = await response.Content.ReadAsStringAsync();

            return accessToken;
        }

        /// <summary>
        /// Get the credentials for a specific API.
        /// </summary>
        /// <param name="apiNumber"></param>
        /// <returns></returns>
        public static string GetCredentials(int apiNumber)
        {
            var userCredential = new UserCredentials
            {
                EndPointId = apiNumber,
                Username = "userAPI" + apiNumber.ToString(),
                Password = "passwordAPI" + apiNumber.ToString()
            };

            return JsonConvert.SerializeObject(userCredential);
        }
    }
}
