using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ShowDataXML
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient webClient = new WebClient();
            string downloadedString = webClient.DownloadString("https://feber.paxx.up2technology.com/health");


            var json = JObject.Parse(downloadedString);

            var keyWord = "adapter";

            var entries = json["entries"].ToList();

            List<JToken> searchedEntries = new List<JToken>();

            foreach (var entry in entries)
            {
                var currentEntry = JsonConvert.SerializeObject(entry);

                var entryElements = currentEntry.Split('"').ToArray();

                var entryName = entryElements[1].ToLower();

                if (entryName.Contains(keyWord))
                {
                    searchedEntries.Add(entry);
                }
            }

            var jsonArray = string.Join(Environment.NewLine, searchedEntries);

            Console.WriteLine(jsonArray);

        }
        
    }
}
