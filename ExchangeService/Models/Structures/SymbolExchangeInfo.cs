using System;
namespace ExchangeService.Models.Structures
{
    public struct SymbolExchangeInfo
    {
        public string symbol { get; set; }
        public string baseAsset { get; set; }
        public string quoteAsset { get; set; }
        public List<Filter> filters { get; set; }
    }
}

