using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Assgt1
{
    public class AddressValidator
    {
        private string apiKey;

        
        public AddressValidator()
        {

            //apiKey for your google account
            //apiKey = your_developer_key;
        }
        public bool queryPostCode(string postCode, string country)
        {
            //using Goole's geocode API

            //Date Created 09/06/2019

            //trying to get the response as a string was a bit frustrating, so I used this source
            //Date Accessed: 2/04/2021
            //Source: Stack overflow
            //link: https://stackoverflow.com/questions/57828956/parsing-json-response-from-httpwebrequest
            var newRequest =(HttpWebRequest) WebRequest.Create("https://maps.googleapis.com/maps/api/geocode/json?components=postal_code:"+postCode+"|country:"+country+"&key="+apiKey);
            //web request settings
            //
            newRequest.Method = "GET";
            newRequest.ContentType = "application/json; charset=utf-8;";

            var response = (HttpWebResponse) newRequest.GetResponse();

            var ResponseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            AddressRequest ar = JsonConvert.DeserializeObject<AddressRequest>(ResponseString);
            //if the postCode is not valid then google's api won't return any search results
            //using this we can determine that the code the user provided was invalid
            if (ar.status == "OK")
                return true;
            else return false;
        }
        
    }
}