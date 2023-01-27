using System;
using System.Security.AccessControl;
using System.Text;

namespace ExchangeService.Data
{
    public class AppDataContext
    {
        private static List<string> BNB { get; set; } = new List<string> {
            "BNB/ETH", "BNB/BTC", "ETC/BNB", "BCH/BNB"
        };
        private static List<string> ETH { get; set; } = new List<string> {
            "BNB/ETH", "ETH/BTC", "ETC/ETH"
        };
        private static List<string> RVN { get; set; } = new List<string> {
            "RVN/BTC"
        };
        private static List<string> ETC { get; set; } = new List<string> {
            "ETC/ETH", "ETC/BTC", "ETC/BNB"
        };
        private static List<string> BTC { get; set; } = new List<string> {
            "BNB/BTC", "ETH/BTC", "RVN/BTC", "ETC/BTC", "BCH/BTC"
        };
        private static List<string> BCH { get; set; } = new List<string> {
            "BCH/BNB", "BCH/BTC"
        };
        private IDictionary<string, List<string>> tickers = new Dictionary<string, List<string>>()
        {
            {"BNB", BNB }, {"ETH", ETH }, {"RVN", RVN },
            {"ETC", ETC }, {"BTC", BTC }, {"BCH", BCH },
        };


        public List<string> GetTickerList(string name)
        {
            foreach (var i in tickers)
            {
                if (i.Key == name)
                {
                    return i.Value;
                }
            }
            return new List<string>();
        }

        public string ReadData()
        {
            var path = @"./Data/data.txt";
            try
            {
                return File.ReadAllText(path, Encoding.UTF8);
            }
            catch (FileNotFoundException ex)
            {
                return ex.Message;
            }
        }

        public void SaveData(string name)
        {
            var path = @"./Data/data.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(name);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

