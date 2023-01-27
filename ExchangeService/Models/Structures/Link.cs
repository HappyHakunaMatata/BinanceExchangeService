

namespace ExchangeService.Models.Structures
{
    public class Link
    {
        public string Wallet = "https://www.binance.com/api/v3/account";
        public string Klines = "https://www.binance.com/api/v3/klines";
        public string order = "https://api.binance.com/api/v3/depth";
        public string exchangeInfo = "https://api.binance.com/api/v3/exchangeInfo";
        public string openOrders = "https://api.binance.com/api/v3/openOrders";
        public string fee = "https://api.binance.com/sapi/v1/asset/tradeFee";
    }
}