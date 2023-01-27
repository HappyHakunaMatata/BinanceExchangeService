using System;
namespace ExchangeService.Models.Structures
{
    public struct Fee
    {
        public string symbol { get; set; }
        public string makerCommission { get; set; }
        public string takerCommission { get; set; }
    }
}

