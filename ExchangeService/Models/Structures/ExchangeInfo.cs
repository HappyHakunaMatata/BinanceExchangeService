using System;
using ExchangeService.Models.Structures;


namespace ExchangeService.Models.Structures
{
    public struct ExchangeInfo
    {
        public List<SymbolExchangeInfo> symbols { get; set; }
    }
}

