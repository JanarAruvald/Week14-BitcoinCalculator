using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitCoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            BitcoinRate currentBitcoin = GetRates();
            Console.WriteLine($"current rate: {currentBitcoin.bpi.EUR.code} {currentBitcoin.bpi.EUR.code}");

            Console.WriteLine("Calculate in: EUR/USD/GBP");
            string userChoice = Console.ReadLine();
            Console.WriteLine("Enter the amount of bitcoins:");
            float userCoins = float.Parse(Console.ReadLine());
            float currentRate = 0;

            if(userChoice == "EUR")
            {
                currentRate = currentBitcoin.bpi.EUR.rate_float;
            }
            if (userChoice == "USD")
            {
                currentRate = currentBitcoin.bpi.USD.rate_float;
            }
            if (userChoice == "GBP")
            {
                currentRate = currentBitcoin.bpi.GBP.rate_float;
            }

            float result = currentRate * userCoins;
            Console.WriteLine($"Your Bitcoins are worth {result} {userChoice}");
            
        }
        public static BitcoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "Get";

            var WebResponse = request.GetResponse();
            var WebStream = WebResponse.GetResponseStream();

            BitcoinRate BitcoinData;

            using (var ResponseReader = new StreamReader(WebStream))
            {
                var response = ResponseReader.ReadToEnd();
                BitcoinData = JsonConvert.DeserializeObject<BitcoinRate>(response);
            }
            return BitcoinData;
        }
    }
}
