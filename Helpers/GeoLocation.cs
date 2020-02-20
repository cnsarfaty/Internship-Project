using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Core.Servers;
using Newtonsoft.Json;
using System.Collections;
using MongoDB.Driver.Core;
using ConvertIPAddressToLocation.model;


namespace ConvertIPAddressToLocation.Helpers
{
    public class GeoLocation


    {
        public Symbol FetchIPLocation(string strIP)
        {
            string strIpLocation = string.Empty;

            var client = new RestClient("https://ipapi.co/" + strIP + "/json/");
            var request = new RestRequest() { Method = Method.GET };
            var response = client.Execute(request);
            IDictionary dictionary= null;
            try
            {
                dictionary = JsonConvert.DeserializeObject<IDictionary>(response.Content);
            }
            catch (Exception e){
                Console.WriteLine("Invalid IP Address");
            };

            if (dictionary != null)
            {


                string newLine = Environment.NewLine;
                foreach (var key in dictionary.Keys)
                {
                    strIpLocation += key.ToString() + ": " + dictionary[key] + newLine;

                }
                Symbol symbol = new Symbol();

                symbol.IP = strIP;
                symbol.City = dictionary["city"].ToString();
                symbol.Region = dictionary["region"].ToString();
                symbol.Country = dictionary["country"].ToString();
                symbol.dateEntry = DateTime.Now;
                symbol.longitude = float.Parse(dictionary["longitude"].ToString());
                symbol.latitude = float.Parse(dictionary["latitude"].ToString());

                return symbol;
            }
            else
            {
                return null;
            }
        }
  
    }
}
