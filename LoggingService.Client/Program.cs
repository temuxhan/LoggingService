using LoggingService.Core;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;

namespace LoggingService.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 100; i++)
            {
                var thread = new Thread(CallApi);
                threads.Add(thread);

                thread.Start();
            }

            System.Console.ReadKey();
        }

        private static int counter = 0;

        private static void CallApi(object obj)
        {
            for (int i = 0; i < 3; i++)
            {
                string baseUri = "https://localhost:44312/logs";
                List<LogMessageModel> logMessageModels = new List<LogMessageModel>();
                for (int j = 0; j < 1500; j++)
                {
                    logMessageModels.Add(new LogMessageModel { Application = "Console", Log_date = 1600935069, Message = "[error] Could not connect to the database!'" });
                }

                HttpClient client = new HttpClient();
                JsonContent jsonContent = JsonContent.Create(logMessageModels, typeof(IEnumerable<LogMessageModel>));

                var response = client.PostAsync(baseUri, jsonContent).Result;
                ++counter;
                System.Console.WriteLine($"[{counter * 15000}] {response.Content.ReadAsStringAsync().Result}");
                Thread.Sleep(1000);
            }
        }
    }
}